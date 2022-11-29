using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class CommentsDB : IData<Comment>
{
    public int? Create(Comment obj)
    {
        int messageId = 0;
        string query = "INSERT INTO posts (content, users_id, posts_types_id, on_post_id) " +
        "VALUES (@Content, @UserId, 2, @OnPostId);";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            messageId = con.ExecuteScalar<int>(query, param: obj);
        }
        return messageId;
    }
    public int? Delete(Comment obj)
    {
        int messageId = 0;
        string query = "DELETE FROM posts WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            messageId = con.ExecuteScalar<int>(query, param: obj);
        }
        return messageId;
    }
    public List<Comment> Get()
    {
        List<Comment> comments = new();
        string query = "SELECT id, content, date_created as 'DateCreated', users_id as 'UserId', " +
        "on_post_id as 'OnPostId' FROM posts WHERE posts_types_id = 2;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            comments = con.Query<Comment>(query).ToList();
        }
        return comments;
    }

    public Comment GetById()
    {
        throw new NotImplementedException();
    }

    public int? Update(Comment obj)
    {
        int rowsEffected = 0;
        string query = "UPDATE posts SET content = @Content WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected; 
    }
}
