using CORE;
namespace LOGIC;
public class UserManager : IManager<User>
{
    IData<User> _userData;
    IDataSearcher<User> _dataSearcher;
    public UserManager(IData<User> userData, IDataSearcher<User> dataSearcher)
    {
        _userData = userData;
        _dataSearcher = dataSearcher;
    }
    //hämta mina posts - usermanager
    //hämta mina medd - messagemanager
    //radera post - usermanager
    //radera medd - usermanager
    public int? Create(User user)
    {
        return _userData.Create(user);
    }
    public List<User> GetBySearch(string name)
    {
        List<User> searchedUsers = new();
        searchedUsers = _dataSearcher.GetSearches(name);
        return searchedUsers;
    }
    public User GetOne(int id, int data2)
    {
        List<User> allUsers = _userData.Get();
        User user = new();
        foreach (User item in allUsers)
        {
            if (item.ID == id)
            {
                user = item;
            }
        }
        return user;
    }
    public int? Remove(User user)
    {
        return _userData.Delete(user);
    }
    public int? Update(User user)
    {
        int? rows = _userData.Update(user);
        if (rows > 0)
        {
            return rows;
        }
        else
        {
            return null;
        }
    }

    public List<User> GetAll(int data)
    {
        throw new NotImplementedException();
    }
}