using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ;
using Repository.Domain.Models;
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
        public int GetNumberOfOrder();
        public ReportOrderResponseModel GetReport();
        public DepositNote CreateDepositeNote(DepositNoteCreateModel model);

    }
    public class OrderService : IOrderService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private Send send = new Send();

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
            OrderJsonFile(orderResponse);
            return orderResponse;
        }

        public Order Create(OrderCreateModel model)
        {
            var order = _mapper.Map<Order>(model);
            order.OrderDate = DateTime.Now;
            order.StoreId = 1;
            order.IsDeposit = false;
            order.StatusId = 2;
            order.ShipperId = null;

            //if (order.Price > 500000)
            //order.StoreId = 1;

            _context.Orders.Add(order);
            _context.SaveChanges();

            var responseOrder = GetOrder(order.Id);
            OrderJsonFile(responseOrder);

            var message = JsonConvert.SerializeObject(responseOrder, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            send.SendMessage(message);
            return responseOrder;
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

        public Order TakeOrder(int order_id, int shipper_id)
        {
            //if(order.ShipperId == null)
            var order = GetOrderIgnoreInclude(order_id);
            order.StatusId = 3;
            order.ShipperId = shipper_id;

            _context.Orders.Update(order);
            _context.SaveChanges();

            var responseOrder = GetOrder(order.Id);

            CreateTransactionForOrder(responseOrder);

            responseOrder.Shipper.Orders = null;
            responseOrder.Shipper.Wallets.Transactions = null;
            foreach (var transaction in responseOrder.Transactions)
            {
                transaction.Wallet = null;
            }
            OrderJsonFile(responseOrder);

            return responseOrder;
        }

        private Order GetOrder(int id)
        {
            var order = OrderExtensions.GetByKey(_context.Orders
                .Include(a => a.DeliveryAddress).ThenInclude(a => a.Customer)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                , id);
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
            var orders = OrderExtensions.ByCustomerId(_context.Orders
                .Include(a => a.DeliveryAddress)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                .ThenInclude(a => a.Product), customer_id);

            foreach(var order in orders)
            {
                OrderJsonFile(order);
            }

            return orders.ToList();
        }

        public List<Order> FollowOrder(int customer_id)
        {
            var orders = OrderExtensions.ByCustomerId(_context.Orders
                            .Include(a => a.DeliveryAddress)
                            .Include(a => a.Store)
                            .Include(a => a.Status)
                            .Include(a => a.DeliverySlot)
                            .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu)
                            .ThenInclude(a => a.Product), customer_id);
            var list = new List<Order>();
            foreach (var order in orders)
            {
                OrderJsonFile(order);
                if (order.StatusId > 1 && order.StatusId < 5) list.Add(order);
            }
            return list;
        }

        private void OrderJsonFile(Order order) 
        {

            order.DeliveryAddress.Orders = null;
            order.DeliveryAddress.Customer.DeliveryAddresses = null;
            order.Status.Orders = null;
            order.Status.PurchaseOrders = null;
            order.Store.Orders = null;
            order.Store.DeliveryAddresses = null;
            order.Store.DeliverySlots = null;
            order.Store.PurchaseOrders = null;
            order.Store.Shippers = null;
            order.Store.Warehouses = null;
            order.DeliverySlot.Orders = null;
            order.DeliverySlot.Store = null;
            order.GoodsExchangeNotes = null;
            order.DepositNote = null;
            order.Status.Orders = null;
            order.Transactions = null;
            foreach (var order1 in order.OrderDetails)
             {
               order1.ProductInMenu.OrderDetails = null;
               order1.ProductInMenu.Product.ProductInMenus = null;
               order1.ProductInMenu.Product.GoodsInProducts = null;
             }
            
        }
        public Order ReOrder(int order_id)
        {
            var order = GetOrder(order_id);

            order.OrderDate = DateTime.Now;

            var newOrder = Create(_mapper.Map<OrderCreateModel>(order));

            var responeOrder = _context.Orders
                .Include(a => a.DeliveryAddress)
                .Include(a => a.Store)
                .Include(a => a.Status)
                .Include(a => a.DeliverySlot)
                .Include(a => a.OrderDetails).ThenInclude(a => a.ProductInMenu).ThenInclude(a => a.Product)
                .AsNoTracking().FirstOrDefault(a => a.Id == newOrder.Id);

            OrderJsonFile(responeOrder);

            return responeOrder;
        }

        public int GetNumberOfOrder()
        {
            return _context.Orders.Count();
        }

        private int GetNumberOfOrderByStatus(int status_id)
        {
            var ordersByStatus = OrderExtensions.ByStatusId(_context.Orders, status_id);
            return ordersByStatus.Count();
        }

        public ReportOrderResponseModel GetReport()
        {
            var report = new ReportOrderResponseModel();
            report.NumberOfFinishOrder = GetNumberOfOrderByStatus(1);
            report.NumberOfWaitingOrder = GetNumberOfOrderByStatus(2);
            report.NumberOfConfirmedOrder = GetNumberOfOrderByStatus(3);
            report.NumberOfShippingOrder = GetNumberOfOrderByStatus(4);
            report.NumberOfFailOrder = GetNumberOfOrderByStatus(5);

            return report;
        }

        public DepositNote CreateDepositeNote(DepositNoteCreateModel model) 
        {
            var order = GetOrder(model.OrderId);

            order.IsDeposit = true;

            var depositeNote = _mapper.Map<DepositNote>(model);

            if(depositeNote.IsDeposit == true)
            {
                order.TotalPrice += depositeNote.Price;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }

            _context.DepositNotes.Add(depositeNote);
            _context.SaveChanges();

            return depositeNote;
        }

        private void CreateTransactionForOrder(Order order)
        {
            var shipper = _context.Shippers.Include(a =>a .Wallets).AsNoTracking().FirstOrDefault(a => a.Id == order.ShipperId);
            var transaction = new Transaction();

            transaction.Date = DateTime.Now;

            int quantityDeposit = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.ProductInMenu.Product.Description == "Bình") quantityDeposit++;
            }

            var priceDeposit = 15000 * quantityDeposit;
            transaction.Price = order.TotalPrice + priceDeposit;
            transaction.WalletId = shipper.Wallets.Id;
            transaction.OrderId = order.Id;
            transaction.Note = "Take Order";

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            shipper.Wallets.Credit += transaction.Price;
            _context.Wallets.Update(shipper.Wallets);
            _context.SaveChanges();
        }

    }
}
