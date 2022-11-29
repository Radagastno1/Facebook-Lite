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

    //man väljer en persons id
    //startar en konversation direkt som är tom först OM inte det redan finns en konversation
    //därefter kan man göra ett meddelande och skriva
}