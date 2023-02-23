using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IPurchaseOrderService
    {
        public IEnumerable<PurchaseOrder> GetAll();
        public PurchaseOrder GetById(int id);
        public void Create(PurchaseOrderCreateModel model);
        public void Update(int id, PurchaseOrderUpdateModel model);
        public void Delete(int id);
    }
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public PurchaseOrderService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _context.PurchaseOrders.Include(a => a.PurchaseOrderDetails)
                .Include(a => a.Store)
                .Include(a => a.Distributor);
        }

        public PurchaseOrder GetById(int id)
        {
            var purchaseOrderResponse = GetPurchaseOrder(id);
            return purchaseOrderResponse;
        }

        public void Create(PurchaseOrderCreateModel model)
        {
            var purchaseOrder = _mapper.Map<PurchaseOrder>(model);
            _context.PurchaseOrders.AddAsync(purchaseOrder);
            _context.SaveChangesAsync();
        }

        public void Update(int id, PurchaseOrderUpdateModel model)
        {
            var purchaseOrder = GetPurchaseOrder(id);
            _mapper.Map(model, purchaseOrder);
            _context.PurchaseOrders.Update(purchaseOrder);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var purchaseOrder = GetPurchaseOrder(id);
            _context.PurchaseOrders.Remove(purchaseOrder);
            _context.SaveChangesAsync();
        }

        private PurchaseOrder GetPurchaseOrder(int id)
        {
            var purchaseOrder = _context.PurchaseOrders.Include(a => a.PurchaseOrderDetails)
                .Include(a => a.Store)
                .Include(a => a.Distributor)
                .AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (purchaseOrder == null) throw new KeyNotFoundException("PurchaseOrder not found!");
            return purchaseOrder;
        }
    }
}
