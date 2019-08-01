using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Requests;
using Api.Client;
using Api.House;
using Db;
using Domain;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResult> Client(int id)
        {
            var result = await _service.ById(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Client([FromBody] PageRequest<ClientFilter> request)
        {
            var result = await _service.GetPage(request);
            return Json(result);
        }

        [HttpPut]
        public async Task<JsonResult> Client([FromBody] ClientDto request)
        {
            var result = await _service.Create(request);
            return Json(result);
        }

        [HttpPatch]
        public async Task<JsonResult> Client([FromBody] ClientUpdateDto request)
        {
            var result = await _service.Update(request);
            return Json(result);
        }

        [HttpDelete]
        public async Task<JsonResult> Client([FromBody] DeleteRequest request)
        {
            var result = await _service.Delete(request.Id);
            return Json(result);
        }
    }
}