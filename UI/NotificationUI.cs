using LOGIC;
using CORE;
namespace UI;
public class NotificationUI
{
    public Action<User> OnShowNotifications;
    INotificationsManager _notificationManager;
    public NotificationUI(INotificationsManager notificationManager)
    {
        _notificationManager = notificationManager;
    }
    public void ShowMyNotifications(User user)
    {
        //imanager passar inte perfekt för notificationmanager, kolla upp?
        List<Notification> notifications = _notificationManager.GetUnreadNotifications(user);
        if (notifications == null)
        {
            Console.WriteLine("No unread notifications.");
        }
        else
        {
            notifications.ForEach(n => Console.WriteLine(n.ToString()));
            //event som triggas här? att dom är lästa
            // OnShowNotifications?.Invoke(user);
            _notificationManager.UpdateToRead(user);
        }
    }
    public void SearchNotifications()
    {

    }
    public void DeleteNotifications()
    {

    }
}