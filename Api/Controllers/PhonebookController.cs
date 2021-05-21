using System;
using DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class PhonebookController : ControllerBase
    {
        private readonly IPhonebookService _phonebookService;
        public PhonebookController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                return Ok(_phonebookService.Get());
            }
            catch(Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("{idUser}")]
        public IActionResult GetByUser(int idUser)
        {
            try
            {
                return Ok(_phonebookService.GetUserPhonebook(idUser));
            }
            catch(ArgumentException e)
            {
                return BadRequest(new { error = e.Message});
            }
            catch(Exception e)
            {
                return StatusCode(500, new { error = e.Message});
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetMyPhonebook")]
        public IActionResult GetMyPhonebook()
        {
            try
            {
                int idUser = int.Parse(User.Identity.Name);
                return Ok(_phonebookService.GetUserPhonebook(idUser));
            }
            catch(ArgumentException e)
            {
                return BadRequest(new { error = e.Message});
            }
            catch(Exception e)
            {
                return StatusCode(500, new { error = e.Message});
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]PhonebookPostParams parameters)
        {
            try
            {
                return Ok(_phonebookService.Post(parameters.Name, parameters.Nickname, parameters.Cellphone, int.Parse(User.Identity.Name)));
            }
            catch(ArgumentException e)
            {
                return BadRequest(new { error = e.Message});
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}