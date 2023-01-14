using CORE;
namespace LOGIC;
public class NotificationManager : IManager<Notification, User>
{
    // i notificationui ett event som körs när man valt att läsa notiserna
    // denna klassen kan lyssna på det och då uppdatera notiserna till att de är lästa ?
    IData<Notification> _notificationData;
    public NotificationManager(IData<Notification> notificationData)
    {
        _notificationData = notificationData;
    }
    public int? Create(Notification obj)
    {
        throw new NotImplementedException();
    }

    public List<Notification> GetAll(int data, User obj)
    {
        throw new NotImplementedException();
    }

    public List<Notification> GetBySearch(string search, User obj)
    {
        throw new NotImplementedException();
    }

    public Notification GetOne(int data, User obj)
    {
        throw new NotImplementedException();
    }

    public int? Remove(Notification obj)
    {
        throw new NotImplementedException();
    }

    public int? Update(Notification obj)
    {
        throw new NotImplementedException();
    }
}