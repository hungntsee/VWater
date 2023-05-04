using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
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
            //var idList = new int[3, 5, 6];
            return _context.TransactionTypes.Include(a => a.Transactions)
                .Where(a=>a.Id==3).Where(a => a.Id == 5).Where(a => a.Id == 6);   
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

        /*
        public TransactionType GetTransactionTypeForStore()
        {
            //var transactionType1 = GetAll();
            //var transactionType = _context.TransactionTypes.AsNoTracking();
            //var transactionType = _mapper.Map<TransactionType>;
            //var transactionType = new TransactionType();
            //if (transactionType.Id ==3 && transactionType.Id == 5 && transactionType.Id == 6)

            return _context.TransactionTypes.Include(a => a.Transactions);


        }
        */
        /*
        public List<TransactionType> GetTransactionTypeForStore()
        {
            var transactionType1 = new TransactionType();

            var transactionType = 
                _context.TransactionTypes;

            if (transactionType1.Id == 3 && transactionType1.Id == 5 && transactionType1.Id == 6)
                return transactionType.ToList();             
        }
        */
    }
}
