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

    }
}
