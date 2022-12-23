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
        _friendData.CreateFriendRequest(user, friendId);
        // en delegat som kollar om man är vänner än eller ej, som kör denna och ändrar till att det står att man är vän? 
        if (_friendData.CheckIfFriends(user, friendId).Count() == 2)
        {
            return true;
        }
        else return false;
    }
    public int CheckIfBefriended(User user, int friendId)
    {
        return _friendData.CheckIfBefriended(user, friendId);
    }
    public List<User> GetMyFriends(User user)
    {
        user.MyFriends = _friendData.GetMyFriends(user);
        return user.MyFriends;
    }
}