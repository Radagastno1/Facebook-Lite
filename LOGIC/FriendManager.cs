using CORE;
namespace LOGIC;
public class FriendManager : IFriendManager
{
    IFriendData<User> _friendData;
    IRelationsData<User> _relationsData;
    public FriendManager(IFriendData<User> friendData, IRelationsData<User> relationsData)
    {
        _friendData = friendData;
        _relationsData = relationsData;
    }
    public bool FriendRequest(User user, int friendId)
    {
        try
        {
            _relationsData.Create(user, friendId);
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
            _relationsData.Update(user, id);
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
            user.MyFriends =  _relationsData.GetMine(user);
        }
        catch (InvalidOperationException)
        {
            user.MyFriends = new();
        }
    }
    public int Delete(User user, int friendId)
    {   //FELHANTERA
        return _relationsData.Delete(user, friendId);
    }
}