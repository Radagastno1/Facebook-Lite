using CORE;
namespace LOGIC;
public class BlockingManager : IBlockingsManager<User>
{
    IRelationsData<User> _relationsData;
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
                blockedId = _relationsData.Create(user, id);
            }
            catch(InvalidOperationException)
            {
                blockedId = 0;
            }
        }
        return blockedId;
    }

    public List<User> GetMyBlockedUsers(User obj)
    {
        throw new NotImplementedException();
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