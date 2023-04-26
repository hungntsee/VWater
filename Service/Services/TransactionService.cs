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
            var transaction = _mapper.Map<Transaction>(request);

            _context.Transactions.AddAsync(transaction);
            _context.SaveChangesAsync();

            return transaction;
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

    }
}
