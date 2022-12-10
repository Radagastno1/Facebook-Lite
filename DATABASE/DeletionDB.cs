using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;

namespace DATABASE;

public class DeletionDB : IData<User>
{
    //CHECK IF USERS SHOULD MOVE TO DELETED_USERS TABLE AND BE DELETED FROM THE USERS TABLE!! :

    // SELECT u.id, u.first_name, u.last_name, u.email, u.pass_word,
    // u.birth_date, u.gender, u.about_me, (u.date_inactive + 30) as date_check
    // FROM users u WHERE @date_check < CURRENT_DATE();
    public int? Create(User user)
    {
       throw new NotImplementedException();
    }
    public int? Delete(User obj)
    {
        //vad används denna till hmmm
        throw new NotImplementedException();
    }
    public List<User> Get()
    { //hämta users som ska läggas in i deleted table, som har varit inaktiva i mer än 30 dagar
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
    public int? Update(User obj)
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

                rows = con.ExecuteScalar<int>(query, param : obj);

            return rows;
        }
        catch (InvalidOperationException)
        {
            return null;
        }throw new NotImplementedException();
    }
}
