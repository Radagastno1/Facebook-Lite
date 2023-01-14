using CORE;
namespace LOGIC;
public class UserManager : IManager<User, User>, IUserManager
{
    IData<User> _userData;
    IUserData _userExtraData;
    IDataToObject<User,User> _userDataToObject;
    public Action<User> OnDelete;
    public UserManager(IData<User> userData, IUserData userExtraData, IDataToObject<User, User> userDataToObject)
    {
        _userData = userData;
        _userExtraData = userExtraData;

        _userDataToObject = userDataToObject;
    }
    public int? Create(User user)
    {
        return _userData.Create(user, QueryGenerator<User>.InsertQuery(user));
    }
    public List<User> GetBySearch(string name, User user)
    {
        List<User> foundUsers = _userExtraData.GetSearches(name);
        List<User> usersAvailable = new();
        foreach (User u in foundUsers)
        {
            User availableUser = _userDataToObject.GetOne(u.ID, user);
            if (availableUser != null)
            {
                usersAvailable.Add(availableUser);
            }
        }
        return usersAvailable;
    }
    public User GetOne(int id, User user)
    {
        // List<User> allUsers = _userData.GetAll();
        // User user = new();
        // foreach (User item in allUsers)
        // {
        //     if (item.ID == id)
        //     {
        //         user = item;
        //     }
        // }
        try
        {
            User foundUser = _userDataToObject.GetOne(id, user);
            return foundUser;
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
    public List<User> GetUsersById(List<int> ids, User user)
    {
        List<User> participants = new();
        foreach (int id in ids)
        {
            User participant = GetOne(id, user);
            participants.Add(participant);
        }
        return participants;
    }
    public int? Remove(User user)
    {
        return _userData.Delete(user, QueryGenerator<User>.DeleteQuery(user));
    }
    public int? Update(User user)
    {
        int? rows = _userData.Update(user, QueryGenerator<User>.UpdateQuery(user));
        if (rows > 0)
        {
            return rows;
        }
        else
        {
            return null;
        }
    }

    public List<User> GetAll(int data, User user)
    {
        throw new NotImplementedException();
    }

    public int? SetAsDeleted()
    {
        List<User> usersToDelete = _userExtraData.GetInactive();
        int usersToDeletedTable = 0;
        // if (usersToDelete == null) throw new InvalidOperationException("No users to delete");
        if (usersToDelete != null)
        {
            foreach (User item in usersToDelete)
            {
                OnDelete?.Invoke(item);
                usersToDeletedTable++;
            }
        }
        return usersToDeletedTable;
    }
}