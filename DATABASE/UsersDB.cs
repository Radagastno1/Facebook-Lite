using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class UsersDB : IData<User>
{
    public int Create(User obj)
    {
        int rowsEffected = 0;
        string query = "INSERT INTO users(first_name, last_name, email, pass_word, birth_date, gender, about_me) " +
                       "VALUES(@FirstName, @LastName, @Email, @PassWord, @BirthDate, @Gender, @AboutMe)";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public int Delete(User obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<User> Get()
    {
        List<User> users = new();
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            users = con.Query<User>(query).ToList();
        }
        return users;
    }
    public int Update(User obj)
    {
        int rowsEffected = 0;
        string query = "";
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