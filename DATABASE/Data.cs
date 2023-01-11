using Dapper;
using MySqlConnector;
using LOGIC;
using CORE;
namespace DATABASE;
public class Data<Tone> : IData<Tone>
{
    public int? Create(Tone obj, string query)
    {
        try
        {
            using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
            int num = con.ExecuteScalar<int>(query, param: obj);
            return num;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
    public int? Delete(Tone obj, string query)
    {
        int rowsEffected = 0;
        try
        {
            using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
            return rowsEffected;
        }
        catch (InvalidOperationException)
        {
            return rowsEffected;
        }
    }
    public List<Tone> GetAll(User user, string query)
    { //param obj är tex för att kolla mot user (id) som kommer in som two obj  ev göra till user bara?
        try
        {
            using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
            List<Tone> objects = con.Query<Tone>(query, param: user).ToList();
            return objects;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
    public int? Update(Tone obj, string query)
    {
        try
        {
            using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
            int rowsEffected = con.ExecuteScalar<int>(query, param: obj);
            return rowsEffected;
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
}