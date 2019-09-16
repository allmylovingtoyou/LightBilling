using Api.Report;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        public JsonResult PaymentReport([FromBody] PaymentReportRequest request)
        {
            var result = _service.PaymentReport(request);
            return Json(result);
        }
    }
}