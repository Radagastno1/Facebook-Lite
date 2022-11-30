using CORE;
namespace LOGIC;
public class UserManager : IManager<User>
{
    IData<User> _userData;
    public UserManager(IData<User> userData)
    {
        _userData = userData;
    }
    //hämta mina posts - usermanager
    //hämta mina medd - messagemanager
    //radera post - usermanager
    //radera medd - usermanager
    int? IManager<User>.Create(User user)
    {
        return _userData.Create(user);
    }
    public List<User> GetBySearch(string search)
    {
         List<User> searchedUsers = new();
        try
        {
            List<User> allUsers = _userData.Get();
            foreach (User user in allUsers)
            {
                if (user.FirstName.ToLower().Contains(search.ToLower()) || user.LastName.ToLower().Contains(search.ToLower()))
                {
                    searchedUsers.Add(user);
                }
            }
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e);
        }
        return searchedUsers;
    }
    public User GetOne(int id, int data2)
    {
        List<User> allUsers = _userData.Get();
        User user = new();
        foreach(User item in allUsers)
        {
            if(item.ID == id)
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
       if(rows > 0)
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