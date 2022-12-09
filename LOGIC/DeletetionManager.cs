using CORE;
namespace LOGIC;
public class DeletionManager : IManager<User>
{
    IData<User> _userData;
    public DeletionManager(IData<User> userData)
    {
        _userData = userData;
    }
    public int? Create(User obj)
    {
        List<User> usersToDelete = _userData.Get();
        int usersToDeletedTable = 0;
        if (usersToDelete != null)
        {
            foreach(User item in usersToDelete)
            {
                _userData.Create(item);
                usersToDeletedTable++;
            }
        }
        return usersToDeletedTable;
    }

    public List<User> GetAll(int data) //get all users som ska till deleted_users tables
    {
        throw new NotImplementedException();
    }

    public List<User> GetBySearch(string search)
    {
        throw new NotImplementedException();
    }

    public User GetOne(int data1, int data2)
    {
        throw new NotImplementedException();
    }

    public int? Remove(User obj)
    {
        throw new NotImplementedException();
    }

    public int? Update(User obj)
    {
        throw new NotImplementedException();
    }
}