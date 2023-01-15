using LOGIC;
using CORE;
namespace UI;
public class NotificationUI
{
    public event EventHandler<User> OnShowNotifications;
    IManager<Notification, User> _notificationManager;
    public NotificationUI(IManager<Notification, User> notificationManager)
    {
        _notificationManager = notificationManager;
    }
    public void ShowMyNotifications(User user)
    {
        //imanager passar inte perfekt för notificationmanager, kolla upp?
        try
        {
            List<Notification> notifications = _notificationManager.GetAll(0, user);
            notifications.ForEach(n => Console.WriteLine(n.ToString()));
            //event som triggas här? att dom är lästa
            OnShowNotifications?.Invoke(this, user);
        }
        catch(InvalidOperationException)
        {
            Console.WriteLine("No unread notifications.");
        }
    }
    public void SearchNotifications()
    {

    }
    public void DeleteNotifications()
    {

    }
}