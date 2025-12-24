using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = _authService.Login(loginRequest);
            if (token == null)
                return Unauthorized("Username or password is wrong");

            return Ok(new {Token = token});
        }
    }
}