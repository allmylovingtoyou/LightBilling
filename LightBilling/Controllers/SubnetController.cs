using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Requests;
using Api.House;
using Db;
using Domain;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubnetController : Controller
    {
        private readonly ISubnetService _service;

        public SubnetController(ISubnetService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<JsonResult> Free([FromBody] object any)
        {
            var result = await _service.GetFreeSubnets();
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> All([FromBody] object any)
        {
            var result = await _service.GetAllSubnets();
            return Json(result);
        }
    }
}