using System.Threading.Tasks;
using Api.Requests;
using Api.Tariff;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TariffController : Controller
    {
        private readonly ITariffService _service;

        public TariffController(ITariffService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResult> Tariff(int id)
        {
            var result = await _service.ById(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Tariff([FromBody] PageRequest<TariffFilter> request)
        {
            var result = await _service.GetPage(request);
            return Json(result);
        }

        [HttpPut]
        public async Task<JsonResult> Tariff([FromBody] TariffDto request)
        {
            var result = await _service.Create(request);
            return Json(result);
        }

        [HttpPatch]
        public async Task<JsonResult> Tariff([FromBody] TariffUpdateDto request)
        {
            var result = await _service.Update(request);
            return Json(result);
        }

        [HttpDelete]
        public async Task<JsonResult> Tariff([FromBody] DeleteRequest request)
        {
            var result = await _service.Delete(request.Id);
            return Json(result);
        }
    }
}