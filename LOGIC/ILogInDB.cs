namespace LOGIC;
public interface ILogInDB<T>
{
    public T GetMemberByLogIn(string email, string passWord);
}