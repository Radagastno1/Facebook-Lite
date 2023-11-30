using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CORE;

namespace LOGIC
{
    public class LogInServiceForDTO : ILogInManager<OutgoingLogInDTO>
    {
        private readonly ILogInDB<User> _logInUser;
        private readonly IConfiguration _configuration;

        public LogInServiceForDTO(ILogInDB<User> logInUser, IConfiguration configuration)
        {
            _logInUser = logInUser;
            _configuration = configuration;
        }

        public OutgoingLogInDTO LogIn(string email, string passWord)
        {
            try
            {
                User user = _logInUser.GetMemberByLogIn(email, passWord);
                if (user != null)
                {
                    _logInUser.UpdateToActivated(user.ID);

                    // Skapa en JWT-token
                    string jwtToken = GenerateJwtToken(user);

                    OutgoingLogInDTO logInDTO = new OutgoingLogInDTO(
                        user.Email,
                        user.PassWord,
                        jwtToken
                    );

                    return logInDTO;
                }

                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Hämta nyckeln från konfigurationen som en Base64-kodad sträng
            var base64Key = _configuration["Jwt:Secret"];

            try
            {
                // Konvertera Base64-strängen till byte-array
                var key = Convert.FromBase64String(base64Key);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim("UserId", user.ID.ToString())
                        }
                    ),
                    Expires = DateTime.UtcNow.AddDays(14),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error converting Base64 string: {ex.Message}");
                return null;
            }
        }
    }
}
