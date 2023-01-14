using LOGIC;
using CORE;
namespace UI;
public class NotificationUI
{
    IManager<Notification, User> _notificationManager;
    public NotificationUI(IManager<Notification, User> notificationManager)
    {
        _notificationManager = notificationManager;
    }
    public void ShowMyNotifications()
    {
        //event som triggas här?
    }
    public void SearchNotifications()
    {

    }
    public void DeleteNotifications()
    {

    }
}