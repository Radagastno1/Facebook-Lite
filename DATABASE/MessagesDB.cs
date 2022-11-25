using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class MessagesDB : IData<Message>
{
    public int Create(Message obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public int Delete(Message obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<Message> Get()
    {
        List<Message> messages = new();
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            messages = con.Query<Message>(query).ToList();
        }
        return messages;
    }
    public int Update(Message obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public Message GetById()
    {
        throw new NotImplementedException();
    }
}