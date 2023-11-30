using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CORE;

namespace LOGIC
{
    public class UserServiceForDTO
    {
        IDataToObject<User, User> _userDataToObject;

        public UserServiceForDTO(IDataToObject<User, User> userDataToObject)
        {
            _userDataToObject = userDataToObject;
        }

        public User GetByJWT(string jwt)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadToken(jwt) as JwtSecurityToken;

                if (token != null)
                {
                    var idClaim = token.Claims.FirstOrDefault(
                        claim => claim.Type == "sub" || claim.Type == "UserId"
                    );

                    if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
                    {
                        User foundUser = _userDataToObject.GetOne(userId, null);
                        return foundUser;
                    }
                }

                return null;
            }
            catch (SecurityTokenException)
            {
                // Tokenet är ogiltigt
                return null;
            }
        }
    }
}
