using LOGIC;
using CORE;
namespace UI;
public class ConversationService
{
    IManager<Conversation> _conversationManager;
    IManager<Message> _messageManager;
    public ConversationService(IManager<Conversation> conversationManager, IManager<Message> messageManager)
    {
        _conversationManager = conversationManager;
        _messageManager = messageManager;
    }
    public int? StartConversation(User user, List<User> participants)
    {
        Conversation conversation = new();
        conversation.CreatorId = user.ID;
        int? conversationId = _conversationManager.Create(conversation);

        //den som startar konversationen är en del av participants med:
        participants.Add(user);

        foreach (User item in participants)
        {
            conversation.ParticipantId = item.ID;
            _conversationManager.Update(conversation);
        }
        // insert into conversations(user.id)
        //sedan insert into users_conversations user.id, conversation.id
        //foreach user in participants: 
        //sedan insert into users_conversations particpant.id, conversation.id
        return conversationId;
    }
    public int GetOneConversation(User user, int participantId)
    {
        Conversation? conversation = _conversationManager.GetOne(user.ID, participantId);
        // hämta konversationen med mitt id och en till id
        if (conversation == null)
        {
            Console.WriteLine("Kastar invalid exception");
            //gör ny konversation
            throw new InvalidOperationException();
        }
        else
        {
            Console.WriteLine("Visar meddelanden i konversation som fanns");
            // via konversationens id, hämta alla messages som har den conversation_id
            MessageService messageService = new(_messageManager);
            messageService.ShowMessages(conversation.ID);
        }
        return conversation.ID;
    }
}