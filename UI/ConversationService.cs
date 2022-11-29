using LOGIC;
using CORE;
namespace UI;
public class ConversationService
{
    IManager<Conversation> _conversationManager;

    public ConversationService(IManager<Conversation> conversationManager)
    {
        _conversationManager = conversationManager;
    }
    public void StartConversation(User user, int toUserId)
    {

    }
}