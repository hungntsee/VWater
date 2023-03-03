using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.DeliveryAddresses
{
    public interface IDeliveryAddressService
    {
        public IEnumerable<DeliveryAddress> GetAll();
        public DeliveryAddress GetById(int id);
        public DeliveryAddress Create(DeliveryAddressCreateModel request);
        public void Update(int id, DeliveryAddressUpdateModel request);
        public void Delete(int id);
    }
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public DeliveryAddressService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DeliveryAddress> GetAll()
        {
            return _context.DeliveryAddresses.Include(a => a.Customer).Include(a => a.Building).Include(a => a.Orders).Include(a => a.DeliveryType);
        }

        public DeliveryAddress GetById(int id)
        {
            var deliveryAddress = GetDeliveryAddress(id);
            return deliveryAddress;
        }

        public DeliveryAddress Create(DeliveryAddressCreateModel request)
        {
            var deliveryAddress = _mapper.Map<DeliveryAddress>(request);

            _context.DeliveryAddresses.AddAsync(deliveryAddress);
            _context.SaveChangesAsync();

            return deliveryAddress;
        }

        public void Update(int id, DeliveryAddressUpdateModel request)
        {
            var deliveryAddress = GetDeliveryAddress(id);

            _mapper.Map(request, deliveryAddress);
            _context.DeliveryAddresses.Update(deliveryAddress);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var deliveryAddress = GetDeliveryAddress(id);
            _context.DeliveryAddresses.Remove(deliveryAddress);
            _context.SaveChangesAsync();
        }

        private DeliveryAddress GetDeliveryAddress(int id)
        {
            var deliveryAddress = _context.DeliveryAddresses.Include(a => a.Customer).Include(a => a.Building).Include(a => a.Orders).Include(a => a.DeliveryType)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (deliveryAddress == null) throw new KeyNotFoundException("DeliveryAddress not found!");
            return deliveryAddress;
        }

    }
}
