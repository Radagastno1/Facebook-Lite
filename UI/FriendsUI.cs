using CORE;
using LOGIC;
namespace UI;
public class FriendsUI
{
    IFriendManager _friendManager;
    // public Action<User> OnFriendUI;
    public FriendsUI(IFriendManager friendManager, User user)
    {
        _friendManager = friendManager;
        // OnFriendUI?.Invoke(user);
    }
    public static void ShowMyFriends(User user)
    {
        foreach (User friend in user.MyFriends)
        {
            Console.WriteLine(friend.ToString());
        }
    }
    public static bool IsFriends(User user, int friendId)
    {
        foreach (User friend in user.MyFriends)
        {
            if (friend.ID == friendId)
            {
                Console.WriteLine("You are friends!");
                return true;
            }
        }
        return false;
    }
    public bool IsFriendRequestSent(User user, int friendId)
    {
        if (_friendManager.CheckIfBefriended(user, friendId) == null || _friendManager.CheckIfBefriended(user, friendId) < 1)
        {
            return false;
        }
        else
        {
            Console.WriteLine("Friendrequest sent!");
            return true;
        }
    }

    public void FriendRequest(User user, int friendId)
    {
        if (_friendManager.FriendRequest(user, friendId)) Console.WriteLine("You are friends!");
    }
}