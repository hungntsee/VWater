﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.DeliveryAddresses
{
    public interface IDeliveryAddressService
    {
        public IEnumerable<DeliveryAddress> GetAll();
        public DeliveryAddress GetById(int id);
        public DeliveryAddress Create(DeliveryAddressCreateModel request);
        public DeliveryAddress GetOrdersByDeliveryAddress(int id);
        public void Update(int id, DeliveryAddressUpdateModel request);
        public void Delete(int id);
    }
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public DeliveryAddressService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DeliveryAddress> GetAll()
        {
            var deliveryAddresses = _context.DeliveryAddresses.Include(a => a.Customer).IgnoreAutoIncludes();
            foreach (var deliveryAddress in deliveryAddresses)
            {
                deliveryAddress.Customer.DeliveryAddresses = null;
            }
            return deliveryAddresses;
        }

        public DeliveryAddress GetById(int id)
        {
            var deliveryAddress = GetDeliveryAddress(id);
            return deliveryAddress;
        }

        public DeliveryAddress GetOrdersByDeliveryAddress(int id)
        {
            var deliveryAddress = _context.DeliveryAddresses.Include(a => a.Orders).AsNoTracking().FirstOrDefault(a => a.Id == id);
            return deliveryAddress;
        }

        public DeliveryAddress Create(DeliveryAddressCreateModel request)
        {           
            var deliveryAddresses = DeliveryAddressExtensions.ByCustomerId(_context.DeliveryAddresses, request.CustomerId);
            foreach (var address in deliveryAddresses)
            {
                if (request.Address.Equals(address.Address)) return address;
            }
            var deliveryAddress = _mapper.Map<DeliveryAddress>(request);
            deliveryAddress.StoreId = 1;

            deliveryAddress.Address.Trim().ToLower();

            _context.DeliveryAddresses.Add(deliveryAddress);
            _context.SaveChanges();

            return deliveryAddress;
        }

        public void Update(int id, DeliveryAddressUpdateModel request)
        {
            var deliveryAddress = GetDeliveryAddress(id);

            _mapper.Map(request, deliveryAddress);
            _context.DeliveryAddresses.Update(deliveryAddress);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var deliveryAddress = GetDeliveryAddress(id);
            _context.DeliveryAddresses.Remove(deliveryAddress);
            _context.SaveChangesAsync();
        }

        private DeliveryAddress GetDeliveryAddress(int id)
        {
            var deliveryAddress = _context.DeliveryAddresses.Include(a => a.Customer).Include(a => a.Orders).Include(a => a.DeliveryType)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (deliveryAddress == null) throw new KeyNotFoundException("DeliveryAddress not found!");
            return deliveryAddress;
        }

        private DeliveryAddress GetAddress(string address)
        {
            var deliveryAddress = _context.DeliveryAddresses.Include(a => a.Customer).Include(a => a.Orders).Include(a => a.DeliveryType)
                .AsNoTracking().FirstOrDefault(a => a.Address == address);
            return deliveryAddress;
        }
    }
}
