using System;
using DataAccess;
using DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthenticateController(DataContext dbContext, IOptions<AppSettings> appSettings)
        {
            _userService = new UserService(dbContext, appSettings);
        }

        [HttpPost]
        public IActionResult Login([FromBody]UserParams parameters)
        {
            try
            {
                return Ok(_userService.Login(parameters.username, parameters.password));
            }
            catch(Exception e)
            {
                return StatusCode(500, new { error = e.Message});
            }
        }
    }
}