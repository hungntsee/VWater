using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Brands
{
    public interface IBrandService
    {
        public IEnumerable<Brand> GetAll();
        public BrandReadModel GetById(int id);
        public void Create(BrandCreateModel request);
        public void Update(int id, BrandUpdateModel request);
        public void Delete(int id);
    }
    public class BrandService : IBrandService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public BrandService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands;
        }

        public BrandReadModel GetById(int id)
        {
            var brand = _mapper.Map<BrandReadModel>(GetBrand(id));
            return brand;
        }

        public void Create(BrandCreateModel request)
        {
            if (_context.Brands.Any(b => b.BrandName == request.BrandName))
                throw new AppException("Brand: '" + request.BrandName + "' already exists");
            var brand = _mapper.Map<Brand>(request);

            _context.Brands.AddAsync(brand);
            _context.SaveChangesAsync();
        }

        public void Update(int id, BrandUpdateModel request)
        {
            var brand = GetBrand(id);

            if (_context.Brands.Any(b => b.BrandName == request.BrandName))
                throw new AppException("Brand: '" + request.BrandName + "' already exists");
            _mapper.Map(request, brand);
            _context.Brands.Update(brand);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var brand = GetBrand(id);
            _context.Brands.Remove(brand);
            _context.SaveChangesAsync();
        }

        private Brand GetBrand(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null) throw new KeyNotFoundException("Brand not found!");
            return brand;
        }

    }
}
