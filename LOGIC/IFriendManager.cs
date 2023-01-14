namespace LOGIC;
public interface IFriendManager<T> //friendmanager implementerar denna
{
    public bool IsFriendRequestWaiting(T obj, int friendId);
     public bool IsBefriended(T obj, int friendId);
     public void LoadFriends(T obj);
}