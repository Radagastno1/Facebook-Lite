using CORE;
namespace LOGIC;
public interface IUserData //BARA USERDB SOM IMPLEMENTERAR
{
    public List<User> GetSearches(string name);
    public List<User> GetInactive();
    public void UpdateToDeleted(User user);
}