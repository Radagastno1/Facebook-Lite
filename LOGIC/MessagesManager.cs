using CORE;
namespace LOGIC;

public class MessgageManager : IManager<Message>
{
    IData<Message> _messageManager;
    IExtraData<Message> _extraData;
    public MessgageManager(IData<Message> messageManager, IExtraData<Message> extraData)
    {
        _messageManager = messageManager;
        _extraData = extraData;
    }
    public int? Create(Message message)
    {
        return _messageManager.Create(message);
    }

    public List<Message> GetAll(int conversationId)
    {
        string text = "";
        //fixa bättre interfaces
        List<Message> selectedMessages = _extraData.GetManyByData(conversationId, text);
        if(selectedMessages == null && selectedMessages.Count() < 1)
        {
            return null;
        }
        else
        {
            return selectedMessages;
        }
        // try
        // {
        //     List<Message> allMessages = _messageManager.Get();
        //     foreach (Message item in allMessages)
        //     {
        //         if (item.ConversationId == data)
        //         {
        //             selectedMessages.Add(item);
        //         }
        //     }
        //     if (selectedMessages.Count() < 1)
        //     {
        //         throw new InvalidOperationException();
        //     }
        //     return selectedMessages;
        // }
        // catch (InvalidOperationException)
        // {
        //     return null;
        // }
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