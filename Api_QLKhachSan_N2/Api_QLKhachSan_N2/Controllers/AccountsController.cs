using Api_QLKhachSan_N2.Entities;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace Api_QLKhachSan_N2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public AccountsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromForm] AuthenticationRequest authenticationRequest)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager(Configuration);
            var authResult = jwtAuthenticationManager.Authenticate(authenticationRequest.UserName, authenticationRequest.Password);
            if (authResult == null)
            {
                return Unauthorized();
            }
            else
                return Ok(authResult);
        }

        
    }
}
