using CORE;
using LOGIC;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class CommentsDB : IData<Comment, User>
{
    public int? Create(Comment obj)  //IDATA
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
    public int? Delete(Comment obj)  //IDATA   //fixa att man bara kan radera sin egen kommentar ELLER om dom är på SIN egen post
    {
        int messageId = 0;
        string query = "UPDATE posts SET is_visible = FALSE WHERE id = @Id AND on_post_id = @OnPostId;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            messageId = con.ExecuteScalar<int>(query, param: obj);
        }
        return messageId;
    }
    public List<Comment> GetAll()
    {
        List<Comment> comments = new();
        string query = "SELECT p.id, p.content, p.date_created as 'DateCreated', p.users_id as 'UserId', " +
        "p.on_post_id as 'OnPostId', u.first_name as 'FirstName', u.last_name as 'LastName' FROM posts p " +
        "INNER JOIN users u ON p.users_id = u.id " +
        "WHERE posts_types_id = 2 AND is_visible = TRUE;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            comments = con.Query<Comment>(query).ToList();
        }
        return comments;
    }
    public List<Comment> GetById(int postId, User user)
    {
        List<Comment> commentsOnPost = new();
        string query = "SELECT p.id, p.content, p.date_created AS 'DateCreated', p.users_id AS 'UserId', " +
        "p.on_post_id AS 'OnPostId', u.first_name AS 'FirstName', u.last_name AS 'LastName' FROM posts p "+
        "INNER JOIN users u ON p.users_id = u.id " +
        "WHERE p.posts_types_id = 2 AND p.is_visible = TRUE AND p.on_post_id = @postId;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            commentsOnPost = con.Query<Comment>(query, new{@postId = postId}).ToList();
        }
        return commentsOnPost;
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
