namespace LOGIC;
public interface ILogInDB<T>  //logindb, också tydlig vad  syftet är
{
    public T GetMemberByLogIn(string email, string passWord);
    public int UpdateToActivated(int id);
}