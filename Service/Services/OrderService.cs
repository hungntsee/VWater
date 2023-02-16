﻿using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetAll();
        public OrderReadModel GetById(int id);
        public void Create(OrderCreateModel model);
        public void Update(int id, OrderUpdateModel model);
        public void Delete(int id);
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
            return _context.Orders;
        }

        public OrderReadModel GetById(int id)
        {
            var orderResponse = _mapper.Map<OrderReadModel>(GetOrder(id));
            return orderResponse;
        }

        public void Create(OrderCreateModel model)
        {
            var order = _mapper.Map<Order>(model);
            _context.Orders.AddAsync(order);
            _context.SaveChangesAsync();
        }

        public void Update(int id, OrderUpdateModel model)
        {
            var order = GetOrder(id);
            _mapper.Map(model, order);
            _context.Orders.Update(order);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var order = GetOrder(id);
            _context.Orders.Remove(order);
            _context.SaveChangesAsync();
        }

        private Order GetOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(p => p.Id == id);
            return order;
        }
    }
}