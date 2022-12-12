namespace LOGIC;
public interface ILogInManager<T>
{
    public T? LogIn(string email, string password);
}