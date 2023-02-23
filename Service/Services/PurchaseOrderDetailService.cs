using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IPurchaseOrderDetailService
    {
        public IEnumerable<PurchaseOrderDetail> GetAll();
        public PurchaseOrderDetail GetById(int id);
        public void Create(PurchaseOrderDetailCreateModel model);
        public void Update(int id, PurchaseOrderDetailUpdateModel model);
        public void Delete(int id);
    }
    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public PurchaseOrderDetailService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<PurchaseOrderDetail> GetAll()
        {
            return _context.PurchaseOrderDetails.Include(a => a.Goods).Include(a => a.PurchaseOrder);
        }

        public PurchaseOrderDetail GetById(int id)
        {
            var purchaseOrderDetailResponse = GetPurchaseOrderDetail(id);
            return purchaseOrderDetailResponse;
        }

        public void Create(PurchaseOrderDetailCreateModel model)
        {
            var purchaseOrderDetail = _mapper.Map<PurchaseOrderDetail>(model);
            _context.PurchaseOrderDetails.AddAsync(purchaseOrderDetail);
            _context.SaveChangesAsync();
        }

        public void Update(int id, PurchaseOrderDetailUpdateModel model)
        {
            var purchaseOrderDetail = GetPurchaseOrderDetail(id);
            _mapper.Map(model, purchaseOrderDetail);
            _context.PurchaseOrderDetails.Update(purchaseOrderDetail);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var purchaseOrderDetail = GetPurchaseOrderDetail(id);
            _context.PurchaseOrderDetails.Remove(purchaseOrderDetail);
            _context.SaveChangesAsync();
        }

        private PurchaseOrderDetail GetPurchaseOrderDetail(int id)
        {
            var purchaseOrderDetail = _context.PurchaseOrderDetails.Include(a => a.Goods).Include(a => a.PurchaseOrder)
                .AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (purchaseOrderDetail == null) throw new KeyNotFoundException("PurchaseOrderDetail not found!");
            return purchaseOrderDetail;
        }
    }
}
