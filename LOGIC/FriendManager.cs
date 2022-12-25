using CORE;
namespace LOGIC;
public class FriendManager : IFriendManager
{
    IFriendData<User> _friendData;
    public FriendManager(IFriendData<User> friendData)
    {
        _friendData = friendData;
    }
    public bool FriendRequest(User user, int friendId)
    {
        try
        {
            _friendData.CreateFriendRequest(user, friendId);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
    public bool CheckIfBefriended(User user, int friendId)
    {
        try
        {
            if (_friendData.CheckIfBefriended(user, friendId) > 0)
            {
                return true;
            }
            else return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
    public void SetToFriends(User user)
    {
        List<int> friendRequestsIds = _friendData.GetMyFriendRequests(user);
        List<int> friendsToBeAccepted = new();
        foreach (int id in friendRequestsIds)
        {
            if (_friendData.CheckIfFriendAccepted(user, id) < 1)
            {
                break;
            }
            else
            {
                friendsToBeAccepted.Add(id);
            }
        }
        foreach (int id in friendsToBeAccepted)
        {
            _friendData.Update(user, id);
        }
    }
    public bool IsFriendRequestWaiting(User user, int friendId)
    {
        try
        {
            if(_friendData.CheckIfFriendAccepted(user, friendId) > 0) return true;
            else return false;
        }
        catch(InvalidOperationException)
        {
            return false;
        }
    }
    public void LoadMyFriends(User user)
    {
        try
        {
            user.MyFriends = _friendData.GetMyFriends(user);
        }
        catch (InvalidOperationException)
        {
            user.MyFriends = new();
        }
    }
}