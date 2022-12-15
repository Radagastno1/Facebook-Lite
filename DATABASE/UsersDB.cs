using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class UsersDB : IData<User,User>, IDataSearcher<User>, IDeletionData<User>
{
    //1.fixa hur det ska se ut överallt om man är inaktiv
    //namn ska synas som deleted user? ingen publik information osv 
    public int? Create(User obj)  //IDATA
    {
        int userId = 0;
        string query = "START TRANSACTION;" +
        "INSERT INTO users(first_name, last_name, email, pass_word, birth_date, gender, about_me) " +
        "VALUES(@FirstName, @LastName, @Email, @PassWord, @BirthDate, @Gender, @AboutMe); " +
        "SET @usersId := LAST_INSERT_ID(); " +
        "INSERT INTO users_roles (users_id, roles_id) VALUES(@usersId, 5);" +
        "COMMIT; SELECT @users_id;";
        try
        {
            using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;Allow User Variables=true;"))

                userId = con.ExecuteScalar<int>(query, param: obj);

            return userId;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
    public int? Delete(User obj)  //IDATA
    {
        int rowsEffected = 0;
        string query = "START TRANSACTION;" +
        "UPDATE users SET is_active = false WHERE id = @id;" + 
        "UPDATE messages SET is_visible = FALSE WHERE sender_id = @id;" + 
        "UPDATE posts SET is_visible = FALSE WHERE users_id = @id;" + 
        "COMMIT;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<User> GetAll()  //IDATA
    {
        List<User> users = new();
        string query =
        "SELECT u.id, u.first_name as 'FirstName', u.last_name as 'LastName', u.email," +
        "u.pass_word as 'PassWord', DATE_FORMAT(u.birth_date, '%Y-%m-%d') as 'BirthDate', u.gender, u.about_me as 'AboutMe', r.name as 'Role' " +
        "FROM users u LEFT JOIN users_roles ur ON ur.users_id = u.id " +
        "LEFT JOIN roles r ON r.id = ur.roles_id WHERE u.is_deleted = false;";
        //NU kan man ej logga in på sitt konto om man är inaktiv för att testa detta
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            users = con.Query<User>(query).ToList();
        }
        return users;
    }
    public int? Update(User user)  //IDATA
    {
        int rowsEffected = 0;
        string query = "Update users SET first_name = @FirstName, last_name = @LastName, " +
        "email = @Email, pass_word = @PassWord, gender = @Gender, " +
        "about_me = @AboutMe WHERE id = @ID;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: user);
        }
        return rowsEffected;
    }
    public List<User> GetSearches(string name)
    {
        List<User> foundUsers = new();
        string query = "SELECT u.id as 'ID', u.first_name as 'FirstName', u.last_name as 'LastName', " +
        "u.birth_date as 'BirthDate', u.gender, u.about_me as 'AboutMe' " +
        "FROM users u LEFT JOIN users_roles ur ON ur.users_id = u.id " +
        "LEFT JOIN roles r ON r.id = ur.roles_id " +
        $"WHERE u.first_name LIKE '%{name}%' AND r.id = 5 AND u.is_active = true;";
        //DENNA METODEN BEHÖVER TA IN EN USER SÅ MAN KAN KOLLA ENS ID ANG USERS_BLOCKED
        //SAMT FIXA QUERYN SÅ DEN INNEHÅLLER SLUTET PÅ QUERYN UNDER HÄR:
        using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        foundUsers = con.Query<User>(query, new { @name = name }).ToList();
        return foundUsers;
    }
    public List<User> GetById(int id, User user)
    {
        string query = "SELECT u.id, u.first_name as 'FirstName', u.last_name as 'LastName', "+
        "DATE_FORMAT(u.birth_date, '%Y-%m-%d') as 'BirthDate', u.gender, " + 
        "u.about_me as 'AboutMe', r.name as 'Role' " + 
        "FROM users u LEFT JOIN users_roles ur ON ur.users_id = u.id " + 
        "LEFT JOIN roles r ON r.id = ur.roles_id WHERE u.is_deleted = false " + 
        "AND u.id not in (select blocked_user_id from users_blocked where users_id = 23) " + 
        "AND u.id not in (select users_id FROM users_blocked where blocked_user_id = 23);";
        throw new NotImplementedException();
    }

    public List<User> GetInactive()
    {
        List<User> users = new();
        string query = "SELECT u.id as 'Id', DATE_ADD(u.date_inactive, interval 30 day) " +
        "as deletingdate FROM users u  WHERE DATE_ADD(u.date_inactive, interval 30 day) < CURRENT_DATE() " +
        "AND is_deleted = false;";
        try
        {
            using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;Allow User Variables=true;"))

                users = con.Query<User>(query).ToList();

            return users;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public int? UpdateToDeleted(User user)
    {
        //uppdatera något? vet ej
        string query = "START TRANSACTION; " +
        "UPDATE users SET is_deleted = true WHERE id = @Id; " +
        "UPDATE messages SET is_visible = false WHERE sender_id = @Id;" +
        "COMMIT;";
        int rows = 0;
        try
        {
            using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;Allow User Variables=true;"))

                rows = con.ExecuteScalar<int>(query, param: user);

            return rows;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}