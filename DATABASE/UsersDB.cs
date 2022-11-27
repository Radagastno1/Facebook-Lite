using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class UsersDB : IData<User>
{
    public int Create(User obj)
    {
        int userId = 0;
        string query = "INSERT INTO users(first_name, last_name, email, pass_word, birth_date, gender, about_me) " +
                       "VALUES(@FirstName, @LastName, @Email, @PassWord, @BirthDate, @Gender, @AboutMe);SELECT LAST_INSERT_ID();";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            userId = con.ExecuteScalar<int>(query, param: obj);
        }
        return userId;
    }
    public int Delete(User obj)
    {
        int rowsEffected = 0;
        string query = "Delete from users WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<User> Get()
    {
        List<User> users = new();
        string query = "SELECT users.id, first_name as 'FirstName', last_name as 'LastName', email," +
        "pass_word as 'PassWord', birth_date as 'BirthDate', gender, about_me as 'AboutMe', roles.name as 'Role' " +
        "FROM users LEFT JOIN users_roles ON users_roles.users_id = users_id " +
        "LEFT JOIN roles ON roles.id = users_roles.roles_id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            users = con.Query<User>(query).ToList();
        }
        return users;
    }
    public int Update(User obj)
    {
        int rowsEffected = 0;
        string query = "Update users SET first_name = @FirstName, last_name = @LastName " +
        "email = @Email, pass_word = @PassWord, birth_date = @BirthDate, gender = @Gender " +
        "about_me = @AboutMe WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public User GetById()
    {
        throw new NotImplementedException();
    }
}