using CORE;
namespace LOGIC;
public class UserManager : IManager<User>, IDeletionManager<User>
{
    IData<User> _userData;
    IDataSearcher<User> _dataSearcher;
    IDeletionData<User> _deletionData;
    public UserManager(IData<User> userData, IDataSearcher<User> dataSearcher, IDeletionData<User> deletionData)
    {
        _userData = userData;
        _dataSearcher = dataSearcher;
        _deletionData = deletionData;
    }
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
    public User GetOne(int id)
    {
        List<User> allUsers = _userData.GetAll();
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

    public int? SetAsDeleted()
    {
        List<User> usersToDelete = _deletionData.GetInactive();
        int usersToDeletedTable = 0;
        if (usersToDelete != null)
        {
            foreach (User item in usersToDelete)
            {
                _deletionData.UpdateToDeleted(item);
                usersToDeletedTable++;
            }
        }
        return usersToDeletedTable;
    }
}