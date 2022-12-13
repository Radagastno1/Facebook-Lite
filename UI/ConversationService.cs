using LOGIC;
using CORE;
namespace UI;
public class ConversationService
{
    IManager<Conversation> _conversationManager;
    IManager<Message> _messageManager;
    IIdManager<Conversation> _idManager;
    public ConversationService(IManager<Conversation> conversationManager, IManager<Message> messageManager, IIdManager<Conversation> idManager)
    {
        _conversationManager = conversationManager;
        _messageManager = messageManager;
        _idManager = idManager;
    }
    public void ShowConversationParticipants(int id)
    {
        List<int> ids = new();
        ids.Add(id);
        try
        {
            List<Conversation> conversations = _idManager.GetIds(ids).Conversations;
            List<int> conversationsIds = new();
            foreach (Conversation c in conversations)
            {
                conversationsIds.Add(c.ID);
            }
            List<Conversation> foundConversations = _idManager.GetById(conversationsIds);
            foreach (Conversation c in foundConversations)
            {
                Console.WriteLine(c.ToString());
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("No conversations yet..");
        }
    }
    public int? GetDialogue(User user, int id)
    {
        MessageService messageService = new(_messageManager);
        Conversation conversation = _idManager.GetDialogueId(user.ID, id);
        if (conversation != null)
        {
            messageService.ShowMessages(conversation.ID);
            return conversation.ID;
        }
        else
        {
            return null;
        }
    }

    // public void ShowConversations(List<Conversation>conversations)
    // {
    //     foreach (Conversation item in conversations)
    //     {
    //         Console.WriteLine($"Conversation [{item.ID}]");
    //     }
    // }
    // public ConversationResult ConversationsExists(List<int> ids, User user)
    // {
    //     ConversationResult result = new();
    //     List<int> participantIds = new();
    //     participantIds.Add(user.ID);
    //     foreach (int id in ids)
    //     {
    //         participantIds.Add(id);
    //     }
    //     List<Conversation> foundConversations = new();
    //     foundConversations = _idManager.GetIds(participantIds);
    //     if(foundConversations.Count > 0)
    //     {
    //         result.conversations = foundConversations;
    //         result.ConversationExists = true;
    //     }
    //     else
    //     {
    //         result.ConversationExists = false;
    //     }
    //     return result;
    // }
}