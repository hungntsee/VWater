using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Domain.Models;
using Service.Helpers;
using System.ComponentModel;
using System.Web;
using VNPAY_CS_ASPX;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Transactions
{
    public interface ITransactionService
    {
        public IEnumerable<Transaction> GetAll();
        public Transaction GetById(int id);
        public ICollection<Transaction> Create(TransactionCreateModel request);
        public void Update(int id, TransactionUpdateModel request);
        public void Delete(int id);
        public string VNPayIpnForShipper(HttpRequest request);
    }
    public class TransactionService : ITransactionService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public TransactionService(VWaterContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public IEnumerable<Transaction> GetAll()
        {
            var transactions = _context.Transactions.Include(a => a.Order).Include(a => a.Wallet).OrderByDescending(a=>a.Id);
            foreach (var transaction in transactions)
            {
                transaction.Order.Transactions = null;
                transaction.Wallet.Transactions = null;
            }
            return _context.Transactions.Include(a => a.Order).Include(a => a.Wallet).OrderByDescending(a => a.Id);
        }

        public Transaction GetById(int id)
        {
            var transaction = GetTransaction(id);
            return transaction;
        }

        public ICollection<Transaction> Create(TransactionCreateModel request)
        {

            if(request.TransactionType_Id == 5) //VoBinh
            {
                var transactions = CreateTransactionForVoBinh(request);
                return transactions;
            }
            else if(request.TransactionType_Id == 3) //Tiền mặt
            {
                var transactions = CreateTransactionForCash(request);            
                return transactions;
            }
            return null;
        }

        public void Update(int id, TransactionUpdateModel request)
        {
            var transaction = GetTransaction(id);

            _mapper.Map(request, transaction);
            _context.Transactions.Update(transaction);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var transaction = GetTransaction(id);
            _context.Transactions.Remove(transaction);
            _context.SaveChangesAsync();
        }

        private Transaction GetTransaction(int id)
        {
            var transaction = _context.Transactions.Include(a => a.Order).Include(a => a.Wallet)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (transaction == null) throw new KeyNotFoundException("Transaction not found!");
            return transaction;
        }
        
        private bool CheckTransactionForOrder(int order_id)
        {
            var order = _context.Orders.Include(a => a.Transactions).ThenInclude(a => a.TransactionType)
                .AsNoTracking().FirstOrDefault(a => a.Id == order_id);
            if (order == null) throw new AppException("Order not found!");

            foreach (var transaction in order.Transactions)
            {
                if (transaction.TransactionType_Id == 3 || transaction.TransactionType_Id == 4)
                {
                    return true;
                }
            }
            return false;

        }

        private void CheckStatusOrder (Order order)
        {
            if (order.StatusId < 4 && order.StatusId > 5 && order.StatusId < 8) throw new AppException("Đơn hàng có mã " + order.Id +  " chưa được giao đến cho khách hàng. Không thể tạo giao dịch hoàn tiền.");
            if (order.StatusId == 5) throw new AppException("Đơn hàng có mã " + order.Id + " đã bị hủy. Không thể tạo giao dịch mới.");
            if (order.StatusId == 8) throw new AppException("Đơn hàng có mã " + order.Id + " đã được hoàn tất và đóng giao dịch. Không thể tạo giao dịch mới.");
        }

        public ICollection<Transaction> CreateTransactionForVoBinh (TransactionCreateModel request)
        {
            var list = new List<Transaction>();
            foreach(var model in request.Orders)
            {
                var order = GetOrder(model.Id);

                if (order.Transactions.Any(a => a.TransactionType_Id == 5)) throw new AppException("Đơn hàng này đã được trả vỏ bình.");
                CheckStatusOrder(order);

                if (GetNumberOfVoBinh(order) == 0) throw new AppException("Đơn hàng " + order.Id + " không có vỏ bình. Không thể tạo giao dịch cho Vỏ bình");
                Transaction transaction = new Transaction();
                transaction.Price = 50000*GetNumberOfVoBinh(order);
                transaction.WalletId = request.WalletId;
                transaction.OrderId = order.Id;
                transaction.Note = "Số lượng vỏ bình mà shipper giao/nộp: " + GetNumberOfVoBinh(order);
                transaction.AccountId = request.Account_Id;
                transaction.TransactionType_Id = 5;
                transaction.Date = DateTime.UtcNow.AddHours(7);

                CheckPriceForTransaction(transaction);

                list.Add(transaction);

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                var wallet = _context.Wallets.AsNoTracking().FirstOrDefault(a => a.Id == transaction.WalletId);
                wallet.Credit -= transaction.Price;
                _context.Update(wallet);
                _context.SaveChanges();

                if (CheckTransactionForOrder(order.Id))
                {                             
                    order.StatusId = 8;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                }

            }         
            return list;
        }

        private void OrderJsonFile(Order order)
        {
            foreach (var order1 in order.OrderDetails)
            {
                order1.ProductInMenu.OrderDetails = null;
            }

        }

        private Order GetOrder(int orderId)
        {
            var order = _context.Orders
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                .Include(a => a.Transactions)
                .Include(a => a.DepositNote)
                .Include(a => a.Shipper)
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == orderId);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            OrderJsonFile(order);
            order.Shipper.Orders = null;
            return order;
        }

        public ICollection<Transaction> CreateTransactionForCash(TransactionCreateModel request)
        {
            var list = new List<Transaction>();
            foreach (var model in request.Orders)
            {
                var order = GetOrder(model.Id);

                if (order.Transactions.Any(a => a.TransactionType_Id == 3 || a.TransactionType_Id == 4 )) throw new AppException("Đơn hàng này đã được thanh toán");

                CheckStatusOrder(order);

                Transaction transaction = new Transaction();

                if (order.AmountPaid == 0) { 
                    if (order.IsDeposit == true)
                    {
                        transaction.Price = order.TotalPrice  + order.DepositNote.Price;
                        transaction.Note = "Bao gồm tiền hàng và tiền cọc bình với số lượng bình là: " + order.DepositNote.Quantity;
                    }
                    else
                    {
                        transaction.Price = order.TotalPrice;
                        transaction.Note = "Thanh toán tiền hàng.";
                    }
                } else if(order.AmountPaid > 0)
                {
                    if (order.IsDeposit == true)
                    {
                        transaction.Price = order.TotalPrice - order.AmountPaid + order.DepositNote.Price;
                        transaction.Note = "Tiền cọc bình với số lượng bình là: " + order.DepositNote.Quantity + "bình.";
                    }
                }
                transaction.WalletId = order.Shipper.Wallet.Id;
                transaction.OrderId = order.Id;               
                transaction.AccountId = request.Account_Id;
                transaction.TransactionType_Id = 3;
                transaction.Date = DateTime.UtcNow.AddHours(7);

                CheckPriceForTransaction(transaction);

                list.Add(transaction);

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                var wallet = _context.Wallets.AsNoTracking().FirstOrDefault(a => a.Id == transaction.WalletId);
                wallet.Credit -= transaction.Price;
                _context.Update(wallet);
                _context.SaveChanges();

                if (order.IsDeposit == true)
                {
                    order.StatusId = 8;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                }
                else if(order.Transactions.Any(a => a.TransactionType_Id == 5))
                {
                     order.StatusId = 8;
                     _context.Orders.Update(order);
                     _context.SaveChanges();
                }
            }
            return list;
        }

        private int GetNumberOfVoBinh(Order order)
        {
            var numberOfVoBinh = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.ProductInMenu.Product.Description == "Bình") numberOfVoBinh += orderDetail.Quantity;
            }

            return numberOfVoBinh;
        }

        private void CheckPriceForTransaction(Transaction transaction)
        {
            var order = _context.Orders
                .Include(a => a.DepositNote)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a. ProductInMenu)
                .ThenInclude(a => a.Product)
                .AsNoTracking().FirstOrDefault(a => a.Id == transaction.OrderId);

            var numberOfVoBinh = GetNumberOfVoBinh(order);

            if (transaction.TransactionType_Id == 3)
            {
                if (transaction.Price != (order.TotalPrice - order.AmountPaid + order.DepositNote.Price)) throw new AppException("Số tiền của giao dịch không đúng với số tiền của đơn hàng!");
            }
            if (transaction.TransactionType_Id == 5)
            {
                if (order.IsDeposit == false)
                {
                    if (transaction.Price != 50000 * numberOfVoBinh) 
                        throw new AppException("Số tiền của giao dịch không đúng với số lượng bình của đơn hàng!");
                }
                else
                {
                    if (transaction.Price != order.DepositNote.Price)
                        throw new AppException("Số tiền của giao dịch không đúng với số tiền trong phiếu cọc bình!");
                }
            }
        }

        public string VNPayIpnForShipper(HttpRequest request)
        {
            string returnContent = string.Empty;
            var data = HttpUtility.ParseQueryString(request.QueryString.Value);
            if (data.Count > 0)
            {
                string vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"]; //Secret key
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

                string vnp_TxnRef = vnpay.GetResponseData("vnp_TxnRef");
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = data["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                string vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

                var ipnData = new
                {
                    vnp_TxnRef = vnp_TxnRef,
                    vnp_Amount = vnp_Amount,
                    vnpayTranId = vnpayTranId,
                    vnp_ResponseCode = vnp_ResponseCode,
                    vnp_TransactionStatus = vnp_TransactionStatus,
                    vnp_SecureHash = vnp_SecureHash,
                    checkSignature = checkSignature
                };

                var list = new List<Transaction>();
                string[] orderIds = vnp_OrderInfo.Split(",");
                /*if () */
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        foreach (string id in orderIds)
                        {
                            int orderId = Convert.ToInt16(id);
                            var order = _context.Orders.Include(a => a.DepositNote).Include(a => a.Shipper).ThenInclude(a => a.Wallet).AsNoTracking().FirstOrDefault(a => a.Id == orderId);

                            if (order.StatusId == 8) throw new AppException("Hóa đơn mã : " + order.Id +" đã được thanh toán.");

                            var transaction = CreateTransactionForOnline(order);
                            if (transaction == null)
                            {
                                returnContent = "{\"RspCode\":\"99\",\"Message\":\"Cannot create Transaction for Orders\"}";
                                System.Console.WriteLine("Không thể tạo giao dịch nạp tiền cho đơn hàng mã: " + orderId);
                            }
                            else list.Add(transaction);
                        }

                        if (list.Count == orderIds.Length)
                        {
                            returnContent = "{\"RspCode\":\"00\",\"Message\":\"Confirm Success\"}";
                        }
                        else returnContent = "{\"RspCode\":\"99\",\"Message\":\"Confirm Fail\"}";
                    }
                    else
                    {
                        System.Console.WriteLine("Thanh toan loi, TxnRef={0}, VNPAY TranId={1},ResponseCode={2}",
                                       vnp_TxnRef, vnpayTranId, vnp_ResponseCode);
                    }
                }
                /*else
                {
                    returnContent = "{\"RspCode\":\"97\",\"Message\":\"Invalid signature\"}";
                    System.Console.WriteLine("Invalid signature, InputData={0}", request.QueryString.Value);
                }*/
            }
            else
            {
                returnContent = "{\"RspCode\":\"99\",\"Message\":\"Input data required\"}";
            }

            return returnContent;
        }

        private Transaction CreateTransactionForOnline(Order order)
        {
            var transaction = new Transaction();
            transaction.OrderId = order.Id;
            transaction.Date = DateTime.UtcNow.AddHours(7);
            transaction.WalletId = order.Shipper.Wallet.Id;

            if (order.AmountPaid == 0)
            {
                if (order.IsDeposit == true && order.DepositNote != null)
                {
                    transaction.Price = order.TotalPrice + order.DepositNote.Price;
                    transaction.Note = "Thanh toán online bao gồm tiền hàng và tiền cọc bình với số lượng bình là: " + order.DepositNote.Quantity;
                }
                else
                {
                    transaction.Price = order.TotalPrice;
                    transaction.Note = "Thanh toán online tiền hàng.";
                }
            }
            else if (order.AmountPaid > 0)
            {
                if (order.IsDeposit == true)
                {
                    transaction.Price = order.TotalPrice - order.AmountPaid + order.DepositNote.Price;
                    transaction.Note = "Thanh toán online tiền cọc bình với số lượng bình là: " + order.DepositNote.Quantity + "bình.";
                }
            }
            
            transaction.AccountId = null;
            transaction.TransactionType_Id = 4;

            CheckPriceForTransaction(transaction);

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            /*var wallet = _context.Wallets.AsNoTracking().FirstOrDefault(a => a.Id == transaction.WalletId);*/

            order.Shipper.Wallet.Credit -= transaction.Price;            

            if (order.IsDeposit == true)
            {
                order.StatusId = 8;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            else if (order.Transactions.Any(a => a.TransactionType_Id == 5))
            {
                order.StatusId = 8;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }

            return transaction;
        }
    }
}
