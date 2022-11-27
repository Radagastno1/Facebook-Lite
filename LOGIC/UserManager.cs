using CORE;
namespace LOGIC;
public class UserManager : IManager<User>, IManager<Post>
{
    IData<User> _userData;
    IData<Post> _postData;
    public UserManager(IData<User> userData, IData<Post> postData)
    {
        _userData = userData;
        _postData = postData;
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
                if (user.FirstName.Contains(search) || user.LastName.Contains(search))
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
    public User GetOne()
    {
        throw new NotImplementedException();
    }
    public int? Remove(User user)
    {
        return _userData.Delete(user);
    }
    public User Update()
    {
        throw new NotImplementedException();
    }

    int? IManager<Post>.Create(Post obj)
    {
        throw new NotImplementedException();
    }

    List<Post> IManager<Post>.GetBySearch(string search)
    {
        throw new NotImplementedException();
    }

    int? IManager<Post>.Remove(Post obj)
    {
        throw new NotImplementedException();
    }

    Post IManager<Post>.GetOne()
    {
        throw new NotImplementedException();
    }

    Post IManager<Post>.Update()
    {
        throw new NotImplementedException();
    }
}