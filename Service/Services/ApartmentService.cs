using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Apartments
{
    public interface IApartmentService
    {
        public IEnumerable<Apartment> GetAll();
        public Apartment GetById(int id);
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
            return _context.Apartments.Include(a => a.Area).Include(a => a.Buildings);
        }

        public Apartment GetById(int id)
        {
            var apartment = GetApartment(id);
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
            var apartment = _context.Apartments.Include(a => a.Area).Include(a => a.Buildings).AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (apartment == null) throw new KeyNotFoundException("Apartment not found!");
            return apartment;
        }

    }
}
