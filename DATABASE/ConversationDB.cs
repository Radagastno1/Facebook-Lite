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
        //här hämtar vi alla konversationer som man har i sin messenger
        throw new NotImplementedException();
    }
    public Conversation GetById(int myId, int participantId)
    {
        Conversation conversation = new();
        string query = "SELECT uc.conversations_id as 'Id' FROM users_conversations uc " +
        "INNER JOIN users u1 ON u1.id = uc.users_id INNER JOIN users u2 ON u1.id = uc.users_id " + 
        "WHERE u1.id = @myId AND u2.id = @participantId;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            conversation = con.QuerySingle<Conversation>(query, new{@myId = myId, @participantId = participantId});
        }
        return conversation;
    }
}