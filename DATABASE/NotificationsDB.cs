using CORE;
using Dapper;
using MySqlConnector;
using LOGIC;
namespace DATABASE;
public class NotificationsDB : INotificationDB
{
    public List<Notification> GetUnread(User user)
    {
        string query = "SELECT CONCAT_WS(' ',u.first_name, u.last_name) AS 'FromUser', n.description " +
                     "FROM users u " +
                     "INNER JOIN users_to_notification utn " +
                     "ON u.id = utn.from_user_id " +
                     "INNER JOIN notifications n " +
                     "ON utn.notifications_id = n.id " +
                     "WHERE utn.to_user_id = @Id " +
                     "AND utn.is_read = false;";
        using MySqlConnection con = new MySqlConnection($"Server=localhost;Database=facebook_lite;Uid=root;Pwd=;");
        List<Notification> notifications = con.Query<Notification>(query, param: user).ToList();
        return notifications;
    }

    // string query = "UPDATE users_to_notification SET is_read = true WHERE to_user_id = @Id;";

    // string query = "UPDATE users_to_notification SET is_deleted = true WHERE id = @Id;";
}