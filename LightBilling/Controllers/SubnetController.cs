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

//        [HttpGet]
//        public async Task<JsonResult> Generate(int count)
//        {
//            var random = new Random();
//            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//
//            for (int i = 0; i < count; i++)
//            {
//                await _service.Create(new HouseDto
//                {
//                    Number = random.Next(1, 100).ToString(),
//                    Address = new string(Enumerable.Repeat(chars, random.Next(2, 10))
//                        .Select(s => s[random.Next(s.Length)]).ToArray()),
//                    Comment = new string(Enumerable.Repeat(chars, random.Next(1, 5))
//                        .Select(s => s[random.Next(s.Length)]).ToArray()),
//                    AdditionalNumber = random.Next(10).ToString()
//                });
//            }
//
//            return Json(count);
//        }

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