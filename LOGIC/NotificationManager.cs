using CORE;
namespace LOGIC;
public class NotificationManager : INotificationsManager
{
    //utv: man ska kunna välja notis och komma till själva händelsen, tex vänförfråg-sida
    INotificationDB _notificationsData;
    public NotificationManager(INotificationDB notificationsData)
    {
        _notificationsData = notificationsData;
    }
    public List<Notification> GetUnreadNotifications(User user)
    {
        try
        {
            List<Notification> notifications =  _notificationsData.GetUnread(user);
            if(notifications.Count < 1 || notifications == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                return notifications;
            }
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
    public void UpdateToRead(User user)
    {
        _notificationsData.UpdateToRead(user);
    }
}