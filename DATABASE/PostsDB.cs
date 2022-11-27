using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class PostsDB : IData<Post>
{
    public int? Create(Post obj)
    {
        int rowsEffected = 0;
        string query = "INSERT INTO posts (content, users_id, posts_types_id) " +
        "VALUES(@Content, @UserId, 1);";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public int? Delete(Post obj)
    {
        int rowsEffected = 0;
        string query = "DELETE FROM posts WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<Post> Get()
    {
        List<Post> posts = new();
        string query = "SELECT id, content, date_created as 'DateCreated', users_id as 'UserId' " +
        "FROM posts WHERE posts_types_id = 1;";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            posts = con.Query<Post>(query).ToList();
        }
        return posts;
    }
    public int? Update(Post obj)
    {
        int rowsEffected = 0;
        string query = "UPDATE posts SET content = @Content WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected; 
    }
    public Post GetById()
    {
        throw new NotImplementedException();
    }
}