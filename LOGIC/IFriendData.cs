namespace LOGIC;
public interface IFriendData<T>
{
    public int? CreateFriendRequest(T obj, int id);
    public int? DeleteFriendship(T obj, int id);
    public List<T> GetMyFriends(T obj);
    public List<int> GetMyFriendRequests(T obj);
    public int CheckIfFriendAccepted(T obj, int friendId);
    public int CheckIfBefriended(T obj, int friendId);
    public int? Update(T obj, int id);
}