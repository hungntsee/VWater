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
        public Order TakeOrder(int id, int shipper_id);
        public Order GetLastestOrder(int customer_id);
        public List<Order> GetOrderByCustomer(int customer_id);
        public List<Order> FollowOrder(int customer_id);
        public Order ReOrder(int order_id);

    }
    public class OrderService : IOrderService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private IOrderDetailService _detailService;

        public OrderService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(a => a.OrderDetails);
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
            order.StatusId = 2;
            order.StoreId = 1;

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

        public Order TakeOrder(int id, int shipper_id)
        {
            var order = GetOrder(id);
            order.StatusId = 2;
            order.ShipperId = shipper_id;

            _context.Orders.Update(order);
            _context.SaveChanges();

            return order;
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
            var orders = OrderExtensions.ByCustomerId(
                _context.Orders.Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product),
                customer_id);
            var lastestOrder = orders.ToList().MaxBy(a => a.OrderDate);
            foreach (var orderDetail in lastestOrder.OrderDetails)
            {
                orderDetail.Order = null;
                orderDetail.ProductInMenu.Product.ProductInMenus = null;
                orderDetail.ProductInMenu.OrderDetails = null;
            }

            return lastestOrder;
        }

        public List<Order> GetOrderByCustomer(int customer_id) 
        {

            /*var orders = _context.Orders.Include(a => a.DeliveryAddress).Include(a => a.OrderDetails).IgnoreAutoIncludes();*/
            var orders = OrderExtensions.ByCustomerId(_context.Orders.Include(a=>a.OrderDetails), customer_id);

            return orders.ToList();
        }

        public List<Order> FollowOrder(int customer_id)
        {
            var orders = OrderExtensions.ByCustomerId(_context.Orders.Include(a => a.OrderDetails).Include(a => a.Status), customer_id);
            var list = new List<Order>();
            foreach(var order in orders)
            {
                order.Status.Orders = null;
                if (order.StatusId >1 && order.StatusId < 5) list.Add(order);
            }
            return list;
        }

        public Order ReOrder(int order_id)
        {
            var order = GetOrder(order_id);

            order.OrderDate = DateTime.Now;

            return Create(_mapper.Map<OrderCreateModel>(order));
        }

    }
}
