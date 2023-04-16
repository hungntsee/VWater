using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Domain.Models;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Shippers
{
    public interface IShipperService
    {
        public IEnumerable<Shipper> GetAll();
        public Shipper GetById(int id);
        public void Create(ShipperCreateModel request);
        public void Update(int id, ShipperUpdateModel request);
        public void Delete(int id);
        public int GetNumberOfShipper();
        public void StatusOfShipper(int id, ShipperStatusModel request1);
    }
    public class ShipperService : IShipperService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public ShipperService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Shipper> GetAll()
        {
            return _context.Shippers.Include(a => a.Orders).Include(a => a.Wallets);
        }

        public Shipper GetById(int id)
        {
            var shipper = GetShipper(id);
            return shipper;
        }

        public void Create(ShipperCreateModel request)
        {
            var shipper = _mapper.Map<Shipper>(request);

            _context.Shippers.AddAsync(shipper);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ShipperUpdateModel request)
        {
            var shipper = GetShipper(id);

            _mapper.Map(request, shipper);
            _context.Shippers.Update(shipper);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var shipper = GetShipper(id);
            _context.Shippers.Remove(shipper);
            _context.SaveChangesAsync();
        }

        private Shipper GetShipper(int id)
        {
            var shipper = _context.Shippers.Include(a => a.Orders).Include(a => a.Wallets)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (shipper == null) throw new KeyNotFoundException("Shipper not found!");
            return shipper;
        }

        public int GetNumberOfShipper()
        {
            var numberOfShipper = _context.Shippers.Count();

            return numberOfShipper;
        }

        private int GetNumberOfOrderByStatusForShipper(int status_id)
        {
            var ordersByStatusForShipper = OrderExtensions.ByStatusId(_context.Orders, status_id);
            return ordersByStatusForShipper.Count();
        }

        public ReportOrderResponseModel GetReportForShipper(int shipper_id)
        {
            var report = new ReportOrderResponseModel();

            var shipper = GetShipper(shipper_id);

            report.NumberOfFinishOrder = GetNumberOfOrderByStatusForShipper(1);
            report.NumberOfWaitingOrder = GetNumberOfOrderByStatusForShipper(2);
            report.NumberOfConfirmedOrder = GetNumberOfOrderByStatusForShipper(3);
            report.NumberOfShippingOrder = GetNumberOfOrderByStatusForShipper(4);
            report.NumberOfFailOrder = GetNumberOfOrderByStatusForShipper(5);

            return report;
        }

        public void StatusOfShipper(int id, ShipperStatusModel request1)
        {
            var shipper = GetShipper(id);
            _mapper.Map(request1, shipper);

            if (shipper.IsOnline == true) { shipper.IsOnline = false; }
            else { shipper.IsOnline = true; }

            _context.Shippers.Update(shipper);
            _context.SaveChangesAsync();
        }
    }
}
