namespace LOGIC;
public interface ILogInDB<T>
{
    public T GetMemberByLogIn(T obj);
}