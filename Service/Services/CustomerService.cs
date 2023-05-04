using AutoMapper;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using System.Text.RegularExpressions;
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
        public Customer GetHistoryOrder(int customer_id);
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

        private void ValidateCustomer(Customer customer)
        {
            var phonenumber = customer.PhoneNumber;
            var fullname = customer.FullName;
            if (phonenumber.Length < 10 || phonenumber.Length > 11) throw new AppException("Wrong format for Phonenumber: Length from 10 to 11.");
            if (Regex.IsMatch(phonenumber, @"\D") || Regex.IsMatch(phonenumber, @"\s")) throw new AppException("Wrong format for Phonenumber: Only have number.");
            if (Regex.IsMatch(fullname, @"\W{\s}")) throw new AppException("Wrong input for Fullname: Can't have special character.");
        }

        public Customer Create(CustomerCreateModel model)
        {

            ValidateCustomer(_mapper.Map<Customer>(model));
            if (GetCustomerByPhone(model.PhoneNumber) != null)
            {
                var customer1 = GetCustomerByPhone(model.PhoneNumber);

                var da1 = customer1.DeliveryAddresses.ToString;
                string daa1 = da1().Trim().ToLower();


                customer1.Note = "Welcome Back";
                //throw new AppException("This Phone number is already existed!");
                _context.Customers.Update(customer1);
                _context.SaveChanges();
                return customer1;
            }

            var customer = _mapper.Map<Customer>(model);

            var da = customer.DeliveryAddresses.ToString;
            string daa = da().Trim().ToLower();

            customer.Note = "New User";

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
            _context.SaveChanges();

        }

        private Customer GetCustomer(int id)
        {
            var customer = _context.Customers.Include(a => a.DeliveryAddresses).ThenInclude(a => a.Orders.OrderByDescending(a => a.Id)).AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (customer == null) throw new KeyNotFoundException("Customer not found!");
            return customer;
        }

        public Customer GetCustomerByPhone(string phone)
        {
            var customer = _context.Customers.Include(a => a.DeliveryAddresses).AsNoTracking().FirstOrDefault(a => a.PhoneNumber == phone);
            return customer;
        }

        public Customer GetHistoryOrder(int customer_id)
        {
            var customer = _context.Customers
                .Include(a => a.DeliveryAddresses)
                .ThenInclude(a => a.Orders)
                .ThenInclude(a => a.OrderDetails)
                .AsNoTracking().FirstOrDefault(a => a.Id == customer_id);            

            return customer;
        }
    }
}
