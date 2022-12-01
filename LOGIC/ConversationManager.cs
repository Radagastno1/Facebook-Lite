using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation>, IIdManager
{
    IData<Conversation> _conversationData;
    IData<Message> _messageData;

    public ConversationManager(IData<Conversation> conversationData, IData<Message> messageData)
    {
        _conversationData = conversationData;
        _messageData = messageData;
    }
    public int? Create(Conversation conversation)
    {
        int? conversationId = _conversationData.Create(conversation);
        if (conversationId != null)
        {             //använder jag för mycket nullable ints?
            conversation.ID = conversationId.GetValueOrDefault();
        }
        return conversationId;
    }
    public int? Update(Conversation conversation)
    {
        int? usersConversationId = _conversationData.Update(conversation);
        return usersConversationId;
    }

    public List<Conversation> GetAll(int data)
    {
        throw new NotImplementedException();
    }

    public List<Conversation> GetBySearch(string search)
    {
        throw new NotImplementedException();
    }

    public Conversation GetOne(int data1, int data2)
    {
        try
        {
            Conversation conversation = _conversationData.GetById(data1, data2);
            return conversation;
        }
        catch (NullReferenceException)
        {
            return null;
        }
    }
    public int? Remove(Conversation obj)
    {
        throw new NotImplementedException();
    }
    public int GetId(List<int> participantIds)
    {
        //en konversation har en lista med participantids

        //id 6 och id 8 i listan  letar efter en konversationsid som har båda dessa i sig
        List<Conversation> conversations = _conversationData.Get();
        int conversationId = 0;
        List<Conversation> selectedConversations = new();

        foreach (Conversation item in conversations)
        {
            foreach (int id in participantIds)
            {
                if (id == item.ParticipantId)
                {
                    // tex konv id 25 och 26
                    selectedConversations.Add(item);
                }
            }
        }

        return conversationId;
    }
}