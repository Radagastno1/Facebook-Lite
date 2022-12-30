using CORE;
namespace LOGIC;
public class BlockingManager : IRelationsManager<User>
{
    IRelationsData<User> _relationsData;
    public Func<User, int, int> OnBlockUser;
    public BlockingManager(IRelationsData<User> relationsData)
    {
        _relationsData = relationsData;
    }
    public int Create(User user, int id)
    {
        int blockedId = 0;
        if (user.ID != id)
        {
            try
            {
                OnBlockUser?.Invoke(user, id);
                blockedId = _relationsData.Create(user, id);
            }
            catch (InvalidOperationException)
            {
                blockedId = 0;
            }
        }
        return blockedId;
    }

    public List<User> GetMine(User user)
    {
        try
        {
            return _relationsData.GetMine(user);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public int Delete(User obj, int id)  //unblock user
    {
        throw new NotImplementedException();
    }
    public void Update(User obj)
    {
        throw new NotImplementedException();
    }
}