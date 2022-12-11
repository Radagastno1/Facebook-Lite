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
        User user = new();
        user.Email = ConsoleInput.GetEmail("Email: ");
        user.PassWord = ConsoleInput.GetPassword("Password: ");
        LogInManager logInManager = new(new UsersDB()); //DENNA SKA HA EN INTERFACE, NU HÅRDKOPPLAD
        try
        {
            user = _logInManager.LogIn(user);
            return user;
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("Wrong email or password.");
            return null;
        }
    }
}