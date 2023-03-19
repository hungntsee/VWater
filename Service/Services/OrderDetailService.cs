using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IOrderDetailService
    {
        public IEnumerable<OrderDetail> GetAll();
        public OrderDetail GetById(int id);
        public OrderDetail Create(OrderDetailCreateModel model);
        public void Update(int id, OrderDetailUpdateModel model);
        public void Delete(int id);
        public List<OrderDetail> ReOrderDetail(int order_id, int newOrder_id);
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
            var orderDetails = _context.OrderDetails.Include(a => a.ProductInMenu);
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.ProductInMenu.OrderDetails = null;
            }
            return orderDetails;
        }

        public OrderDetail GetById(int id)
        {
            var orderDetail = GetOrderDetail(id);
            return orderDetail;
        }

        public OrderDetail Create(OrderDetailCreateModel model)
        {
            var orderDetail = _mapper.Map<OrderDetail>(model);

            _context.OrderDetails.AddAsync(orderDetail);
            _context.SaveChangesAsync();

            return orderDetail;
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

        public List<OrderDetail> ReOrderDetail(int order_id, int newOrder_id)
        {
            var list = new List<OrderDetail>();
            var orderDetails = OrderDetailExtensions.ByOrderId(_context.OrderDetails, order_id);
            foreach ( var orderDetail in orderDetails)
            {
                orderDetail.OrderId = newOrder_id;
                list.Add(Create(_mapper.Map<OrderDetailCreateModel>(orderDetail)));
            }
            return list;
        }

    }
}
