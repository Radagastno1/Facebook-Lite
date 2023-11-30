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
        private readonly ILogInManager<LogInDTO> _iLogInManager;

        public LogInController(ILogInManager<LogInDTO> iLogInManager)
        {
            _iLogInManager = iLogInManager;
        }

        [HttpPost]
        public async Task<ActionResult<LogInDTO>> LogInUser([FromBody] LogInDTO loginData)
        {
            try
            {
                var loggedInUser = _iLogInManager.LogIn(loginData.Email, loginData.PassWord);

                if (loggedInUser == null)
                {
                    return BadRequest("Failed to log in.");
                }

                // Implementera logiken för att generera och returnera JWT här.

                // Exempel: return Ok(new { Token = "your_generated_token" });
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
