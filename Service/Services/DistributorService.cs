using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Distributors
{
    public interface IDistributorService
    {
        public IEnumerable<Distributor> GetAll();
        public DistributorReadModel GetById(int id);
        public void Create(DistributorCreateModel request);
        public void Update(int id, DistributorUpdateModel request);
        public void Delete(int id);
    }
    public class DistributorService : IDistributorService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public DistributorService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Distributor> GetAll()
        {
            return _context.Distributors;
        }

        public DistributorReadModel GetById(int id)
        {
            var distributor = _mapper.Map<DistributorReadModel>(GetDistributor(id));
            return distributor;
        }

        public void Create(DistributorCreateModel request)
        {
            /*
            if (_context.Distributors.Any(d => d.DistributorName == request.DistributorName))
                throw new AppException("Distributor: '" + request.DistributorName + "' already exists");
            */
            var distributor = _mapper.Map<Distributor>(request);

            _context.Distributors.AddAsync(distributor);
            _context.SaveChangesAsync();
        }

        public void Update(int id, DistributorUpdateModel request)
        {
            var distributor = GetDistributor(id);

            /*
            if (_context.Distributors.Any(d => d.DistributorName == request.DistributorName))
                throw new AppException("Distributor: '" + request.DistributorName + "' already exists");
            */
            _mapper.Map(request, distributor);
            _context.Distributors.Update(distributor);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var distributor = GetDistributor(id);
            _context.Distributors.Remove(distributor);
            _context.SaveChangesAsync();
        }

        private Distributor GetDistributor(int id)
        {
            var distributor = _context.Distributors.Find(id);
            if (distributor == null) throw new KeyNotFoundException("Distributor not found!");
            return distributor;
        }

    }
}
