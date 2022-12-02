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
    public int GetOneConversation(User user, List<int>ids)
    {
        List<int> participantIds = new();
        participantIds.Add(user.ID);
        foreach(int id in ids)
        {
            participantIds.Add(id);
        }
        //Conversation? conversation = _conversationManager.GetOne(user.ID, participantId);
        List<Conversation>foundConversations = new();
        foundConversations = _idManager.GetIds(participantIds);
        // hämta konversationen med mitt id och fler
        foreach(Conversation item in foundConversations)
        {
            Console.WriteLine($"Conversation [{item.ID}]");
        }
        int conversationId = ConsoleInput.GetInt("Choose: ");
            Console.WriteLine("Visar meddelanden i konversation som fanns");
            // via konversationens id, hämta alla messages som har den conversation_id
            MessageService messageService = new(_messageManager);
            messageService.ShowMessages(conversationId);
        
        return conversationId;
    }
}