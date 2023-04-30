using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.TransactionTypes
{
    public interface ITransactionTypeService
    {
        public IEnumerable<TransactionType> GetAll();
        public TransactionType GetById(int id);
        public void Create(TransactionTypeCreateModel request);
        public void Update(int id, TransactionTypeUpdateModel request);
        public void Delete(int id);
    }
    public class TransactionTypeService : ITransactionTypeService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public TransactionTypeService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<TransactionType> GetAll()
        {
            return _context.TransactionTypes.Include(a => a.Transactions);
        }

        public TransactionType GetById(int id)
        {
            var transactionType = GetTransactionType(id);
            return transactionType;
        }

        public void Create(TransactionTypeCreateModel request)
        {
            var transactionType = _mapper.Map<TransactionType>(request);

            _context.TransactionTypes.AddAsync(transactionType);
            _context.SaveChangesAsync();
        }

        public void Update(int id, TransactionTypeUpdateModel request)
        {
            var transactionType = GetTransactionType(id);

            _mapper.Map(request, transactionType);
            _context.TransactionTypes.Update(transactionType);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var transactionType = GetTransactionType(id);
            _context.TransactionTypes.Remove(transactionType);
            _context.SaveChangesAsync();
        }

        private TransactionType GetTransactionType(int id)
        {
            var transactionType = _context.TransactionTypes
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            transactionType.Transactions = null;
            if (transactionType == null) throw new KeyNotFoundException("TransactionType not found!");
            return transactionType;
        }

    }
}
