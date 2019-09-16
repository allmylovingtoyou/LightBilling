using System.Threading.Tasks;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Network : Controller
    {
        private readonly INetworkService _service;

        public Network(INetworkService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<JsonResult> FreeSubnets([FromBody] object any)
        {
            var result = await _service.GetFreeSubnets();
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AllSubnets([FromBody] object any)
        {
            var result = await _service.GetAllSubnets();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> FreeAddressesByHouseId(int houseId)
        {
            var result = await _service.GetFreeAddressesByHouseId(houseId);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> FreeWhiteAddresses()
        {
            var result = await _service.GetFreeWhiteAddresses();
            return Json(result);
        }
    }
}