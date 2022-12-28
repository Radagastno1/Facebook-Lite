using CORE;
namespace LOGIC;
public class BlockingManager : IBlockingsManager<User>
{
    IRelationsData<User> _relationsData;
    public Func<User, int, int> OnBlockUser;
    public BlockingManager(IRelationsData<User> relationsData)
    {
        _relationsData = relationsData;
    }
    public int BlockUser(User user, int id)
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

    public List<User> GetMyBlockedUsers(User user)
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

    public int UnBlockUser(User obj, int id)
    {
        throw new NotImplementedException();
    }

    public int Update(User obj, int id)
    {
        throw new NotImplementedException();
    }
}