using CORE;
namespace LOGIC;
public class UserManager : IManager<User, User>, IDeletionManager<User>, IMultipleDataGetter<User, int>
{
    IData<User> _userData;
    IDataSearcher<User> _dataSearcher;
    IDeletionData<User> _deletionData;
    IDataToObject<User> _userDataToObject;
    public Action<User> OnDelete;

    public UserManager(IData<User> userData, IDataSearcher<User> dataSearcher, IDeletionData<User> deletionData, IDataToObject<User> userDataToObject)
    {
        _userData = userData;
        _dataSearcher = dataSearcher;
        _deletionData = deletionData;
        _userDataToObject = userDataToObject;
    }
    public int? Create(User user)
    {
        return _userData.Create(user);
    }
    public List<User> GetBySearch(string name, User user)
    {
        List<User> foundUsers = _dataSearcher.GetSearches(name);
        List<User> usersAvailable = new();
        foreach (User u in foundUsers)
        {
            User availableUser = _userDataToObject.GetById(u.ID, user);
            if (availableUser != null)
            {
                usersAvailable.Add(availableUser);
            }
        }
        return usersAvailable;
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
    public List<User> GetUsersById(List<int> ids)
    {
        List<User> participants = new();
        foreach (int id in ids)
        {
            User participant = GetOne(id);
            participants.Add(participant);
        }
        return participants;
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

    public List<User> GetAll(int data, User user)
    {
        throw new NotImplementedException();
    }

    public int? SetAsDeleted()
    {
        List<User> usersToDelete = _deletionData.GetInactive();
        int usersToDeletedTable = 0;
        // if (usersToDelete == null) throw new InvalidOperationException("No users to delete");
        if(usersToDelete != null)
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