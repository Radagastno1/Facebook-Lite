using CORE;
namespace LOGIC;
public class NotificationManager : INotificationsManager
{
    // i notificationui ett event som körs när man valt att läsa notiserna
    // denna klassen kan lyssna på det och då uppdatera notiserna till att de är lästa ?
    INotificationDB _notificationsData;
    public NotificationManager(INotificationDB notificationsData)
    {
        _notificationsData = notificationsData;
    }
    public List<Notification> GetUnreadNotifications(User user)
    {
        return _notificationsData.GetUnread(user);
    }
    public void UpdateToRead(User user)
    {
        throw new NotImplementedException();
    }
}