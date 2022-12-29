namespace LOGIC;
public interface IFriendManager<T>
{
    public bool IsFriendRequestWaiting(T obj, int friendId);
     public bool IsBefriended(T obj, int friendId);
}