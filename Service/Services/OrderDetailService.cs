using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IOrderDetailService
    {
        public IEnumerable<OrderDetail> GetAll();
        public OrderDetail GetById(int id);
        public void Create(OrderDetailCreateModel model);
        public void Update(int id, OrderDetailUpdateModel model);
        public void Delete(int id);
    }
    public class OrderDetailService : IOrderDetailService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public OrderDetailService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetails.Include(a => a.Order);
        }

        public OrderDetail GetById(int id)
        {
            var orderDetail = GetOrderDetail(id);
            return orderDetail;
        }

        public void Create(OrderDetailCreateModel model)
        {
            var orderDetail = _mapper.Map<OrderDetail>(model);

            _context.OrderDetails.AddAsync(orderDetail);
            _context.SaveChangesAsync();
        }

        public void Update(int id, OrderDetailUpdateModel model)
        {
            var orderDetail = GetOrderDetail(id);
            _mapper.Map(model, orderDetail);
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var orderDetail = GetOrderDetail(id);
            _context.OrderDetails.Remove(orderDetail);
            _context.SaveChangesAsync();
        }

        private OrderDetail GetOrderDetail(int id)
        {
            var orderDetail = _context.OrderDetails.Include(a => a.Order).AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (orderDetail == null) throw new KeyNotFoundException("OrderDetail not found!");
            return orderDetail;
        }

    }
}
