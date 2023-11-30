using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using LOGIC;
using System;
using System.Threading.Tasks;
using CORE;

namespace Controllers
{
    [ApiController]
    [Route("login")]
    public class LogInController : ControllerBase
    {
        private readonly ILogInManager<OutgoingLogInDTO> _iLogInManager;

        public LogInController(ILogInManager<OutgoingLogInDTO> iLogInManager)
        {
            _iLogInManager = iLogInManager;
        }

        [HttpPost]
        public async Task<ActionResult<OutgoingLogInDTO>> LogInUser(
            [FromBody] IncomingLogInDTO loginData
        )
        {
            try
            {
                var loggedInUser = _iLogInManager.LogIn(loginData.Email, loginData.PassWord);

                if (loggedInUser == null)
                {
                    return BadRequest("Failed to log in.");
                }

                return Ok(loggedInUser);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
