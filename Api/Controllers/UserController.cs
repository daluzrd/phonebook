using System;
using System.Collections.Generic;
using DataAccess;
using DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace phonebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(DataContext dbContext)
        {
            _userService = new UserService(dbContext);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_userService.Get());
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(string username, string password)
        {
            try
            {
                return Ok(_userService.Post(username, password));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
