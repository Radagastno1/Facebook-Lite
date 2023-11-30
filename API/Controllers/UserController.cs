using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using System;
using System.Threading.Tasks;
using CORE;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserDTO()
        {
            Console.WriteLine("I GET USER");
            try
            {
                var jwt = HttpContext.Request.Headers["Authorization"]
                    .ToString()
                    .Replace("Bearer ", string.Empty);

                Console.WriteLine(jwt);
                if (string.IsNullOrWhiteSpace(jwt))
                {
                    return BadRequest("JWT token is missing.");
                }
                var loggedInUser = _userService.GetByJWT(jwt);

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
