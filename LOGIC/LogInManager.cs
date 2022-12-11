using CORE;
namespace LOGIC;
public class LogInManager : ILogInManager<User>  
{
    IData<User> _userData;

    public LogInManager(IData<User> userData)
    {
        _userData = userData;
    }
    public User? LogIn(User user)
    {
        List<User> allUsers = _userData.GetAll();
        foreach (User item in allUsers)
        {
            if (item.Email == user.Email && item.PassWord == user.PassWord)
            {
                return item;
            }
        }
        return null;
    }
}