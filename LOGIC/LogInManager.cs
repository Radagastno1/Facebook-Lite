using CORE;
namespace LOGIC;
public class LogInManager
{
    IData<User> _userData;

    public LogInManager(IData<User> userData)
    {
        _userData = userData;
    }

    public User? UserLogIn(User user)
    {
        List<User>allUsers = _userData.Get();
        foreach(User item in allUsers)
        {
            if(item.Email == user.Email && item.PassWord == user.PassWord) return user;
        }
        return null;
    }
}