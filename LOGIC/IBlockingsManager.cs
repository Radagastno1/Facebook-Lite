namespace LOGIC;
public interface IBlockingsManager<T>
{
    public int BlockUser(T obj, int id);
    public int UnBlockUser(T obj, int id);
    public List<T> GetMyBlockedUsers(T obj);
    public int Update(T obj, int id);
}