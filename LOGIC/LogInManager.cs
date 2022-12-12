using CORE;
namespace LOGIC;
public class LogInManager : ILogInManager<User>
{
    ILogInDB<User> _logInUser;

    public LogInManager(ILogInDB<User> logInUser)
    {
        _logInUser = logInUser;
    }
    public User LogIn(string email, string passWord)  // ska ist prata med logindb!!
    {
        try
        {
            User user = _logInUser.GetMemberByLogIn(email, passWord);
            _logInUser.UpdateToActivated(user.ID);
            return user;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}