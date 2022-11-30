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

    public void GetOneConversation(User user,int participantId)
    {
        // hämta konversationen med mitt id och en till id
        Conversation conversation = _conversationManager.GetOne(user.ID, participantId);
        // via konversationens id, hämta alla messages som har den conversation_id
    

    }
}