using AutoMapper;
using VWater.Data.Entities;
using VWater.Data;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface ICustomerService
    {
        public IEnumerable<Customer> GetAll();
        public CustomerReadModel GetById(int id);
        public void Create(CustomerCreateModel model);
        public void Update(int id, CustomerUpdateModel model);
        public void Delete(int id);
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
            return _context.Customers;
        }

        public CustomerReadModel GetById(int id)
        {
            var customer = _mapper.Map<CustomerReadModel>(GetCustomer(id));
            return  customer;
        }

        public void Create(CustomerCreateModel model)
        {
            var customer = _mapper.Map<Customer>(model);

            _context.Customers.AddAsync(customer);
            _context.SaveChangesAsync();
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
            var customer = _context.Customers.Find(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found!");
            return customer;
        }

    }
}
