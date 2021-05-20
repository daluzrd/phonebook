using System;
using DataAccess;
using DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        public AuthenticateController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserParams parameters)
        {
            try
            {
                return Ok(_authenticationService.Login(parameters.username, parameters.password));
            }
            catch(Exception e)
            {
                return StatusCode(500, new { error = e.Message});
            }
        }
    }
}