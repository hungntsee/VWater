using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetAll();
        public Order GetById(int id);
        public Order Create(OrderCreateModel model);
        public void Update(int id, OrderUpdateModel model);
        public void Delete(int id);
        public void ConfirmOrder(int id);
        public Order GetLastestOrder(int customer_id);
        public List<Order> GetOrderByCustomer(int customer_id);
    }
    public class OrderService : IOrderService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public OrderService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(a => a.DeliveryAddress).Include(a => a.DeliverySlot).Include(a => a.OrderDetails);
        }

        public Order GetById(int id)
        {
            var orderResponse = GetOrder(id);
            return orderResponse;
        }

        public Order Create(OrderCreateModel model)
        {
            var order = _mapper.Map<Order>(model);
            order.OrderDate = DateTime.Now;

            /*
            if (order.TotalPrice < 1000000) order.Status = 2;
            else order.Status = 1;
            */
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public void Update(int id, OrderUpdateModel model)
        {
            var order = GetOrder(id);
            _mapper.Map(model, order);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = GetOrder(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void ConfirmOrder(int id)
        {
            var order = GetOrder(id);
            //order.Status = 2;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        private Order GetOrder(int id)
        {
            var order = _context.Orders.Include(a => a.DeliveryAddress).Include(a => a.DeliverySlot).Include(a => a.OrderDetails)
                .AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            return order;
        }

        private Order GetOrderIgnoreInclude(int id)
        {
            var order = _context.Orders.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (order == null) throw new KeyNotFoundException("Order not found!");
            return order;
        }

        public Order GetLastestOrder(int customer_id)
        {
            var orders = OrderExtensions.ByCustomerId(_context.Orders.Include(a => a.OrderDetails), customer_id);

            return orders.ToList().MaxBy(a => a.OrderDate);
        }

        public List<Order> GetOrderByCustomer(int customer_id) 
        {

            /*var orders = _context.Orders.Include(a => a.DeliveryAddress).Include(a => a.OrderDetails).IgnoreAutoIncludes();*/
            var orders = OrderExtensions.ByCustomerId(_context.Orders.Include(a=>a.OrderDetails), customer_id);

            return orders.ToList();
        }

    }
}
