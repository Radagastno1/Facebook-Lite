using CORE;
using LOGIC;
namespace UI;
public class FriendsUI
{
    IFriendManager _friendManager;
    public FriendsUI(IFriendManager friendManager)
    {
        _friendManager = friendManager;
    }

    public static void ShowMyFriends(User user)
    {
        foreach(User friend in user.MyFriends)
        {
            Console.WriteLine(friend.ToString());
        }
    }
    public static bool IsFriends(User user, int friendId)
    {
        foreach(User friend in user.MyFriends)
        {
            if(friend.ID == friendId)
            {
                Console.WriteLine("You are friends!");
                return true;
            }
        }
        return false;
    }

    public void FriendRequest(User user, int friendId)
    {
        _friendManager.FriendRequest(user, friendId);
    }
}