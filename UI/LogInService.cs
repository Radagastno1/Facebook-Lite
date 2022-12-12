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
        try
        {
            User user = _logInManager.LogIn(email, passWord);
            return user;
        }
        catch(Exception e)
        {
            Console.WriteLine("Wrong email or password." + e.Message);
            Console.ReadLine();
            return null;
        }
    }
}