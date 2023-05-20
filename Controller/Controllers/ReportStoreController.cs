using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportStoreController : ControllerBase
    {
        private IReportStoreService _reportStoreService;
        public ReportStoreController(IReportStoreService reportStoreService)
        {
            _reportStoreService = reportStoreService;
        }

        [HttpGet("/api/GetReportForStoreManager")]
        public IActionResult GetReportForStoreManager(int store_id)
        {
            var number = _reportStoreService.GetReportForNumber(store_id);
            var revenue = _reportStoreService.GetReportForRevenue(store_id);
            ICollection<Object> result = new List<Object>();

            result.Add(number);
            result.Add(revenue);

            return Ok(result);
        }
        [HttpGet("/api/GetNumberForDashBoardForStoreManager")]
        public IActionResult GetNumberForDashboardForStoreManager(int store_id)
        {
            var reportStore = _reportStoreService.GetReportForDashboard(store_id);
            return Ok(reportStore);
        }
    }
}
