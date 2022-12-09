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
    public int? Create(User obj)
    {
        //lägg till på deleted_user table
        throw new NotImplementedException();
    }

    public int? Delete(User obj)
    {
        //om man nu behöver deleta därifrån?
        throw new NotImplementedException();
    }

    public List<User> Get()
    { //hämta users som ska läggas in i deleted table
        List<User>users = new();
        string query = "SELECT u.id, u.first_name, u.last_name, u.email, u.pass_word, " +
         "u.birth_date, u.gender, u.about_me, (u.date_inactive + 30) as date_check " +
         "FROM users u WHERE @date_check < CURRENT_DATE();";
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
        throw new NotImplementedException();
    }
}
