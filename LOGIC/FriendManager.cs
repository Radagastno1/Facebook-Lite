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
        catch(InvalidOperationException)
        {
            return false;
        }
        // // en delegat som kollar om man är vänner än eller ej, som kör denna och ändrar till att det står att man är vän? 
        // if (_friendData.CheckIfFriendAccepted(user, friendId) > 0)
        // {
        //     return true;
        // }
        // else return false;
    }
    public int CheckIfBefriended(User user, int friendId)
    {
        try
        {
            return _friendData.CheckIfBefriended(user, friendId);
        }
        catch (InvalidOperationException)
        {
            return 0;
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