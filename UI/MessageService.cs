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
        string content = ConsoleInput.GetString("Message: ");
        Message message = new(content, user.ID, conversationId);
        _messageManager.Create(message);
    }
    //man väljer en persons id
    //startar en konversation direkt som är tom först OM inte det redan finns en konversation
    //därefter kan man göra ett meddelande och skriva
}