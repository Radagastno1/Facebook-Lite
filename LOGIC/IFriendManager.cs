using CORE;
namespace LOGIC;
public interface IFriendManager
{
    public bool FriendRequest(User user, int friendId);
    public int CheckIfBefriended(User user, int friendId);
}