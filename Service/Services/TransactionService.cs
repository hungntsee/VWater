using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Domain.Models;
using Service.Helpers;
using System.ComponentModel;
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
    }
    public class TransactionService : ITransactionService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public TransactionService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

                CheckStatusOrder(order);

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

                if (CheckTransactionForOrder(order.Id))
                {                   
                    order.StatusId = 8;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                }

                if (transaction.TransactionType_Id > 2)
                {
                    var wallet = _context.Wallets.AsNoTracking().FirstOrDefault(a => a.Id == transaction.WalletId);
                    wallet.Credit -= transaction.Price;
                    _context.Update(wallet);
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

        private Order GetOrder(int id)
        {
            var order = _context.Orders
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                .Include(a => a.Transactions)
                .Include(a => a.DepositNote)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            OrderJsonFile(order);
            return order;
        }

        public ICollection<Transaction> CreateTransactionForCash(TransactionCreateModel request)
        {
            var list = new List<Transaction>();
            foreach (var model in request.Orders)
            {
                var order = GetOrder(model.Id);

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
                transaction.WalletId = request.WalletId;
                transaction.OrderId = order.Id;               
                transaction.AccountId = request.Account_Id;
                transaction.TransactionType_Id = 3;
                transaction.Date = DateTime.UtcNow.AddHours(7);

                CheckPriceForTransaction(transaction);

                list.Add(transaction);

                _context.Transactions.Add(transaction);
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

                if (transaction.TransactionType_Id > 2 && transaction.TransactionType_Id < 6)
                {
                    var wallet = _context.Wallets.AsNoTracking().FirstOrDefault(a => a.Id == transaction.WalletId);
                    wallet.Credit -= transaction.Price;
                    _context.Update(wallet);
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
    }
}
