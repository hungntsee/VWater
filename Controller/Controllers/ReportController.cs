using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _reportService;
        public ReportController (IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("/api/GetReportForAdmin")]
        public IActionResult GetReportForAdmin()
        {
            var number = _reportService.GetReportForNumber();
            var revenue = _reportService.GetReportForRevenue();
            ICollection<Object> result = new List<Object>();

            result.Add(number);
            result.Add(revenue);

            return Ok(result);
        }
        [HttpGet("/api/GetNumberForDashBoard")]
        public IActionResult GetNumberForDashboard()
        {
            var report = _reportService.GetReportForDashboard();
            return Ok(report);
        }
    }
}
