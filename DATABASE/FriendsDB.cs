using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class FriendsDB : IFriendData<User>
{
    public int? CreateFriendRequest(User user, int friendId)
    {   //om en skickar förfrågan så körs insert för den som skickar förfrågan.
        //om den andra svarar ja, så körs insert även för den som svarar ja.
        //på det viset blir båda satta som users_id1 och var och en har en relation med varandra
        //när man SVARAR JA på en friendrequest så kommer table uppdateras för båda till is_accepted = TRUE via update
        using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        string query = "INSERT INTO users_friends (users_id1, users_id2) VALUES(@userId, @friendId);";
        return connection.ExecuteScalar<int>(query, new { @userId = user.ID, @friendId = friendId });
    }
    public int? DeleteFriendship(User user, int friendId)
    {
        //däremot om en raderar relationen, så försvinner den från båda håll
        using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        string query = "START TRANSACTION;" +
        "DELETE FROM TABLE users_friends WHERE users_id1 = @userId AND users_id2 = @friendId;" +
        "DELETE FROM TABLE users_friends WHERE users_id1 = @friendId AND users_id2 = @userId;" +
        "COMMIT;";
        throw new NotImplementedException();
    }

    public List<User> GetMyFriends(User user)
    {
        using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        string query = "SELECT u.id, u.first_name AS 'FirstNae', u.last_name AS 'LastName' FROM users u " +
                    "INNER JOIN users_friends uf ON u.id = uf.users_id2 " +
                    "WHERE uf.users_id1 = @userId AND uf.is_accepted = TRUE;";
        List<User> friends = connection.Query<User>(query, new { @userId = user.ID }).ToList();
        return friends;
    }
    public List<int> GetMyFriendRequests(User user)
    {
        //NÄSTA STEG, KOLLA OM MAN ÄR VÄN OCH ISÅFALL UPPDATERA TILL VÄNNER
        List<int> ids = new();
        using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        string query = "SELECT users_id2 " +
                "FROM users_friends  " +
                "WHERE users_id1 = @userId;";
        ids = connection.Query<int>(query, new { @userId = user.ID }).ToList();
        return ids;
    }
    public int CheckIfFriendAccepted(User user, int friendId)
    {
        try
        {
            using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
            string query = "SELECT id FROM users_friends WHERE users_id1 = @friendId AND users_id2 = @userId";
            int id = connection.QuerySingle<int>(query, new { @userId = user.ID, @friendId = friendId });
            return id;
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }

    public int CheckIfBefriended(User user, int friendId)
    {
        using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        string query = "SELECT id FROM users_friends WHERE users_id1 = @userId AND users_id2 = @friendId";
        int id = connection.QuerySingle<int>(query, new { @userId = user.ID, @friendId = friendId });
        return id;
    }
    public int? Update(User user, int friendId) //uppdaterar till att man har accepterat förfrågan
    {
        using MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        string query = "START TRANSACTION;" +
        "UPDATE users_friends SET is_accepted = TRUE WHERE users_id1 = @userId AND users_id2 = @friendId;" +
        "UPDATE users_friends SET is_accepted = TRUE WHERE users_id1 = @friendId AND users_id2 = @userId;" +
        "COMMIT;";
        int row = connection.ExecuteScalar<int>(query, new { @userId = user.ID, @friendId = friendId });
        return row;
    }
}