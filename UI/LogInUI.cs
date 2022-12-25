using CORE;
using LOGIC;
using DATABASE; //UI måste känna till database pga loginmanager? egentligen inte i lök va?
namespace UI;
public class LogInUI
{
    ILogInManager<User> _logInManager;
      public Action<User> OnLoggedIn;
    public LogInUI(ILogInManager<User> logInManager)
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
            OnLoggedIn?.Invoke(user);
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