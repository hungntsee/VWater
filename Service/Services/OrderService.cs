using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using RabbitMQ;
using Repository.ZaloPayHelper;
using Service.Helpers;
using System.Reflection.PortableExecutable;
using System.Web;
using VNPAY_CS_ASPX;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;
using ZaloPay.Helper;
using ZaloPay.Helper.Crypto;

namespace Service.Services
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetAll();
        public IEnumerable<Order> GetOrderByStore(int store_id);
        public IEnumerable<Order> GetAllOrderByStatus( int status_id);
        public IEnumerable<Order> GetOrderOfStoreByStatus(int store_id, int status_id);
        public IEnumerable<Order> GetOrderOfShipperByStatus(int shipper_id, int status_id);
        public Order GetById(int id);
        public Order Create(OrderCreateModel model);
        public void Update(int id, OrderUpdateModel model);
        public void Delete(int id);
        public Order TakeOrder(int id, int shipper_id);
        public Order GetLastestOrder(int customer_id);
        public List<Order> GetOrderByCustomer(int customer_id);
        public List<Order> FollowOrder(int customer_id);
        public Order ReOrder(int order_id);
        public int GetNumberOfOrder();
        public DepositNote CreateDepositeNote(DepositNoteCreateModel model);
        public void CancelOrder(int order_id);
        public void ConfirmOrder(int order_id);
        public void FinishOrder(int order_id);      
        //public Order GetOrderByStatusForShipper(int shipper_id, int status_id);
        public int CountOrderByStatus();
        public List<Order> GetNewOrderByStoreId(int store_id);
        public Task<Dictionary<string, object>> CreateOrderWithZaloPay(OrderCreateModel model);
        public Dictionary<string, object> CallBackFromZalo(ZaloCallBackRequest cbData);
        public string CreateOrderWithVNPay(OrderCreateModel model);
        public string VNPayIpn(HttpRequest request);
        public List<Order> GetOrderByShipper(int shipper_id);
        //public List<Order> GetOrderByStatus(int status_id);
    }
    public class OrderService : IOrderService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private Send send = new Send();
        private IConfiguration _config;
        private IHttpContextAccessor _contextAccessor;

        public OrderService(VWaterContext context, IMapper mapper, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _config = configuration;
            _contextAccessor = contextAccessor;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _context.Orders.Include(a => a.OrderDetails);
            var list = orders.ToList().OrderByDescending(a => a.OrderDate);
            return list;
        }

        public Order GetById(int id)
        {
            var orderResponse = GetOrder(id);
            OrderJsonFile(orderResponse);
            orderResponse.DeliveryAddress.Customer.DeliveryAddresses = null;
            return orderResponse;
        }

        private void CheckPayingOrder(int customer_id)
        {
            var orders = OrderExtensions.ByCustomerId(_context.Orders,customer_id);
            foreach (var order in orders)
            {
                if (order.StatusId == 6) throw new AppException("Bạn đang có 1 đơn hàng chưa thanh toán.");
            }
        }

        public Order Create(OrderCreateModel model)
        {
            if (model.OrderDetails == null) throw new AppException("Không có sản phẩm nào trong giỏ hàng của bạn.");
            var order = _mapper.Map<Order>(model);
            order.OrderDate = DateTime.UtcNow.AddHours(7);
            order.DeliveryAddress = _context.DeliveryAddresses.AsNoTracking().FirstOrDefault(a => a.Id == order.DeliveryAddressId);
            order.StoreId = order.DeliveryAddress.StoreId;
            order.IsDeposit = false;           
            order.ShipperId = null;
            order.AmountPaid = 0;

            CheckPayingOrder(order.DeliveryAddress.CustomerId);

            order.DeliveryAddress = null;

            if (order.TotalPrice > 500000) order.StatusId = 1;
            else order.StatusId = 2;

            _context.Orders.Add(order);
            _context.SaveChanges();

            var responseOrder = GetOrder(order.Id);
            OrderJsonFile(responseOrder);
            responseOrder.DeliveryAddress.Customer.DeliveryAddresses = null;
            if (order.StatusId == 2)
            {
                var message = JsonConvert.SerializeObject(responseOrder, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                send.SendMessage(message);
            }
            return responseOrder;
        }

        #region Payment With Zalo
        public async Task<Dictionary<string,object>> CreateOrderWithZaloPay(OrderCreateModel model)
        {
            if (model.OrderDetails == null) throw new AppException("Không có sản phẩm nào trong giỏ hàng của bạn.");
            var order = _mapper.Map<Order>(model);

            order.OrderDate = DateTime.UtcNow.AddHours(7);
            order.DeliveryAddress = _context.DeliveryAddresses.AsNoTracking().FirstOrDefault(a => a.Id == order.DeliveryAddressId);
            order.StoreId = order.DeliveryAddress.StoreId;
            order.IsDeposit = false;
            order.ShipperId = null;

            CheckPayingOrder(order.DeliveryAddress.CustomerId);

            order.DeliveryAddress = null;

            //Add data for request to zalo
            var amount = long.Parse(order.TotalPrice.ToString());
            var embed_data = new
            {
                redirect = "https://vwater-user-ui.vercel.app/order-tracking"
            };
            var zaloRequestData = new ZaloPayCreateRequest(amount, embed_data, null, _config);

            var result = await HttpHelper.PostFormAsync(_config["ZaloPay:ZaloPayApiCreateOrder"], zaloRequestData.AsParams());

            var returnCode = (long)result["returncode"];
            if (returnCode == 1)
            {
                order.StatusId = 6;
                order.OrderIdMomo = zaloRequestData.AppTransId;
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            
            return result;
        }

        public Dictionary<string, object> CallBackFromZalo(ZaloCallBackRequest cbData)
        {
            var result = new Dictionary<string, object>();
            try
            {
                var dataStr = Convert.ToString(cbData.Data);
                var requestMac = Convert.ToString(cbData.Mac);

                var isValidCallBack = VerifyCallback(dataStr, requestMac);
                if(!isValidCallBack)
                {
                    result["returncode"] = - 1;
                    result["returnmessage"] = "mac not equal";
                }
                else
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                    var apptransid = data["apptransid"].ToString();

                    var order = _context.Orders.AsNoTracking().FirstOrDefault(a => a.OrderIdMomo == apptransid);
                    if (order != null)
                    {
                         var ipnData = new
                        {
                            Zptransid = data["zptransid"].ToString(),
                            Channel = int.Parse(data["channel"].ToString()),
                            Status = 1
                        };
                        order.IpnData = Convert.ToString(ipnData);
                        order.StatusId = 7;
                        order.AmountPaid = order.TotalPrice;

                        _context.Orders.Update(order);
                        _context.SaveChanges();
                    }
                }
                result["returncode"] = 1;
                result["returnmessage"] = "success";
                Console.WriteLine(result);
                return result;
            }catch (Exception e)
            {
                result["returncode"] = 0;
                result["returnmessage"] = e.Message;
                return result;
            }
        }

        private bool VerifyCallback(string data, string requestMac)
        {
            try
            {
                string mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _config["ZaloPay:Key2"], data);

                return requestMac.Equals(mac);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Payment With VNPay
        public string CreateOrderWithVNPay (OrderCreateModel model)
        {
            if (model.OrderDetails == null) throw new AppException("Don't have products in your cart");
            var order = _mapper.Map<Order>(model);

            order.OrderDate = DateTime.UtcNow.AddHours(7) ;
            order.DeliveryAddress = _context.DeliveryAddresses.AsNoTracking().FirstOrDefault(a => a.Id == order.DeliveryAddressId);
            order.StoreId = order.DeliveryAddress.StoreId;
            order.IsDeposit = false;
            order.ShipperId = null;
            order.StatusId = 6;

            CheckPayingOrder(order.DeliveryAddress.CustomerId);

            order.DeliveryAddress = null;

            _context.Orders.Add(order);
            _context.SaveChanges();

            //Get Config Info
            string vnp_Returnurl = _config["VNPay:vnp_Returnurl"];
            string vnp_Url = _config["VNPay:vnp_Url"];
            string vnp_TmnCode = _config["VNPay:vnp_TmnCode"];
            string vnp_HashSecret = _config["VNPay:vnp_HashSecret"];

            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                throw new AppException("Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config");
            }

            //Build URL for VNPay
            var amount = long.Parse(order.TotalPrice.ToString());

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", order.OrderDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", VNPAY_CS_ASPX.Utils.GetIpAddress(_contextAccessor));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan tai VWater cho don hang: " + order.Id);
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.UtcNow.AddHours(7).AddMinutes(15).ToString("yyyyMMddHHmmss"));

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            order.OrderIdMomo = paymentUrl;

            _context.Orders.Update(order);
            _context.SaveChanges();
            
            return paymentUrl;

        }

        public string VNPayIpn(HttpRequest request)
        {
            string returnContent = string.Empty;
            var data = HttpUtility.ParseQueryString(request.QueryString.Value);
            if(data.Count > 0)
            {
                string vnp_HashSecret = _config["VNPay:vnp_HashSecret"]; //Secret key
                var vnpayData = data;
                VnPayLibrary vnpay = new VnPayLibrary();
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }

                }
                //Lay danh sach tham so tra ve tu VNPAY
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve
                
                int orderId = Convert.ToInt16(vnpay.GetResponseData("vnp_TxnRef"));
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = data["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

                var ipnData = new {
                     orderId = orderId,
                     vnp_Amount = vnp_Amount,
                     vnpayTranId = vnpayTranId,
                     vnp_ResponseCode = vnp_ResponseCode,
                     vnp_TransactionStatus = vnp_TransactionStatus,
                     vnp_SecureHash = vnp_SecureHash,
                     checkSignature = checkSignature
                    };

                if (checkSignature)
                {
                    var order = GetOrderIgnoreInclude(orderId);
                    if(order != null)
                    {
                        if(order.TotalPrice == vnp_Amount)
                        {
                            if(order.StatusId == 6)
                            {
                                if(vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                                {
                                    System.Console.WriteLine("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId,
                                        vnpayTranId);
                                    order.StatusId = 7;
                                    order.AmountPaid = vnp_Amount;
                                    order.IpnData = JsonConvert.SerializeObject(ipnData);                 
                                }
                                else
                                {
                                    System.Console.WriteLine("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}",
                                        orderId,
                                        vnpayTranId, vnp_ResponseCode);
                                    order.StatusId = 5;
                                    order.IpnData = "Thanh toan khong thanh cong";
                                }
                                _context.Orders.Update(order);
                                _context.SaveChanges();

                                returnContent = "{\"RspCode\":\"00\",\"Message\":\"Confirm Success\"}";
                            }
                            else
                            {
                                returnContent = "{\"RspCode\":\"02\",\"Message\":\"Order already confirmed\"}";
                            }
                        }
                        else
                        {
                            returnContent = "{\"RspCode\":\"04\",\"Message\":\"invalid amount\"}";
                        }
                    }
                    else
                    {
                        returnContent = "{\"RspCode\":\"01\",\"Message\":\"Order not found\"}";
                    }
                }
                else
                {
                    returnContent = "{\"RspCode\":\"97\",\"Message\":\"Invalid signature\"}";
                    System.Console.WriteLine("Invalid signature, InputData={0}", request.QueryString.Value);
                }
            }
            else
            {
                returnContent = "{\"RspCode\":\"99\",\"Message\":\"Input data required\"}";
            }

            return returnContent;
        }
        #endregion
        public void Update(int id, OrderUpdateModel model)
        {
            var order = GetOrder(id);
            _mapper.Map(model, order);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = GetOrder(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        private void CheckCreditOfShipper(int shipperId)
        {
            var wallet = _context.Wallets.AsNoTracking().FirstOrDefault(a => a.ShipperId == shipperId);
            if (wallet.Credit > 2000000)
            {
                throw new AppException("Bạn đang có số dư nợ vượt quá số tiền qui định./n"
                    + "Vui lòng nạp thêm vào tài khoản để có thể nhận đơn tiếp.");
            }
        }

        public Order TakeOrder(int order_id, int shipper_id)
        {
            //if(order.ShipperId == null)
            CheckCreditOfShipper(shipper_id);

            var order = GetOrderIgnoreInclude(order_id);
            order.StatusId = 3;
            order.ShipperId = shipper_id;

            _context.Orders.Update(order);
            _context.SaveChanges();

            var responseOrder = GetOrder(order.Id);

            CreateTransactionForOrder(responseOrder);

            responseOrder.Shipper.Orders = null;
            responseOrder.Shipper.Wallet.Transactions = null;
            foreach (var transaction in responseOrder.Transactions)
            {
                transaction.Wallet = null;
            }
            OrderJsonFile(responseOrder);

            return responseOrder;
        }

        private Order GetOrder(int id)
        {
            var order = OrderExtensions.GetByKey(_context.Orders
                .Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.DepositNote)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                , id);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            if (order.DepositNote != null)
                order.DepositNote.Order = null;
            return order;
        }

        public void ConfirmOrder(int order_id)
        {
            var order = GetOrderIgnoreInclude(order_id);

            order.StatusId = 2;
            _context.Orders.Update(order);
            _context.SaveChanges();

            var responseOrder = GetOrder(order_id);
            OrderJsonFile(responseOrder);
            responseOrder.DeliveryAddress.Customer.DeliveryAddresses = null;

            if (responseOrder.StatusId == 2)
            {
                var message = JsonConvert.SerializeObject(responseOrder, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                send.SendMessage(message);
            }
        }

        public void CancelOrder(int order_id)
        {
            var order = GetOrder(order_id);
            if (order.StatusId < 3 || order.StatusId == 6)
            {
                order.StatusId = 5;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            else throw new AppException("Đơn hàng của bạn đã được nhận bởi Shipper. Không thể hủy");
        }

        public void FinishOrder(int order_id)
        {
            var order = GetOrderIgnoreInclude(order_id);

            order.StatusId = 4;
            _context.Orders.Update(order);
            _context.SaveChanges();

        }

        private Order GetOrderIgnoreInclude(int id)
        {
            var order = _context.Orders.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            return order;
        }

        public Order GetLastestOrder(int customer_id)
        {
            var orders = OrderExtensions.ByCustomerId(
                _context.Orders.Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product),
                customer_id);
            var lastestOrder = orders.ToList().MaxBy(a => a.OrderDate);
            foreach (var orderDetail in lastestOrder.OrderDetails)
            {
                orderDetail.Order = null;
                orderDetail.ProductInMenu.Product.ProductInMenus = null;
                orderDetail.ProductInMenu.OrderDetails = null;
            }

            return lastestOrder;
        }

        public List<Order> GetOrderByCustomer(int customer_id)
        {
            /*var orders = _context.Orders.Include(a => a.DeliveryAddress).Include(a => a.OrderDetails).IgnoreAutoIncludes();*/
            var orders = OrderExtensions.ByCustomerId(_context.Orders
                .Include(a => a.DeliveryAddress)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                .ThenInclude(a => a.Product).OrderByDescending(a => a.Id), customer_id);

            foreach(var order in orders)
            {
                OrderJsonFile(order);
            }

            return orders.ToList();
        }

        public List<Order> FollowOrder(int customer_id)
        {
            var orders = OrderExtensions.ByCustomerId(_context.Orders
                            .Include(a => a.DeliveryAddress)
                            .Include(a => a.Store)
                            .Include(a => a.Status)
                            .Include(a => a.DeliverySlot)
                            .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                            .ThenInclude(a => a.Product).OrderByDescending(a=>a.Id), customer_id);
            var list = new List<Order>();
            foreach (var order in orders)
            {
                OrderJsonFile(order);
                if (order.StatusId < 4 ||  order.StatusId == 6 || order.StatusId == 7 ) list.Add(order);
            }
            return list;
        }

        private void OrderJsonFile(Order order) 
        {

            order.DeliveryAddress.Orders = null;
            order.Status.Orders = null;
            order.Store.Orders = null;
            order.Store.DeliveryAddresses = null;
            order.Store.DeliverySlots = null;
            order.DeliverySlot.Orders = null;
            order.DeliverySlot.Store = null;
            order.DepositNote = null;
            order.Status.Orders = null;
            order.Transactions = null;
            foreach (var order1 in order.OrderDetails)
             {
               order1.ProductInMenu.OrderDetails = null;
             }
            
        }
        public Order ReOrder(int order_id)
        {
            var order = GetOrder(order_id);

            order.OrderDate = DateTime.UtcNow.AddHours(7);

            var newOrder = Create(_mapper.Map<OrderCreateModel>(order));

            var responeOrder = _context.Orders
                .Include(a => a.DeliveryAddress)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                .AsNoTracking().FirstOrDefault(a => a.Id == newOrder.Id);

            OrderJsonFile(responeOrder);

            return responeOrder;
        }

        public int GetNumberOfOrder()
        {
            return _context.Orders.Count();
        }

        public DepositNote CreateDepositeNote(DepositNoteCreateModel model) 
        {
            var order = GetOrder(model.OrderId);

            order.IsDeposit = true;

            _context.Orders.Update(order);
            _context.SaveChanges();

            var depositeNote = _mapper.Map<DepositNote>(model);

            int quantityDeposit = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.ProductInMenu.Product.Description == "Bình")
                {
                    quantityDeposit += orderDetail.Quantity;
                }
            }

            if (depositeNote.Quantity > quantityDeposit)
            {
                throw new AppException("Số lượng bình khách cọc không được lớn hơn số lượng bình khách mua.");
            }

            if (quantityDeposit == 0) throw new AppException("Không có sản phẩm loại bình. Không thể tạo phiếu cọc bình");

            _context.DepositNotes.Add(depositeNote);
            _context.SaveChanges();

            return depositeNote;
        }

        private void CreateTransactionForOrder(Order order)
        {
            var shipper = _context.Shippers.Include(a =>a .Wallet).AsNoTracking().FirstOrDefault(a => a.Id == order.ShipperId);

            //TransactionForOrder
            var transactionForOrder = new Transaction();

            transactionForOrder.Date = DateTime.UtcNow.AddHours(7);            
            transactionForOrder.Price = order.TotalPrice - order.AmountPaid;
            transactionForOrder.WalletId = shipper.Wallet.Id;
            transactionForOrder.OrderId = order.Id;
            transactionForOrder.TransactionType_Id = 1;
            transactionForOrder.Note = "Nợ tiền hàng.";

            //TransactionForVoBinh
            int quantityDeposit = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.ProductInMenu.Product.Description == "Bình")
                {
                    quantityDeposit += orderDetail.Quantity;
                }
            }
            decimal priceDeposit = 50000 * quantityDeposit;

            var transactionForVoBinh = new Transaction();

            transactionForVoBinh.Date = DateTime.UtcNow.AddHours(7);
            transactionForVoBinh.Price = priceDeposit;
            transactionForVoBinh.WalletId = shipper.Wallet.Id;
            transactionForVoBinh.OrderId = order.Id;
            transactionForVoBinh.TransactionType_Id = 1;
            transactionForVoBinh.Note = "Nợ tiền vỏ bình với số lượng bình là: " + quantityDeposit;


            _context.Transactions.Add(transactionForOrder);
            _context.Transactions.Add(transactionForVoBinh);
            _context.SaveChanges();

            shipper.Wallet.Credit += transactionForOrder.Price + priceDeposit;
            _context.Wallets.Update(shipper.Wallet);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrderByStore(int store_id)
        {
            var orders = OrderExtensions.ByStoreId(_context.Orders
                .Include(a => a.DeliveryAddress).ThenInclude(a=> a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.DepositNote)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)                
                .ThenInclude(a => a.Product).OrderByDescending(a => a.Id), store_id);

            foreach (var order in orders)
            {
                OrderJsonFile(order);
                order.DeliveryAddress.Customer.DeliveryAddresses = null;
                order.DeliveryAddress.Store = null;
                if (order.DepositNote != null)
                    order.DepositNote.Order = null;
                
            }
            return orders.ToList();
        }

        public Order GetOrderByStatusForShipper(int shipper_id, int status_id)
        {
            var order = GetShipperByStatus(shipper_id, status_id);
            return order;
        }

        private Order GetShipperByStatus(int shipper_id, int status_id)
        {
            var responseOrder = _context.Orders.Include(a => a.Shipper).Include(a=>a.Status)
                .AsNoTracking().FirstOrDefault(a => a.ShipperId == shipper_id && a.StatusId == status_id);
            if (responseOrder == null) throw new KeyNotFoundException("Can not found!");

            responseOrder.Shipper.Orders = null;
            responseOrder.Status.Orders = null;
            //OrderJsonFile(responseOrder);
            return responseOrder;

        }

        public int CountOrderByStatus()
        {
            return _context.Orders.Count(a => a.StatusId == 2);   
        }

        public IEnumerable<Order> GetAllOrderByStatus(int status_id)
        {
            var orders = _context.Orders.Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.DepositNote)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                .ThenInclude(a => a.Product).OrderByDescending(a => a.Id).Where( a => a.StatusId == status_id);

            foreach (var order in orders)
            {
                OrderJsonFile(order);
                if (order.DepositNote != null)
                    order.DepositNote.Order = null;
            }

            return orders.ToList();
        }

        /*
        public Order GetNewOrderByStoreId(int store_id)
        {
            var orders = OrderExtensions.ByStoreId(
                _context.Orders.Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product),
                store_id);
            var newOrderByStoreId = orders.ToList().MaxBy(a => a.OrderDate);
            foreach (var orderDetail in newOrderByStoreId.OrderDetails)
            {
                orderDetail.Order = null;
                orderDetail.ProductInMenu.Product.ProductInMenus = null;
                orderDetail.ProductInMenu.OrderDetails = null;
            }

            return newOrderByStoreId;
        }
        */

        public List<Order> GetNewOrderByStoreId(int store_id)
        {
            var orders = OrderExtensions.ByStoreId(
                _context.Orders.Include(a => a.DeliveryAddress)
                            .Include(a => a.Store)
                            .Include(a => a.Status)
                            .Include(a => a.DeliverySlot)
                            .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                            .ThenInclude(a => a.Product).OrderByDescending(a => a.Id),
                store_id);
            var list = new List<Order>();
            foreach (var order in orders)
            {
                OrderJsonFile(order);
                if (order.StatusId ==2 || order.StatusId == 7) list.Add(order);
            }
            return list;
        }

        public List<Order> GetOrderByShipper(int shipper_id)
        {
            var orders = OrderExtensions.ByShipperId(_context.Orders
                .Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                .Include(a => a.DepositNote)
                .Include(a => a.Transactions)
                .OrderByDescending(a => a.Id), shipper_id);

            foreach (var order in orders)
            {
                OrderJsonFile(order);
            }

            return orders.ToList();
        }

        public IEnumerable<Order> GetOrderOfStoreByStatus(int store_id, int status_id)
        {
            var order = StoreAndStatus(store_id, status_id);
            return order;
        }

        public IEnumerable<Order> GetOrderOfShipperByStatus(int shipper_id, int status_id)
        {
            var order = ShipperAndStatus(shipper_id, status_id);
            return order;
        }

        private IEnumerable<Order> ShipperAndStatus(int shipper_id, int status_id)
        {
            var orders = _context.Orders.Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Shipper)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                .ThenInclude(a => a.Product).OrderByDescending(a => a.Id).Where(a => a.StatusId == status_id && a.ShipperId == shipper_id);

            foreach (var order in orders)
            {
                OrderJsonFile1(order);
            }

            return orders.ToList();
        }

        private void OrderJsonFile1(Order order)
        {
            order.DeliveryAddress.Orders = null;
            order.Status.Orders = null;
            order.DeliverySlot.Orders = null;
            order.DepositNote = null;
            order.Transactions = null;
            foreach (var order1 in order.OrderDetails)
            {
                order1.ProductInMenu.OrderDetails = null;
            }

        }

        private IEnumerable<Order> StoreAndStatus(int store_id, int status_id)
        {
            var orders = _context.Orders.Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.DepositNote)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                .ThenInclude(a => a.Product).OrderByDescending(a => a.Id).Where(a => a.StatusId == status_id && a.StoreId == store_id);

            foreach (var order in orders)
            {
                OrderJsonFile(order);
                if (order.DepositNote != null)
                    order.DepositNote.Order = null;
            }

            return orders.ToList();
        }

        /*
        public List<Order> GetOrderByStatus(int status_id)
        {
            var orders = OrderExtensions.ByStatusId(_context.Orders
                .Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                .Include(a => a.DepositNote)
                .Include(a => a.Transactions)
                .OrderByDescending(a => a.Id), status_id);

            foreach (var order in orders)
            {
                OrderJsonFile(order);
            }

            return orders.ToList();
        }
        */
    }
}
