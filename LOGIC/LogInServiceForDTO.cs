using DTO;
using CORE;

namespace LOGIC
{
    public class LogInServiceForDTO : ILogInManager<LogInDTO>
    {
        private readonly ILogInDB<User> _logInUser;

        public LogInServiceForDTO(ILogInDB<User> logInUser)
        {
            _logInUser = logInUser;
        }

        public LogInDTO LogIn(string email, string passWord)
        {
            try
            {
                User user = _logInUser.GetMemberByLogIn(email, passWord);

                _logInUser.UpdateToActivated(user.ID);

                LogInDTO logInDTO = new LogInDTO(user.Email, user.PassWord);

                return logInDTO;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
