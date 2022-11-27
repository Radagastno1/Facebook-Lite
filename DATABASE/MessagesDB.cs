using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class MessagesDB : IData<Message>
{
    public int? Create(Message obj)
    { 
        int messageId = 0;
        string query = "START TRANSACTION " +
        "INSERT INTO conversations(creator_id) VALUES(@Id);" +
        "SET @conversations_id := LAST_INSERT_ID();" +
        "INSERT INTO users_conversations(users_id, conversations_id) VALUES (@participant, @conversations_id);" +
        "INSERT INTO messages (content, sender_id, conversations_id) " +
        "VALUES(@Content, @SenderId, @conversations_id); "+
        "COMMIT; SELECT LAST_INSERT_ID();";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            messageId = con.ExecuteScalar<int>(query, param: obj);
        }
        return messageId;
    }
    public int? Delete(Message obj)
    {
        int rowsEffected = 0;
        string query = "DELETE FROM messages WHERE id = @Id;";
        // du ska kunna deleta dina medd. samt dig själv från konversationen
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<Message> Get()
    {
        List<Message> messages = new();
         string query = "SELECT m.content, concat(u1.first_name, ' ', u1.last_name) as 'Sender' " +
        "FROM messages m " + 
        "INNER JOIN users u1 ON m.sender_id = u1.id " +
        "INNER JOIN conversations c ON m.conversations_id = c.id " + 
        "INNER JOIN users_conversations uc " +
        "ON c.id = uc.conversations_id " +
        "INNER JOIN users u2 ON uc.users_id = u2.id " +
        "WHERE u2.id = 2 ORDER BY m.date_created ASC;";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            messages = con.Query<Message>(query).ToList();
        }
        return messages;
    }
    public int? Update(Message obj)
    {
        int rowsEffected = 0;
        //fixa så att du bara kan ändra dina egna meddelanden
        string query = "UPDATE messages SET content = @Content WHERE id = @Id;";
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