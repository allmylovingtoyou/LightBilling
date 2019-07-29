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
    public class HouseController : Controller
    {
        private readonly IHouseService _service;

        public HouseController(IHouseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResult> Generate(int count)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < count; i++)
            {
                await _service.Create(new HouseDto
                {
                    Number = random.Next(1, 100).ToString(),
                    Address = new string(Enumerable.Repeat(chars, random.Next(2, 10))
                        .Select(s => s[random.Next(s.Length)]).ToArray()),
                    Comment = new string(Enumerable.Repeat(chars, random.Next(1, 5))
                        .Select(s => s[random.Next(s.Length)]).ToArray()),
                    AdditionalNumber = random.Next(10).ToString()
                });
            }

            return Json(count);
        }

        [HttpGet]
        public async Task<JsonResult> House(int id)
        {
            var result = await _service.ById(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> House([FromBody] PageRequest<HouseFilter> request)
        {
            var result = await _service.GetPage(request);
            return Json(result);
        }

        [HttpPut]
        public async Task<JsonResult> House([FromBody] HouseDto request)
        {
            var result = await _service.Create(request);
            return Json(result);
        }

        [HttpPatch]
        public async Task<JsonResult> House([FromBody] HouseUpdateDto request)
        {
            var result = await _service.Update(request);
            return Json(result);
        }

        [HttpDelete]
        public async Task<JsonResult> House([FromBody] DeleteRequest request)
        {
            var result = await _service.Delete(request.Id);
            return Json(result);
        }
    }
}