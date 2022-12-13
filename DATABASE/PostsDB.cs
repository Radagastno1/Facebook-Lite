using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class PostsDB : IData<Post>, IIdData<Post>
{
    public int? Create(Post obj)  //IDATA
    {
        int id = 0;
        string query = "INSERT INTO posts (content, users_id, posts_types_id) " +
        "VALUES(@Content, @UserId, 1); SELECT LAST_INSERT_ID()";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            id = con.ExecuteScalar<int>(query, param: obj);
        }
        return id;
    }
    //FRÅGA KRISTER, SKA INLÄGG OCH KOMMENTARER RADERAS HELT ELLER FLAGGAS?
    // "START TRANSACTION; " +
    //     "DELETE FROM posts WHERE id = @id AND users_id = @usersId;" +
    //     "DELETE FROM posts WHERE on_post_id = @id;" + 
    //     "COMMIT;";
    public int? Delete(Post post)  //om man väljer att deleta en post, så kommer den bli visible när du loggar in
    // därför behövs en tilll bool is_deleted som sätts när man deletar istället! 
    {              
        int rowsEffected = 0;
        string query = "Update posts SET is_deleted = TRUE WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, new{@Id = post.ID});
        }
        return rowsEffected;
    }
    public List<Post> GetAll() //IDATA
    {
        List<Post> posts = new();
        string query = $"SELECT p.id as 'Id', p.content as 'Content', p.date_created as 'DateCreated', u.first_name as 'FirstName', u.last_name as 'LastName', p.users_id as 'UserId' " +
         $"FROM posts p INNER JOIN users u ON p.users_id = u.id WHERE p.posts_types_id = 1 AND p.is_visible = TRUE AND p.is_deleted = FALSE;";
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
        string query = "UPDATE posts SET content = @Content, is_edited = True WHERE id = @Id;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, new{@Content = obj.Content, @Id = obj.ID});
        }
        return rowsEffected; 
    }
    public List<Post> GetById(int userId) //HÄMTAR EN VISS USERS INLÄGG ANVÄND VID USERPAGE
    {
         List<Post> posts = new();
        string query = $"SELECT p.id as 'Id', p.content as 'Content', p.date_created as 'DateCreated', u.first_name as 'FirstName', u.last_name as 'LastName', p.users_id as 'UserId' " +
         $"FROM posts p INNER JOIN users u ON p.users_id = u.id WHERE p.posts_types_id = 1 AND p.is_visible = TRUE AND p.is_deleted = FALSE AND p.users_id = @userId;";
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
    public Post GetIds(int postId)  //byt namn på metoden i interfacet iiddata
    {
        Post post = new();
        string query = $"SELECT p.id as 'Id', p.content as 'Content', p.date_created as 'DateCreated', u.first_name as 'FirstName', u.last_name as 'LastName', p.users_id as 'UserId' " +
         $"FROM posts p INNER JOIN users u ON p.users_id = u.id WHERE p.posts_types_id = 1 AND p.is_visible = TRUE AND p.is_deleted = FALSE AND p.id = @postId;";
        using (MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;"))
        {
            post = con.QuerySingle<Post>(query, new{@postId = postId});
        }
        if(post != null)
        {
            return post;
        }
        else
        {
            return null;
        }
    }
}