using CORE;
namespace LOGIC;
public interface INotificationDB
{
    public List<Notification> GetUnread(User user);
}