using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Entities.DTOs;
using MovieStoreWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Auth(CustomerAuthDto customerAuthDto)
        {
            var token = _authService.Auth(customerAuthDto);
            return Ok(token);
        }

        [HttpPost]
        [Route("refreshToken")]
        public IActionResult RefreshToken([FromQuery] string refreshToken)
        {
            var token = _authService.RefreshToken(refreshToken);
            return Ok(token);
        }
    }
}
