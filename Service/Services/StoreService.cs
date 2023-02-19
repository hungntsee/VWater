using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Stores
{
    public interface IStoreService
    {
        public IEnumerable<Store> GetAll();
        public StoreReadModel GetById(int id);
        public void Create(StoreCreateModel request);
        public void Update(int id, StoreUpdateModel request);
        public void Delete(int id);
    }
    public class StoreService : IStoreService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public StoreService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Store> GetAll()
        {
            return _context.Stores;
        }

        public StoreReadModel GetById(int id)
        {
            var store = _mapper.Map<StoreReadModel>(GetStore(id));
            return store;
        }

        public void Create(StoreCreateModel request)
        {
            if (_context.Stores.Any(b => b.StoreName == request.StoreName))
                throw new AppException("Store: '" + request.StoreName + "' already exists");
            var store = _mapper.Map<Store>(request);

            _context.Stores.AddAsync(store);
            _context.SaveChangesAsync();
        }

        public void Update(int id, StoreUpdateModel request)
        {
            var store = GetStore(id);

            if (_context.Stores.Any(b => b.StoreName == request.StoreName))
                throw new AppException("Store: '" + request.StoreName + "' already exists");
            _mapper.Map(request, store);
            _context.Stores.Update(store);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var store = GetStore(id);
            _context.Stores.Remove(store);
            _context.SaveChangesAsync();
        }

        private Store GetStore(int id)
        {
            var store = _context.Stores.Find(id);
            if (store == null) throw new KeyNotFoundException("Store not found!");
            return store;
        }

    }
}
