using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class ConversationDB : IData<Conversation>
{
    public int? Create(Conversation conversation)
    {
        int conversationId = 0;
        string query = "INSERT INTO conversations(creator_id) VALUES(@CreatorId);" +
        "SELECT LAST_INSERT_ID();";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            conversationId = con.ExecuteScalar<int>(query, param: conversation);
        }
        return conversationId;
    }
    public int? Update(Conversation conversation)
    {
         int usersConversationId = 0;
        string query = "INSERT INTO users_conversations(users_id, conversations_id) VALUES (@participantId, @Id);" +
        "SELECT LAST_INSERT_ID();";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            usersConversationId = con.ExecuteScalar<int>(query, param: conversation);
        }
        return usersConversationId;
    }
    public int? Delete(Conversation obj)
    {
        throw new NotImplementedException();
    }
    public List<Conversation> Get()
    {
        throw new NotImplementedException();
    }
    public Conversation GetById()
    {
        throw new NotImplementedException();
    }
}