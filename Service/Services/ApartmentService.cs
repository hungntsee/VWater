using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Apartments
{
    public interface IApartmentService
    {
        public IEnumerable<Apartment> GetAll();
        public ApartmentReadModel GetById(int id);
        public void Create(ApartmentCreateModel request);
        public void Update(int id, ApartmentUpdateModel request);
        public void Delete(int id);
    }
    public class ApartmentService : IApartmentService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public ApartmentService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Apartment> GetAll()
        {
            return _context.Apartments;
        }

        public ApartmentReadModel GetById(int id)
        {
            var apartment = _mapper.Map<ApartmentReadModel>(GetApartment(id));
            return apartment;
        }

        public void Create(ApartmentCreateModel request)
        {
            var apartment = _mapper.Map<Apartment>(request);

            _context.Apartments.AddAsync(apartment);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ApartmentUpdateModel request)
        {
            var apartment = GetApartment(id);

            _mapper.Map(request, apartment);
            _context.Apartments.Update(apartment);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var apartment = GetApartment(id);
            _context.Apartments.Remove(apartment);
            _context.SaveChangesAsync();
        }

        private Apartment GetApartment(int id)
        {
            var apartment = _context.Apartments.Find(id);
            if (apartment == null) throw new KeyNotFoundException("Apartment not found!");
            return apartment;
        }

    }
}
