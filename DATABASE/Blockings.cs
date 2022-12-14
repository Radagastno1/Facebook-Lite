using CORE;
using LOGIC;
namespace DATABASE;
public class Blocking : IData<User,User>
{
    public int? Create(User obj)
    {
        string query = "INSERT INTO users_blocked (users_id, blocked_user_id) VALUES(@userId, @blockedUserId);";
        throw new NotImplementedException();
    }

    public int? Delete(User obj)
    {
        string query = "DELETE FROM users_blocked WHERE users_id = @userId AND blocked_user_id = @blockedId";
        throw new NotImplementedException();
    }

    public List<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<User> GetById(int id, User user)
    {
        string query = "SELECT u.id, u.first_name, u.last_name FROM users u " +
        "INNER JOIN users_blocked ub ON u.id = ub.blocked_user_id " + 
        "WHERE ub.users_id = @userId;";
        throw new NotImplementedException();
    }

    public int? Update(User obj)
    {
        throw new NotImplementedException();
    }
        //THIS NEEDS TO BE ADDED IN QUERY WHERE SELECTING MESSAGES, CONVERSATIONS, POSTS, COMMENTS ETC:
        //WHERE users_id not in 
        // (select blocked_user_id from users_blocked where users_id = 22);
}