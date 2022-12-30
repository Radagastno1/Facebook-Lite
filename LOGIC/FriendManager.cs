using CORE;
namespace LOGIC;
public class FriendManager : IRelationsManager<User>, IFriendManager<User>
{
    IFriendData<User> _friendData;
    IRelationsData<User> _relationsData;
    public FriendManager(IFriendData<User> friendData, IRelationsData<User> relationsData)
    {
        _friendData = friendData;
        _relationsData = relationsData;
    }
    public int Create(User user, int friendId)
    {
        try
        {
            _relationsData.Create(user, friendId);
            return 1;
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }
    public bool IsBefriended(User user, int friendId)
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
    public void Update(User user)
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
    public List<User> GetMine(User user)
    {
        try
        {
            return _relationsData.GetMine(user);
        }
        catch (InvalidOperationException)
        {
            List<User> users= new();
            return users;
        }
    }
    public void LoadFriends(User user)
    {
        user.MyFriends.Clear();
        user.MyFriends = GetMine(user);
    }
    public int Delete(User user, int friendId)
    {   //FELHANTERA
        return _relationsData.Delete(user, friendId);
    }
}