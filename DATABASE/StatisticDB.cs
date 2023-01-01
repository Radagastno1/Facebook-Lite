using Dapper;
using MySqlConnector;
namespace DATABASE;
public class StatisticDB
{
    public float GetPercentageActiveUsers()
    {
        string query = "SELECT AVG(is_active) * 100 " +
                    "FROM users;";
        using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        float activeUsers = con.QuerySingle<int>(query);
        return activeUsers;
    }
    public Dictionary<string, float> GetPercentageAudience()
    {
        string query = "SELECT gender, COUNT(*) * 100/ " + 
                    "(SELECT COUNT (*) FROM users) " +      
                    "FROM users " +
                    "GROUP BY gender;";
        using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        Dictionary<string, float> audiencePercentage  = con.Query<KeyValuePair<string, float>>(query).ToDictionary(pair => pair.Key, pair => pair.Value);
        return audiencePercentage;
    }
  
}