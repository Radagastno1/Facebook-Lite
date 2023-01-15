using CORE;
namespace LOGIC;
public interface INotificationsManager
{
    public List<Notification> GetUnreadNotifications(User user);
    public void UpdateToRead(User user);
}