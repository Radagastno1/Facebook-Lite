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
        List<Conversation> conversations = _conversationData.Get();
        int conversationId = 0;
        foreach (int id in participantIds)
        {
            foreach (Conversation item in conversations)
            {
               if(id == item.ParticipantId)
               {
                conversationId = item.ID;
               }
            }
        }
        return conversationId;
    }
}