using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Quotations
{
    public interface IQuotationService
    {
        public IEnumerable<Quotation> GetAll();
        public QuotationReadModel GetById(int id);
        public void Create(QuotationCreateModel request);
        public void Update(int id, QuotationUpdateModel request);
        public void Delete(int id);
    }
    public class QuotationService : IQuotationService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public QuotationService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Quotation> GetAll()
        {
            return _context.Quotations;
        }

        public QuotationReadModel GetById(int id)
        {
            var quotation = _mapper.Map<QuotationReadModel>(GetQuotation(id));
            return quotation;
        }

        public void Create(QuotationCreateModel request)
        {
            var quotation = _mapper.Map<Quotation>(request);

            _context.Quotations.AddAsync(quotation);
            _context.SaveChangesAsync();
        }

        public void Update(int id, QuotationUpdateModel request)
        {
            var quotation = GetQuotation(id);
            _mapper.Map(request, quotation);
            _context.Quotations.Update(quotation);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var quotation = GetQuotation(id);
            _context.Quotations.Remove(quotation);
            _context.SaveChangesAsync();
        }

        private Quotation GetQuotation(int id)
        {
            var quotation = _context.Quotations.Find(id);
            if (quotation == null) throw new KeyNotFoundException("Quotation not found!");
            return quotation;
        }

    }
}
