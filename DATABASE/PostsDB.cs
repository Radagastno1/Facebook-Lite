using LOGIC;
using CORE;
using Dapper;
using MySqlConnector;
namespace DATABASE;
public class PostsDB : IData<Post>
{
    public int Create(Post obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public int Delete(Post obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected;
    }
    public List<Post> Get()
    {
        List<Post> posts = new();
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            posts = con.Query<Post>(query).ToList();
        }
        return posts;
    }
    public int Update(Post obj)
    {
        int rowsEffected = 0;
        string query = "";
        using (MySqlConnection con = new MySqlConnection("connectionstring"))
        {
            rowsEffected = con.ExecuteScalar<int>(query, param: obj);
        }
        return rowsEffected; throw new NotImplementedException();
    }
    public Post GetById()
    {
        throw new NotImplementedException();
    }
}