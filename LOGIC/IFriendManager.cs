using CORE;
namespace LOGIC;
public interface IFriendManager
{
    public bool FriendRequest(User user, int friendId);
    public bool CheckIfBefriended(User user, int friendId);
    public bool IsFriendRequestWaiting(User user, int friendId);
    public void SetToFriends(User user);
        public void LoadMyFriends(User user);
        public int Delete(User user, int id);
}