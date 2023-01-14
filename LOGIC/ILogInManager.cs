namespace LOGIC;
public interface ILogInManager<T>  //loginmanager implementerar, denna är specifik för vad den gör iaf
{
    public T? LogIn(string email, string password);
}