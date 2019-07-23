using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.User;
using Db;
using Domain;
using LightBilling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightBilling.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpPost]
        public async Task<JsonResult> User([FromBody] UserDto request)
        {
            var result = await _service.CreateUser(request);
            return Json(result);
        }
    }
}