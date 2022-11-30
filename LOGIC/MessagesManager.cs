using CORE;
namespace LOGIC;

public class MessgageManager : IManager<Message>
{
    IData<Message> _messageManager;
    public MessgageManager(IData<Message> messageManager)
    {
        _messageManager = messageManager;
    }
    public int? Create(Message message)
    {
        return _messageManager.Create(message);
    }

    public List<Message> GetAll(int data)
    {
        List<Message> allMessages = new();
        List<Message> selectedMessages = _messageManager.Get();
        // if....
        return selectedMessages;
    }

    public List<Message> GetBySearch(string search)
    {
        throw new NotImplementedException();
    }

    public Message GetOne(int data1, int data2)
    {
        throw new NotImplementedException();
    }

    public int? Remove(Message obj)
    {
        throw new NotImplementedException();
    }

    public int? Update(Message obj)
    {
        throw new NotImplementedException();
    }
}