using CORE;
namespace LOGIC;

public class MessgageManager : IManager<Message, User>
{
    IData<Message, User> _messageManager;
    public MessgageManager(IData<Message, User> messageManager)
    {
        _messageManager = messageManager;
    }
    public int? Create(Message message)
    {
        return _messageManager.Create(message);
    }

    public List<Message> GetAll(int conversationId, User user)
    {
        List<Message> selectedMessages = _messageManager.GetById(conversationId, user);
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

    public List<Message> GetBySearch(string search)   //söka i meddelandet efter datum eller ord?
    {
        throw new NotImplementedException();
    }

    public Message GetOne(int data1)    //hämta specifikt medd? vet ej
    {
        throw new NotImplementedException();
    }

    public int? Remove(Message obj)   //man ska kunna ta bort sitt medd = is_visible = true
    {
        throw new NotImplementedException();
    }

    public int? Update(Message obj)  //man kan redigera sitt meddel inom en viss tid, lägg en bool is_edited ?
    {
        throw new NotImplementedException();
    }
}