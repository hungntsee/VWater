using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ;
using Service.Helpers;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;
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
        public ReportOrderResponseModel GetReport();
        public DepositNote CreateDepositeNote(DepositNoteCreateModel model);
        public void CancelOrder(int order_id);
        public void ConfirmOrder(int order_id);
        public void FinishOrder(int order_id);
        public List<Order> GetOrderByStore(int store_id);
        public Order GetOrderByStatusForShipper(int shipper_id, int status_id);
        public int CountOrderByStatus();
        public Order GetNewOrderByStoreId(int store_id);
        public Task<Dictionary<string, object>> CreateOrderWithZaloPay(OrderCreateModel model);
        public void GetResponeseFromMomo(ResponseFromMomo response);
    }
    public class OrderService : IOrderService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private Send send = new Send();
        private IConfiguration _config;

        public OrderService(VWaterContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _config = configuration;
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

        public Order Create(OrderCreateModel model)
        {
            if (model.OrderDetails == null) throw new AppException("Don't have products in your cart");
            var order = _mapper.Map<Order>(model);
            order.OrderDate = DateTime.Now;
            order.DeliveryAddress = _context.DeliveryAddresses.AsNoTracking().FirstOrDefault(a => a.Id == order.DeliveryAddressId);
            order.StoreId = order.DeliveryAddress.StoreId;
            order.IsDeposit = false;           
            order.ShipperId = null;
            order.AmountPaid = 0;

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

        #region Payment With Momo
        public async Task<Dictionary<string,object>> CreateOrderWithZaloPay(OrderCreateModel model)
        {
            if (model.OrderDetails == null) throw new AppException("Don't have products in your cart");
            var order = _mapper.Map<Order>(model);
            order.OrderDate = DateTime.Now;
            order.DeliveryAddress = _context.DeliveryAddresses.AsNoTracking().FirstOrDefault(a => a.Id == order.DeliveryAddressId);
            order.StoreId = order.DeliveryAddress.StoreId;
            order.IsDeposit = false;
            order.ShipperId = null;

            order.DeliveryAddress = null;

            Random rnd = new Random();
            var app_trans_id = rnd.Next(1000000);

            var embed_data = new
            {
                redirect = "https://vwater-user-ui.vercel.app/order-tracking"
            };
            var param = new Dictionary<string, string>();
            ZaloPayCreateRequest request = new ZaloPayCreateRequest();

            request.app_id = _config["ZaloPay:Appid"];
            request.app_trans_id = DateTime.Now.ToString("yyMMdd")+ "_"+ app_trans_id;
            request.app_time = Utils.GetTimeStamp().ToString();
            request.amount = (order.TotalPrice*100).ToString();
            request.embed_data = JsonConvert.SerializeObject(embed_data);
            request.callback_url = "";
            request.mac = GenerateMac(request);

            param.Add("app_id",request.app_id);
            param.Add("app_user", request.app_user);
            param.Add("app_time", request.app_time);
            param.Add("amount", request.amount);
            param.Add("app_trans_id",request.app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embed_data", request.embed_data);
            param.Add("item", request.item);
            param.Add("description", request.desciption);
            param.Add("bank_code", request.bank_code);

            string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";

            var result = await HttpHelper.PostFormAsync(create_order_url, param);


            return result;
        }

        private string GenerateMac(ZaloPayCreateRequest request)
        {
            var key1 = _config["ZaloPay:Key1"];
            var key2 = _config["ZaloPay:Key2"];
            var data =
                request.app_id +
                "|" + request.app_trans_id +
                "|" + request.app_user +
                "|" + request.amount +
                "|" + request.app_time +
                "|" + request.embed_data +
                "|" + request.item;

            var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data);
            return mac;
        }

        public void GetResponeseFromMomo(ResponseFromMomo response)
        {
            if (response.ResultCode == 0)
            {
                var orderMomoId = response.OrderId;
                var order = _context.Orders.AsNoTracking().FirstOrDefault(t => t.OrderIdMomo == orderMomoId);

                if (order == null) throw new AppException("Order is not exist for this request.!");
                if (order.StatusId == 7) throw new AppException("This Order already paid.!");
                if (order.TotalPrice != response.Amount) throw new AppException("Wrong Amount.!");

                order.StatusId = 7;
                order.AmountPaid = response.Amount;
                order.IpnData = JsonConvert.SerializeObject(response);

                _context.Orders.Update(order);
                _context.SaveChanges();

            }
            else if (response.ResultCode != 0) throw new AppException("Transaction Fail.");
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

        public Order TakeOrder(int order_id, int shipper_id)
        {
            //if(order.ShipperId == null)
            var order = GetOrderIgnoreInclude(order_id);
            order.StatusId = 3;
            order.ShipperId = shipper_id;

            _context.Orders.Update(order);
            _context.SaveChanges();

            var responseOrder = GetOrder(order.Id);

            CreateTransactionForOrder(responseOrder);

            responseOrder.Shipper.Orders = null;
            responseOrder.Shipper.Wallets.Transactions = null;
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
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                , id);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            
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
            if (order.StatusId < 3)
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
                .ThenInclude(a => a.Product), customer_id);

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
                            .ThenInclude(a => a.Product), customer_id);
            var list = new List<Order>();
            foreach (var order in orders)
            {
                OrderJsonFile(order);
                if (order.StatusId < 4) list.Add(order);
            }
            return list;
        }

        private void OrderJsonFile(Order order) 
        {

            order.DeliveryAddress.Orders = null;
            order.Status.Orders = null;
            order.Status.PurchaseOrders = null;
            order.Store.Orders = null;
            order.Store.DeliveryAddresses = null;
            order.Store.DeliverySlots = null;
            order.Store.PurchaseOrders = null;
            order.Store.Warehouses = null;
            order.DeliverySlot.Orders = null;
            order.DeliverySlot.Store = null;
            order.GoodsExchangeNotes = null;
            order.DepositNote = null;
            order.Status.Orders = null;
            order.Transactions = null;
            foreach (var order1 in order.OrderDetails)
             {
               order1.ProductInMenu.OrderDetails = null;
               order1.ProductInMenu.Product.ProductInMenus = null;
               order1.ProductInMenu.Product.GoodsInProducts = null;
             }
            
        }
        public Order ReOrder(int order_id)
        {
            var order = GetOrder(order_id);

            order.OrderDate = DateTime.Now;

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

        private int GetNumberOfOrderByStatus(int status_id)
        {
            var ordersByStatus = OrderExtensions.ByStatusId(_context.Orders, status_id);
            return ordersByStatus.Count();
        }

        public ReportOrderResponseModel GetReport()
        {
            var report = new ReportOrderResponseModel();
            report.NumberOfFinishOrder = GetNumberOfOrderByStatus(1);
            report.NumberOfWaitingOrder = GetNumberOfOrderByStatus(2);
            report.NumberOfConfirmedOrder = GetNumberOfOrderByStatus(3);
            report.NumberOfShippingOrder = GetNumberOfOrderByStatus(4);
            report.NumberOfFailOrder = GetNumberOfOrderByStatus(5);

            return report;
        }

        public DepositNote CreateDepositeNote(DepositNoteCreateModel model) 
        {
            var order = GetOrder(model.OrderId);

            order.IsDeposit = true;

            var depositeNote = _mapper.Map<DepositNote>(model);

            if(depositeNote.IsDeposit == true)
            {
                order.TotalPrice += depositeNote.Price;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }

            _context.DepositNotes.Add(depositeNote);
            _context.SaveChanges();

            return depositeNote;
        }

        private void CreateTransactionForOrder(Order order)
        {
            var shipper = _context.Shippers.Include(a =>a .Wallets).AsNoTracking().FirstOrDefault(a => a.Id == order.ShipperId);
            var transaction = new Transaction();

            transaction.Date = DateTime.Now;

            int quantityDeposit = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.ProductInMenu.Product.Description == "Bình")
                {
                    quantityDeposit+= orderDetail.Quantity;
                }
            }

            var priceDeposit = 15000 * quantityDeposit;
            transaction.Price = order.TotalPrice + priceDeposit;
            transaction.WalletId = shipper.Wallets.Id;
            transaction.OrderId = order.Id;
            transaction.Note = "Take Order";

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            shipper.Wallets.Credit += transaction.Price;
            _context.Wallets.Update(shipper.Wallets);
            _context.SaveChanges();
        }

        public List<Order> GetOrderByStore(int store_id)
        {
            var orders = OrderExtensions.ByStoreId(_context.Orders
                .Include(a => a.DeliveryAddress).ThenInclude(a=> a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                .ThenInclude(a => a.Product), store_id);

            foreach (var order in orders)
            {
                OrderJsonFile(order);
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

    }
}
