using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Requests;
using Api.User;
using Db;
using Domain;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemUserController : Controller
    {
        private readonly IUserService _service;

        public SystemUserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<JsonResult> SystemUser([FromBody] PageRequest request)
        {
            var result = await _service.GetPage(request);
            return Json(result);
        }

        [HttpPut]
        public async Task<JsonResult> SystemUser([FromBody] SystemUserDto request)
        {
            var result = await _service.CreateUser(request);
            return Json(result);
        }
    }
}