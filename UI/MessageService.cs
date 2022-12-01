using CORE;
using LOGIC;
namespace UI;

public class MessageService
{
    IManager<Message> _messageManager;

    public MessageService(IManager<Message> messageManager)
    {
        _messageManager = messageManager;
    }
    public void MakeMessage(User user, int conversationId)
    {
        //man väljer en persons id
        //startar en konversation direkt som är tom först OM inte det redan finns en konversation
        //därefter kan man göra ett meddelande och skriva
        string content = ConsoleInput.GetString("Message: ");
        Message message = new(content, user.ID, conversationId);
        _messageManager.Create(message);
    }
    public void ShowMessages(int conversationId)
    {
        List<Message> messages = _messageManager.GetAll(conversationId);
        if (messages == null || messages.Count() < 1)
        {
            Console.WriteLine("No messages here yet..");
        }
        else if (messages.Count() > 0)
        {
            foreach (Message item in messages)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

}