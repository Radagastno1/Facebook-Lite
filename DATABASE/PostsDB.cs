using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class PostsDB : IData<Post>
{
    public int? Create(Post obj)  //IDATA
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
    //FRÅGA KRISTER, SKA INLÄGG OCH KOMMENTARER RADERAS HELT ELLER FLAGGAS?
    // "START TRANSACTION; " +
    //     "DELETE FROM posts WHERE id = @id AND users_id = @usersId;" +
    //     "DELETE FROM posts WHERE on_post_id = @id;" + 
    //     "COMMIT;";
    public int? Delete(Post obj)  //IDATA //MÅSTE HÄMTA ALLA POST ID SAMT COMMENT ID TILL USERN NÄR DEN LOGGAR IN 
    {
        int rowsEffected = 0;
        string query = "Update posts SET is_visible = FALSE WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<Post> GetAll() //IDATA
    {
        List<Post> posts = new();
        string query = $"SELECT p.id as 'Id', p.content as 'Content', p.date_created as 'DateCreated', u.first_name as 'FirstName', u.last_name as 'LastName', p.users_id as 'UserId' " +
         $"FROM posts p INNER JOIN users u ON p.users_id = u.id WHERE p.posts_types_id = 1 AND is_visible = TRUE;";
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
    public int? Update(Post obj)  //IDATA
    {
        int rowsEffected = 0;
        string query = "UPDATE posts SET content = @Content WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected; 
    }
    public List<Post> GetById(int userId) //HÄMTAR EN VISS USERS INLÄGG ANVÄND VID USERPAGE
    {
         List<Post> posts = new();
        string query = $"SELECT p.id as 'Id', p.content as 'Content', p.date_created as 'DateCreated', u.first_name as 'FirstName', u.last_name as 'LastName', p.users_id as 'UserId' " +
         $"FROM posts p INNER JOIN users u ON p.users_id = u.id WHERE p.posts_types_id = 1 AND is_visible = TRUE AND p.users_id = @userId;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            posts = con.Query<Post>(query, new{@userId = userId}).ToList();
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
}