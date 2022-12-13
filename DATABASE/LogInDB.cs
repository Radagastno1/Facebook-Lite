using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class LogInDB : ILogInDB<User>
{
    public User GetMemberByLogIn(string email, string passWord)
    {
        User user = new();
        string query = 
        "SELECT u.id as 'Id', u.first_name as 'FirstName', u.last_name as 'LastName', " +
        "u.email as 'Email', u.pass_word as 'PassWord', u.birth_date as 'BirthDate', u.gender as 'Gender', u.about_me as 'AboutMe' " +
        "FROM users u " +
        "INNER JOIN users_roles ur ON ur.users_id = u.id " +
        "INNER JOIN roles r ON ur.roles_id = r.id " +
        "WHERE email = @Email AND pass_word = @PassWord AND r.name = 'Member' AND is_deleted = FALSE;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;Allow User Variables=True;"))
        {
            user = con.QuerySingle<User>(query, new { @Email = email, @PassWord = passWord });
        }
        return user;
    }
    public int UpdateToActivated(int userId)
    {
        int rows = 0;
        string query = "START TRANSACTION;" +
        "UPDATE users SET is_active = TRUE WHERE id = @id;" +
        "UPDATE messages SET is_visible = TRUE WHERE sender_id = @id; " +
        "UPDATE posts SET is_visible = TRUE WHERE users_id = @id; " +
        "COMMIT;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;Allow User Variables=True;"))
        {
            rows = con.ExecuteScalar<int>(query, new{@id = userId});
        }
        return rows;   //returnerar ej rows
    }
}