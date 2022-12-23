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
        // hämta alla mina vänner
        List<User> friends = _friendData.GetMyFriends(user);
        if (friends.Where(friend => friend.ID == friendId).Count() < 1)
        {
            // om inte vi redan är vänner 
            // skicka vänförfrågan 
            _friendData.CreateFriendRequest(user, friendId);
            // en delegat som kollar om man är vänner än eller ej, som kör denna och ändrar till att det står att man är vän? 
            if (_friendData.CheckIfFriends(user, friendId).Count() == 2)
            {
                return true;
            }
        }
        return false;
    }
    public List<User> GetMyFriends(User user)
    {
        user.MyFriends = _friendData.GetMyFriends(user);
        return user.MyFriends;
    }
}