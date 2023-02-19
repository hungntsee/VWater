using AutoMapper;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.DeliveryTypes
{
    public interface IDeliveryTypeService
    {
        public IEnumerable<DeliveryType> GetAll();
        public DeliveryTypeReadModel GetById(int id);
        public void Create(DeliveryTypeCreateModel request);
        public void Update(int id, DeliveryTypeUpdateModel request);
        public void Delete(int id);
    }
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public DeliveryTypeService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DeliveryType> GetAll()
        {
            return _context.DeliveryTypes;
        }

        public DeliveryTypeReadModel GetById(int id)
        {
            var deliveryType = _mapper.Map<DeliveryTypeReadModel>(GetDeliveryType(id));
            return deliveryType;
        }

        public void Create(DeliveryTypeCreateModel request)
        {
            if (_context.DeliveryTypes.Any(t => t.TypeName == request.TypeName))
                throw new AppException("DeliveryType: '" + request.TypeName + "' already exists");
            var deliveryType = _mapper.Map<DeliveryType>(request);

            _context.DeliveryTypes.AddAsync(deliveryType);
            _context.SaveChangesAsync();
        }

        public void Update(int id, DeliveryTypeUpdateModel request)
        {
            var deliveryType = GetDeliveryType(id);

            if (_context.DeliveryTypes.Any(t => t.TypeName == request.TypeName))
                throw new AppException("DeliveryType: '" + request.TypeName + "' already exists");
            _mapper.Map(request, deliveryType);
            _context.DeliveryTypes.Update(deliveryType);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var deliveryType = GetDeliveryType(id);
            _context.DeliveryTypes.Remove(deliveryType);
            _context.SaveChangesAsync();
        }

        private DeliveryType GetDeliveryType(int id)
        {
            var deliveryType = _context.DeliveryTypes.Find(id);
            if (deliveryType == null) throw new KeyNotFoundException("DeliveryType not found!");
            return deliveryType;
        }

    }
}
