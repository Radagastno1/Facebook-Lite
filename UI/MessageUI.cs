using CORE;
using LOGIC;
namespace UI;

public class MessageUI
{
    IManager<Message, User> _messageManager;
    IIdManager<Conversation, User> _idManager;

    public MessageUI(IManager<Message, User> messageManager, IIdManager<Conversation, User> idManager)
    {
        _messageManager = messageManager;
        _idManager = idManager;
    }
    public void MakeMessage(User user, int conversationId)
    {
        string content = ConsoleInput.GetString("Message: ");
        Message message = new(content, user.ID, conversationId);
        _messageManager.Create(message);
    }
    public void ShowMessages(int conversationId, User user)
    {
        List<Message> messages = _messageManager.GetAll(conversationId, user);
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