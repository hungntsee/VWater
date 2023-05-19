using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IReportService
    {
        public ReportOrderResponseModel GetReportForNumber();
        public ReportRevenueResponeModel GetReportForRevenue();
        public ReportForOrderByStatusId GetReportForDashboard();
    }
    public class ReportService: IReportService
    {
        private VWaterContext _context;
        public ReportService (VWaterContext context)
        {
            _context = context;
        }

        private int GetNumberOfOrderByStatus(int status_id)
        {
            var ordersByStatus = OrderExtensions.ByStatusId(_context.Orders, status_id);
            return ordersByStatus.Count();
        }

        private int GetNumberOfOrderByCash()
        {
            var orders = _context.Orders.Where(a => a.AmountPaid == 0 && a.StatusId == 4);
            return orders.Count();
        }

        private int GetNumberOfOrderByOnlinePayment()
        {
            var orders = _context.Orders.Where(a => a.AmountPaid > 0 && a.StatusId == 4);
            return orders.Count();
        }

        public ReportOrderResponseModel GetReportForNumber()
        {
            var report = new ReportOrderResponseModel();
            report.DonHangGiaoThanhCong = GetNumberOfOrderByStatus(4);
            report.DonHangThanhToanTienMat = GetNumberOfOrderByCash();
            report.DonHangThanhToanOnline = GetNumberOfOrderByOnlinePayment();
            report.DonHangDaHoanTatGiaoDich = GetNumberOfOrderByStatus(8);
            report.TongDonHang = report.DonHangGiaoThanhCong + report.DonHangDaHoanTatGiaoDich;

            return report;
        }
        
        private decimal GetCashOrder()
        {
            var orders = _context.Orders.Where(a => a.AmountPaid == 0 && (a.StatusId == 4 || a.StatusId == 8));
            decimal amount = 0;
            foreach (var order in orders)
            {
                amount += order.TotalPrice;
            }
            return amount;
        }

        private decimal GetAmountPaidOfOrder() 
        {
            var orders = _context.Orders.Where(a => a.AmountPaid > 0 && a.StatusId != 7);
            decimal amount = 0;
            foreach (var order in orders)
            {
                amount += order.AmountPaid;
            }
            return amount;
        }

        private decimal GetPriceOfDepositNote()
        {
            var depositNotes = _context.DepositNotes;
            decimal price = 0;
            foreach (var deposit in depositNotes)
            {
                price += deposit.Price;
            }
            return price;
        }

        private decimal GetChiPhiPhatSinh()
        {
            var transactions = _context.Transactions.Where( a => a.TransactionType_Id == 6);
            decimal amount = 0;
            foreach(var transaction in transactions)
            {
                amount += transaction.Price;
            }
            return amount;
        }

        public ReportRevenueResponeModel GetReportForRevenue()
        {
            var report = new ReportRevenueResponeModel();
            report.TienMatDaThu = GetCashOrder();
            report.TienChuyenKhoan = GetAmountPaidOfOrder();
            report.TienCocBinh = GetPriceOfDepositNote();
            report.ChiPhiPhatSinh = GetChiPhiPhatSinh();
            report.TongDoanhThu = GetCashOrder() + GetAmountPaidOfOrder() + GetPriceOfDepositNote() -GetChiPhiPhatSinh();

            return report;
        }

        private int GetNumberOrderByStatus(int statusId)
        {
            var orders = _context.Orders.Include( o => o.Status ).Where( a => a.StatusId == statusId );
            return orders.Count();
        }

        public ReportForOrderByStatusId GetReportForDashboard()
        {
            ReportForOrderByStatusId report = new ReportForOrderByStatusId();
            report.WaitingOrder = GetNumberOrderByStatus(1);
            report.ApprovedOrder = GetNumberOrderByStatus(2);
            report.PayingOrder = GetNumberOrderByStatus(6);
            report.PaidOrder = GetNumberOrderByStatus(7);
            report.CancelOrder = GetNumberOrderByStatus(5);
            report.ShippingOrder = GetNumberOrderByStatus(3);
            report.ShippedOrder = GetNumberOrderByStatus(4);
            report.TotalOrder =_context.Orders.Count();

            return report;
        }
    }
}
