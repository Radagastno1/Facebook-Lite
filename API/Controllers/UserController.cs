using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using System;
using System.Threading.Tasks;
using CORE;

namespace Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserServiceForDTO _userService;

        public UserController(UserServiceForDTO userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> GetUserDTO([FromBody] OutgoingLogInDTO loginData)
        {
            try
            {
                var loggedInUser = _userService.GetByJWT(loginData.Jwt);

                if (loggedInUser == null)
                {
                    return BadRequest("Failed to get user.");
                }

                return Ok(loggedInUser.FirstName);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
