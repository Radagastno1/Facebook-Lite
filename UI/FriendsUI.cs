using CORE;
using LOGIC;
namespace UI;
public class FriendsUI
{
    IRelationsManager<User> _relationsManager;
    IFriendManager<User> _friendManager;
    public Action<User> LoadFriends;
    static Dictionary<int, string> FriendRequestStatus = new Dictionary<int, string>()
    {
        [1] = "[A] Add friend]",
        [2] = "[Friend request sent]",
        [3] = "[Confirm friend request]",
        [4] = "[Friends]"
    };
    public FriendsUI(IRelationsManager<User> relationsManager, IFriendManager<User> friendManager, User user)
    {
        _relationsManager = relationsManager;
        _friendManager = friendManager;
        LoadFriends?.Invoke(user);
    }
    public void ShowMyFriends(User user)
    {
        foreach (User friend in user.MyFriends)
        {
            Console.WriteLine(friend.ToString());
        }
    }
    public bool IsFriends(User user, int friendId)
    {
        foreach (User friend in user.MyFriends)
        {
            if (friend.ID == friendId)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsFriendRequestSent(User user, int friendId)
    {
        if (!_friendManager.IsBefriended(user, friendId))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void FriendRequest(User user, int friendId)
    {
        _relationsManager.Create(user, friendId);
    }
    public int GetFriendShipStatus(User user, int friendId)
    {
        int status = 0;
        if (IsFriends(user, friendId))
        {
            status = 4;
        }
        else if (_friendManager.IsFriendRequestWaiting(user, friendId))
        {
            status = 3;
        }
        else if (IsFriendRequestSent(user, friendId))
        {
            status = 2;
        }
        else
        {
            status = 1;
        }
        return status;
    }
    public string ShowFriendShipStatus(int id, User user)
    {
        int status = GetFriendShipStatus(user, id);
        string statusString = string.Empty;
        switch (status)
        {
            case 2:
                statusString = "[FriendRequest Sent]";
                break;
            case 3:
                statusString = "[Confirm Request]";
                break;
            case 4:
                statusString = "[Friends]";
                break;
        }
        return statusString;
    }
    public void DeleteFriendship(User user, int friendId)
    {
        ConsoleKey answerKey = ConsoleInput.GetPressedKey("Delete friendship? Y/N", LogicTool.NewKeyList(ConsoleKey.Y, ConsoleKey.N));
        if (answerKey == ConsoleKey.Y) _relationsManager.Delete(user, friendId);
        else return;
    }
}
