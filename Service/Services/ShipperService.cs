using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Helpers;
using System.Collections;
using System.Web;
using VNPAY_CS_ASPX;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Shippers
{
    public interface IShipperService
    {
        public IEnumerable<Shipper> GetAll();
        public Shipper GetById(int id);
        public void Create(ShipperCreateModel request);
        public void Update(int id, ShipperUpdateModel request);
        public void Delete(int id);
        public int GetNumberOfShipper();
        //public void StatusOfShipper(int id, ShipperStatusModel request1);
        public ReportOrderResponseModel GetReportForShipper(int shipper_id);
        public List<Shipper> GetShipperByStoreId(int store_id);
        public Shipper ChangeStatus(int id);
        public Shipper ChangeShipperActivation(int id);
        public string Deposit(ICollection<Order> orders);
        public IEnumerable<Shipper> GetActiveShipper();
        /*public string VNPayIpnForShipper(HttpRequest request);*/
    }
    public class ShipperService : IShipperService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private IHttpContextAccessor _contextAccessor;

        public ShipperService(VWaterContext context, IMapper mapper, IConfiguration config, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _contextAccessor = contextAccessor;
        }
        public IEnumerable<Shipper> GetAll()
        {
            return _context.Shippers.Include(a => a.Orders).Include(a => a.Wallet);
        }

        public IEnumerable<Shipper> GetActiveShipper()
        {
            return _context.Shippers.Include(a => a.Orders).Include(a => a.Wallet).Where(a => a.IsActive == true);
        }

        public Shipper GetById(int id)
        {
            var shipper = GetShipper(id);
            return shipper;
        }

        public void Create(ShipperCreateModel request)
        {
            var shipper = _mapper.Map<Shipper>(request);
            shipper.IsActive= true;

            _context.Shippers.AddAsync(shipper);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ShipperUpdateModel request)
        {
            var shipper = GetShipper(id);

            _mapper.Map(request, shipper);
            _context.Shippers.Update(shipper);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var shipper = GetShipper(id);
            _context.Shippers.Remove(shipper);
            _context.SaveChangesAsync();
        }

        private Shipper GetShipper(int id)
        {
            var shipper = _context.Shippers.Include(a => a.Orders).Include(a => a.Wallet)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (shipper == null) throw new KeyNotFoundException("Shipper not found!");
            return shipper;
        }

        public int GetNumberOfShipper()
        {
            var numberOfShipper = _context.Shippers.Count();

            return numberOfShipper;
        }

        private int GetNumberOfOrderByStatusForShipper(int status_id)
        {
            var ordersByStatusForShipper = OrderExtensions.ByStatusId(_context.Orders, status_id);
            return ordersByStatusForShipper.Count();
        }

        public ReportOrderResponseModel GetReportForShipper(int shipper_id)
        {
            var report = new ReportOrderResponseModel();
            var shipper = GetShipper(shipper_id);
            //var order = new Order();
            //var orders = OrderExtensions.ByShipperId(_context.Orders, shipper_id);

            if (shipper.Id== shipper_id)
            { 
                report.NumberOfFinishOrder = GetNumberOfOrderByStatusForShipper(1);
                report.NumberOfWaitingOrder = GetNumberOfOrderByStatusForShipper(2);
                report.NumberOfConfirmedOrder = GetNumberOfOrderByStatusForShipper(3);
                report.NumberOfShippingOrder = GetNumberOfOrderByStatusForShipper(4);
                report.NumberOfFailOrder = GetNumberOfOrderByStatusForShipper(5);           
                return report;
            }
            else
            {
                throw new AppException("Can not found!!!");
            }

        }

        public void StatusOfShipper(int id, ShipperStatusModel request1)
        {
            var shipper = GetShipper(id);
            _mapper.Map(request1, shipper);

            if (shipper.IsOnline == true) { shipper.IsOnline = false; }
            else { shipper.IsOnline = true; }

            _context.Shippers.Update(shipper);
            _context.SaveChangesAsync();
        }

        public Shipper ChangeStatus(int id)
        {
            var shipper = GetShipper(id);

            if (shipper.IsOnline == true) { shipper.IsOnline = false; }
            else { shipper.IsOnline = true; }
            _context.Shippers.Update(shipper);
            _context.SaveChangesAsync();

            return shipper;
        }

        public Shipper ChangeShipperActivation(int id)
        {
            var shipper = GetShipper(id);

            if (shipper.IsActive == true) { shipper.IsActive = false; }
            else { shipper.IsActive = true; }
            _context.Shippers.Update(shipper);
            _context.SaveChangesAsync();

            return shipper;
        }

        public List<Shipper> GetShipperByStoreId(int store_id)
        {
            var shipper = ShipperExtensions.ByStoreId(
                _context.Shippers.Include(a => a.Account),
                store_id);

            return shipper.ToList();
        }

        private void CheckStatusOfOrder(Order order)
        {
            if (order.StatusId == 8)  throw new AppException("Đơn hàng số " + order.Id + " đã được đóng giao dịch.");

            if (order.StatusId != 4) throw new AppException("Đơn hàng số " + order.Id + " chưa giao hàng thành công hoặc đã bị hủy.");
        }

        public string Deposit(ICollection<Order> orders)
        {
            decimal totalAmount = 0;
            List<int> listId = new List<int>();
            int? shipper_id = 0;
            Shipper shipper = null;
            
            foreach (var order in orders)
            {              
                CheckStatusOfOrder(order);
                shipper_id = order.ShipperId;
                listId.Add(order.Id);
                totalAmount += order.TotalPrice;
            }

            if (shipper_id != null)
            {
               shipper = _context.Shippers.Include(a => a.Wallet).Include(a => a.Orders).AsNoTracking().FirstOrDefault(a => a.Id == shipper_id);
            }

            //Get Config Info
            string vnp_Returnurl = _config["VNPay:vnp_Returnurl_ForShipper"];
            string vnp_Url = _config["VNPay:vnp_Url"];
            string vnp_TmnCode = _config["VNPay:vnp_TmnCode"];
            string vnp_HashSecret = _config["VNPay:vnp_HashSecret"];
            string vnp_TxnRef = string.Join(",", listId);

            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                throw new AppException("Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config");
            }

            //Build URL for VNPay
            var amount = long.Parse(totalAmount.ToString());

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", VNPAY_CS_ASPX.Utils.GetIpAddress(_contextAccessor));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Nạp tiền vào ví dư nợ của:" + shipper.Fullname);
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", vnp_TxnRef);
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.UtcNow.AddHours(7).AddMinutes(15).ToString("yyyyMMddHHmmss"));

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return paymentUrl;
        }

        /*public string VNPayIpnForShipper(HttpRequest request)
        {
            string returnContent = string.Empty;
            var data = HttpUtility.ParseQueryString(request.QueryString.Value);
            if (data.Count > 0)
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

                string orderIds = vnpay.GetResponseData("vnp_TxnRef");
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = data["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);


                //
                var ipnData = new
                {
                    orderIds = orderIds,
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
                    if (order != null)
                    {
                        if (order.TotalPrice == vnp_Amount)
                        {
                            if (order.StatusId == 6)
                            {
                                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
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
        }*/
    }
}
