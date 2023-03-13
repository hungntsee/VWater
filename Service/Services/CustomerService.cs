using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface ICustomerService
    {
        public IEnumerable<Customer> GetAll();
        public Customer GetById(int id);
        public Customer GetCustomerByPhone(string phone);
        public Customer Create(CustomerCreateModel model);
        public void Update(int id, CustomerUpdateModel model);
        public void Delete(int id);
        public Customer GetOrderByPhonenumber(int customer_id);
    }
    public class CustomerService : ICustomerService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public CustomerService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.Include(a => a.DeliveryAddresses);
        }

        public Customer GetById(int id)
        {
            var customer = GetCustomer(id);
            return customer;
        }

        public Customer Create(CustomerCreateModel model)
        {
            if (GetCustomerByPhone(model.PhoneNumber) == null)
            {
                throw new AppException("This Phone number is already existed!");
            }

            var customer = _mapper.Map<Customer>(model);

            customer.Note = " ";

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        public void Update(int id, CustomerUpdateModel model)
        {
            var customer = GetCustomer(id);
            _mapper.Map(model, customer);
            _context.Customers.Update(customer);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var customer = GetCustomer(id);
            _context.Customers.Remove(customer);
            _context.SaveChangesAsync();
        }

        private Customer GetCustomer(int id)
        {
            var customer = _context.Customers.Include(a => a.DeliveryAddresses).AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (customer == null) throw new KeyNotFoundException("Customer not found!");
            return customer;
        }

        public Customer GetCustomerByPhone(string phone)
        {
            var customer = _context.Customers.Include(a => a.DeliveryAddresses).AsNoTracking().FirstOrDefault(a => a.PhoneNumber == phone);
            return customer;
        }

        public Customer GetOrderByPhonenumber(int customer_id)
        {
            var customers = _context.Customers.Include(a => a.DeliveryAddresses).ThenInclude(a => a.Orders).FirstOrDefault(a => a.Id == customer_id);
            return customers;
        }
    }
}
