using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IReportStoreService
    {
        public ReportOrderResponseModel GetReportForNumber(int store_id);
        public ReportRevenueResponeModel GetReportForRevenue(int store_id);
        public ReportForOrderByStatusId GetReportForDashboard(int store_id);
    }
    public class ReportStoreService : IReportStoreService
    {
        private VWaterContext _context;
        public ReportStoreService(VWaterContext context)
        {
            _context = context;
        }

        private int GetNumberOfOrderByStatus(int status_id, int store_id)
        {
            var ordersByStatus = _context.Orders.Where(a => a.AmountPaid == 0 && a.StatusId == status_id && a.StoreId == store_id);
            return ordersByStatus.Count();
        }

        private int GetNumberOfOrderByCash(int store_id)
        {
            var orders = _context.Orders.Where(a => a.AmountPaid == 0 && a.StatusId == 4 && a.StoreId == store_id);
            return orders.Count();
        }

        private int GetNumberOfOrderByOnlinePayment(int store_id)
        {
            var orders = _context.Orders.Where(a => a.AmountPaid > 0 && a.StatusId == 4 && a.StoreId == store_id);
            return orders.Count();
        }

        public ReportOrderResponseModel GetReportForNumber(int store_id)
        {
            var report = new ReportOrderResponseModel();
            report.DonHangGiaoThanhCong = GetNumberOfOrderByStatus(4, store_id);
            report.DonHangThanhToanTienMat = GetNumberOfOrderByCash(store_id);
            report.DonHangThanhToanOnline = GetNumberOfOrderByOnlinePayment(store_id);
            report.DonHangDaHoanTatGiaoDich = GetNumberOfOrderByStatus(8, store_id);
            report.TongDonHang = report.DonHangGiaoThanhCong + report.DonHangDaHoanTatGiaoDich;

            return report;
        }

        private decimal GetCashOrder(int store_id)
        {
            var orders = _context.Orders.Where(a => a.AmountPaid == 0 && (a.StatusId == 4 || a.StatusId == 8) && a.StoreId == store_id);
            decimal amount = 0;
            foreach (var order in orders)
            {
                amount += order.TotalPrice;
            }
            return amount;
        }

        private decimal GetAmountPaidOfOrder(int store_id)
        {
            var orders = _context.Orders.Where(a => a.AmountPaid > 0 && a.StatusId != 7 && a.StoreId == store_id);
            decimal amount = 0;
            foreach (var order in orders)
            {
                amount += order.AmountPaid;
            }
            return amount;
        }

        private decimal GetPriceOfDepositNote(int store_id)
        {
            var depositNotes = _context.DepositNotes.Where(a => a.Order.StoreId == store_id);
            decimal price = 0;
            foreach (var deposit in depositNotes)
            {
                price += deposit.Price;
            }
            return price;
        }

        private decimal GetChiPhiPhatSinh(int store_id)
        {
            var transactions = _context.Transactions.Where(a => a.TransactionType_Id == 6 && a.Order.StoreId == store_id);
            decimal amount = 0;
            foreach (var transaction in transactions)
            {
                amount += transaction.Price;
            }
            return amount;
        }

        public ReportRevenueResponeModel GetReportForRevenue(int store_id)
        {
            var report = new ReportRevenueResponeModel();
            report.TienMatDaThu = GetCashOrder(store_id);
            report.TienChuyenKhoan = GetAmountPaidOfOrder(store_id);
            report.TienCocBinh = GetPriceOfDepositNote(store_id);
            report.ChiPhiPhatSinh = GetChiPhiPhatSinh(store_id);
            report.TongDoanhThu = GetCashOrder(store_id) + GetAmountPaidOfOrder(store_id) + GetPriceOfDepositNote(store_id) - GetChiPhiPhatSinh(store_id);

            return report;
        }

        private int GetNumberOrderByStatus(int statusId, int store_id)
        {
            var orders = _context.Orders.Include(o => o.Status).Where(a => a.StatusId == statusId && a.StoreId == store_id);
            return orders.Count();
        }

        public ReportForOrderByStatusId GetReportForDashboard(int store_id)
        {
            ReportForOrderByStatusId report = new ReportForOrderByStatusId();
            report.WaitingOrder = GetNumberOrderByStatus(1, store_id);
            report.ApprovedOrder = GetNumberOrderByStatus(2, store_id);
            report.PayingOrder = GetNumberOrderByStatus(6, store_id);
            report.PaidOrder = GetNumberOrderByStatus(7, store_id);
            report.CancelOrder = GetNumberOrderByStatus(5, store_id);
            report.ShippingOrder = GetNumberOrderByStatus(3, store_id);
            report.ShippedOrder = GetNumberOrderByStatus(4, store_id);
            report.TotalOrder = _context.Orders.Where(a=>a.StoreId== store_id).Count();

            return report;
        }
    }
}
