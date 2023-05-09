using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Domain.Models;
using Service.Helpers;
using System.ComponentModel;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Transactions
{
    public interface ITransactionService
    {
        public IEnumerable<Transaction> GetAll();
        public Transaction GetById(int id);
        public Transaction Create(TransactionCreateModel request);
        public void Update(int id, TransactionUpdateModel request);
        public void Delete(int id);
        public void RefundForShipper(TransactionCreateModel request);
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
            return _context.Transactions.Include(a => a.Order).Include(a => a.Wallet).OrderByDescending(a=>a.Id);
        }

        public Transaction GetById(int id)
        {
            var transaction = GetTransaction(id);
            return transaction;
        }

        public Transaction Create(TransactionCreateModel request)
        {
            if(request.TransactionType_Id == 5)
            {
                var transaction1 = CreateTransactionForDepositNote(request);
                return transaction1;
            }
            else
            {
                var transaction = _mapper.Map<Transaction>(request);

                CheckPriceForTransaction(transaction);

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                if(transaction !=null && (transaction.TransactionType_Id != 1 || transaction.TransactionType_Id != 2 || transaction.TransactionType_Id != 6)) {
            
                    var order = _context.Orders.AsNoTracking().FirstOrDefault(a => a.Id == transaction.OrderId);
                    order.StatusId = 8;
                    _context.Orders.Update(order);
                    _context.SaveChanges();

                }
                return transaction;
            }    
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

        public void RefundForShipper(TransactionCreateModel request)
        {
            var transaction = Create(request);
            if (transaction == null) return;
            var wallet = _context.Wallets.Include(a => a.Shipper).AsNoTracking().FirstOrDefault(a => a.Id == request.WalletId);
            wallet.Credit -= transaction.Price;
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
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

        public Transaction CreateTransactionForDepositNote (TransactionCreateModel request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            transaction.Date = DateTime.UtcNow.AddHours(7);

            CheckPriceForTransaction(transaction);

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            if(CheckTransactionForOrder(transaction.OrderId))
            {
                var order = _context.Orders.AsNoTracking().FirstOrDefault(a => a.Id == transaction.OrderId);
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

            return transaction;
        }

        private void CheckPriceForTransaction(Transaction transaction)
        {
            var order = _context.Orders
                .Include(a => a.DepositNote)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a. ProductInMenu)
                .ThenInclude(a => a.Product)
                .AsNoTracking().FirstOrDefault(a => a.Id == transaction.OrderId);

            var numberOfVoBinh = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.ProductInMenu.Product.Description == "Bình") numberOfVoBinh += orderDetail.Quantity;
            }

            if (transaction.TransactionType_Id == 3)
            {
                if (transaction.Price != order.TotalPrice) throw new AppException("Số tiền của giao dịch không đúng với số tiền của đơn hàng!");
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
