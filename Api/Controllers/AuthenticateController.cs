using System;
using DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticateController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserParams parameters)
        {
            try
            {
                return Ok(_authenticationService.Login(parameters.Username, parameters.Password));
            }
            catch(Exception e)
            {
                return StatusCode(500, new { error = e.Message});
            }
        }
    }
}