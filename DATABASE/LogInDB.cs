using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class LogInDB : ILogInDB<User>
{
    public User GetMemberByLogIn(User user)
    {
        //OM DENNA SKA ANVÄNDAS SÅ ÄNDRA JOINEN
        string query = "SELECT id, first_name as 'FirstName', last_name as 'LastName' " + 
        "email, pass_word as 'PassWord', birth_date as 'BirthDate', gender, about_me as 'AboutMe' " +
        "INNER JOIN roles r ON r.id = u.users_roles_id " +
        "FROM users u WHERE email = @Email AND pass_word = @PassWord AND r.name = 'Member';";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            user = con.QuerySingle<User>(query, param : user);
        }
        return user;
    }
}