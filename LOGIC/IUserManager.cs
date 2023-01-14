using CORE;
namespace LOGIC;
public interface IUserManager  //klar
{
    public List<User> GetUsersById(List<int> ids, User user);
     public int? SetAsDeleted();
}

