using Dapper;
using MySqlConnector;
namespace DATABASE;
public class StatisticDB  //används inte i programmet än, men experimenterar med statistik
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
        Dictionary<string, float> audiencePercentage = con.Query<KeyValuePair<string, float>>(query).ToDictionary(pair => pair.Key, pair => pair.Value);
        return audiencePercentage;
    }
    public float GetAverageAge()
    {
        string query = "SELECT AVG(2022-YEAR(birth_date)) " +
                        "FROM users;";
        using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        float averageAge = con.QuerySingle<float>(query);
        return averageAge;
    }
    public Dictionary<DateTime, int> GetAmountOfNewUsersPerDate()
    {
        string query = "SELECT DATE(account_created) as 'Date', " +
                        "COUNT(users.id) as 'Users' " +
                        "FROM users " +
                        "GROUP BY DAY(account_created);";
        using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        Dictionary<DateTime, int> newAccountPerDate = con.Query<KeyValuePair<DateTime, int>>(query).ToDictionary(pair => pair.Key, pair => pair.Value);
        return newAccountPerDate;
    }

}