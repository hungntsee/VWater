using AutoMapper;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.DeliverySlots
{
    public interface IDeliverySlotService
    {
        public IEnumerable<DeliverySlot> GetAll();
        public DeliverySlotReadModel GetById(int id);
        public void Create(DeliverySlotCreateModel request);
        public void Update(int id, DeliverySlotUpdateModel request);
        public void Delete(int id);
    }
    public class DeliverySlotService : IDeliverySlotService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public DeliverySlotService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DeliverySlot> GetAll()
        {
            return _context.DeliverySlots;
        }

        public DeliverySlotReadModel GetById(int id)
        {
            var deliverySlot = _mapper.Map<DeliverySlotReadModel>(GetDeliverySlot(id));
            return deliverySlot;
        }

        public void Create(DeliverySlotCreateModel request)
        {
            if (_context.DeliverySlots.Any(s => s.SlotName == request.SlotName))
                throw new AppException("DeliverySlot: '" + request.SlotName + "' already exists");
            var deliverySlot = _mapper.Map<DeliverySlot>(request);

            _context.DeliverySlots.AddAsync(deliverySlot);
            _context.SaveChangesAsync();
        }

        public void Update(int id, DeliverySlotUpdateModel request)
        {
            var deliverySlot = GetDeliverySlot(id);

            if (_context.DeliverySlots.Any(s => s.SlotName == request.SlotName))
                throw new AppException("DeliverySlot: '" + request.SlotName + "' already exists");
            _mapper.Map(request, deliverySlot);
            _context.DeliverySlots.Update(deliverySlot);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var deliverySlot = GetDeliverySlot(id);
            _context.DeliverySlots.Remove(deliverySlot);
            _context.SaveChangesAsync();
        }

        private DeliverySlot GetDeliverySlot(int id)
        {
            var deliverySlot = _context.DeliverySlots.Find(id);
            if (deliverySlot == null) throw new KeyNotFoundException("DeliverySlot not found!");
            return deliverySlot;
        }

    }
}
