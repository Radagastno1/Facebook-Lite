using CORE;
namespace LOGIC;
public class FriendManager
{
    IFriendData<User> _friendData;
    public FriendManager(IFriendData<User> friendData)
    {
        _friendData = friendData;
    }
    public void FriendRequest(User user, int friendId)
    {
        // hämta alla mina vänner
        List<User> friends = _friendData.GetMyFriends(user);
        if (friends.Where(friend => friend.ID == friendId).Count() < 1)
        {
            // om inte vi redan är vänner 
            // skicka vänförfrågan 
            _friendData.CreateFriendRequest(user, friendId);
            // en delegat som kollar om man är vänner än eller ej, som kör denna och ändrar till att det står att man är vän? 
            _friendData.CheckIfFriends(user, friendId);
        }

    }
    public List<User> GetMyFriends(User user)
    {
        List<User> friends = _friendData.GetMyFriends(user);
        return friends;
    }
}