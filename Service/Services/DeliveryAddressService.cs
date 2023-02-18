using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.DeliveryAddresses
{
    public interface IDeliveryAddressService
    {
        public IEnumerable<DeliveryAddress> GetAll();
        public DeliveryAddressReadModel GetById(int id);
        public void Create(DeliveryAddressCreateModel request);
        public void Update(int id, DeliveryAddressUpdateModel request);
        public void Delete(int id);
    }
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public DeliveryAddressService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<DeliveryAddress> GetAll()
        {
            return _context.DeliveryAddresses;
        }

        public DeliveryAddressReadModel GetById(int id)
        {
            var deliveryAddress = _mapper.Map<DeliveryAddressReadModel>(GetDeliveryAddress(id));
            return deliveryAddress;
        }

        public void Create(DeliveryAddressCreateModel request)
        {
            var deliveryAddress = _mapper.Map<DeliveryAddress>(request);

            _context.DeliveryAddresses.AddAsync(deliveryAddress);
            _context.SaveChangesAsync();
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
            var deliveryAddress = _context.DeliveryAddresses.Find(id);
            if (deliveryAddress == null) throw new KeyNotFoundException("DeliveryAddress not found!");
            return deliveryAddress;
        }

    }
}
