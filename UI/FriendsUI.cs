using CORE;
namespace UI;
public class FriendsUI
{
    public static void ShowMyFriends(User user)
    {
        foreach(User friend in user.MyFriends)
        {
            Console.WriteLine(friend.ToString());
        }
    }
}