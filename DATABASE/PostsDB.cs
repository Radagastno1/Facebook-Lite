using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class PostsDB : IData<Post>, IExtraData<Post>
{
    public int? Create(Post obj)
    {
        int rowsEffected = 0;
        string query = "INSERT INTO posts (content, users_id, posts_types_id) " +
        "VALUES(@Content, @UserId, 1);";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public int? Delete(Post obj)
    {
        int rowsEffected = 0;
        string query = "DELETE FROM posts WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<Post> Get()
    {
        List<Post> posts = new();
        string query = $"SELECT p.id as 'Id', p.content as 'Content', p.date_created as 'DateCreated', u.first_name as 'FirstName', u.last_name as 'LastName', p.users_id as 'UserId' " +
         $"FROM posts p INNER JOIN users u ON p.users_id = u.id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            posts = con.Query<Post>(query).ToList();
        }
        if(posts.Count > 0)
        {
            return posts;
        }
        else
        {
            return null;
        }
    }
    public int? Update(Post obj)
    {
        int rowsEffected = 0;
        string query = "UPDATE posts SET content = @Content WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected; 
    }
    public Post GetById(int data1, int data2)
    {
        throw new NotImplementedException();
    }

    public List<Post> GetManyByData(int data, string text)
    {
        throw new NotImplementedException();
    }
}