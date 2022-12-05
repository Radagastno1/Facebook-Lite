using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class ConversationDB : IData<Conversation>, IExtraData<Conversation>, IIdData<Conversation>
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
        List<Conversation> allConversations = new();
        string query = "SELECT uc.conversations_id as 'Id', c.date_created as 'DateCreated', c.creator_id as 'CreatorId', u.id as 'ParticipantId'" +
                       "FROM conversations c " +
                       "LEFT JOIN users_conversations uc " +
                       "ON c.id = uc.conversations_id " +
                       "LEFT JOIN users u " +
                        "ON u.id = uc.users_id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            allConversations = con.Query<Conversation>(query).ToList();
        }
        return allConversations;
    }
    public List<Conversation> GetManyByData(int amountOfUsers, string sql)
    {
        List<Conversation> conversations = new();
        string query = $"SELECT uc.conversations_id AS 'ID', " +
         "GROUP_CONCAT(uc.users_id) AS User_List " +
        "FROM users_conversations uc " +
        $"WHERE  uc.users_id IN ({sql})" + //I SQL SKA IN IDS I EN PARANTES
        "GROUP BY uc.conversations_id " +
        "HAVING COUNT(DISTINCT uc.users_id) = @amountOfUsers;"; //2 is how many usersids HÄR SKA IN LÄNGD PÅ LISTAN
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            conversations = con.Query<Conversation>(query, new { @amountOfUsers = amountOfUsers }).ToList();
        }
        return conversations;
    }

    Conversation IExtraData<Conversation>.GetOneByData(int data, string text)
    {
        throw new NotImplementedException();
    }

    public ConversationResult GetIds(int conversationId)
    {
        List<User>users = new();
        ConversationResult result = new();
            string query = $" SELECT c.id as 'ID', GROUP_CONCAT(u.first_name) AS ParticipantsNames " +
                           "FROM conversations c " +
                           "INNER JOIN users_conversations uc " +
                           "ON uc.conversations_id = c.id " +
                           "INNER JOIN users u " +
                           "ON u.id = uc.users_id " +
                           "WHERE c.id = @id;";
            using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
            {
                result.Conversation = con.QuerySingle<Conversation>(query, new { @id = conversationId });
            }
            if(result.Conversation != null)
            {
                result.ConversationExists = true;
            }
        return result;
    }
    public List<Conversation> GetById(int data1)
    {
        List<Conversation>conversations = new();
        string query = $"SELECT uc.conversation_id as 'ID' FROM users_conversations uc WHERE uc.users_id = $data;"; 
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            conversations = con.Query<Conversation>(query).ToList();
        }
        return conversations;
    }
}