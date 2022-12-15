using CORE;
using LOGIC;
namespace DATABASE;
public class FriendsDB : IData<User>
{
    public int? Create(User obj)
    {   //om en skickar förfrågan så körs insert för den som skickar förfrågan.
        //om den andra svarar ja, så körs insert även för den som svarar ja.
        //på det viset blir båda satta som users_id1 och var och en har en relation med varandra
        //när man SVARAR JA på en friendrequest så kommer table uppdateras för båda till is_accepted = TRUE via update
        string query = 
        "INSERT INTO users_friends (users_id1, users_id2) VALUES(@userId, @friendId);";
        throw new NotImplementedException();
    }
    public int? Delete(User obj)
    {
        //däremot om en raderar relationen, så försvinner den från båda håll
        string query = "START TRANSACTION;"+
        "DELETE FROM TABLE users_friends WHERE users_id1 = @userId AND users_id2 = @friendId;" + 
        "DELETE FROM TABLE users_friends WHERE users_id1 = @friendId AND users_id2 = @userId;" +
        "COMMIT;";
        throw new NotImplementedException();
    }

    public List<User> GetAll() 
    {
        throw new NotImplementedException();
    }

    public List<User> GetById(int id, User user)
    {
        string query = "SELECT u.id, u.first_name, u.last_name FROM users u " +
                    "INNER JOIN users_friends uf ON uf.users_id2 = u.id " +
                    "WHERE uf.users_id1 = 23 AND uf.is_accepted = TRUE;";
        throw new NotImplementedException();
    }

    public int? Update(User obj) //uppdaterar till att man har accepterat förfrågan
    {
        string query = "START TRANSACTION;" + 
        "UPDATE users_friends SET is_accepted = TRUE WHERE users_id1 = @userId AND users_id2 = @friendId;" +
        "UPDATE users_friends SET is_accepted = TRUE WHERE users_id1 = @friendId AND users_id2 = @userId;"+
        "COMMIT;";
        throw new NotImplementedException();
    }
}