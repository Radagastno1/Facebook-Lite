namespace LOGIC;
public interface IFriendData<T>
{
    public List<int> GetMyFriendRequests(T obj);
    public int CheckIfFriendAccepted(T obj, int friendId);
    public int CheckIfBefriended(T obj, int friendId);
}