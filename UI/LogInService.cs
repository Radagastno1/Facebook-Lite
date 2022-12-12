using CORE;
using LOGIC;
using DATABASE; //UI måste känna till database pga loginmanager? egentligen inte i lök va?
namespace UI;
public class LogInService
{
    ILogInManager<User> _logInManager;
    public LogInService(ILogInManager<User> logInManager)
    {
        _logInManager = logInManager;
    }
    public User? LogIn()
    {
        string email = ConsoleInput.GetEmail("Email: ");
        string passWord = ConsoleInput.GetPassword("Password: ");
         //DENNA SKA HA EN INTERFACE, NU HÅRDKOPPLAD
        try
        {
            User user = _logInManager.LogIn(email, passWord);
            return user;
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("Wrong email or password.");
            return null;
        }
    }
}