using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation>
{
    IData<Conversation> _conversationData;

    public ConversationManager(IData<Conversation> conversationData)
    {
        _conversationData = conversationData;
    }
    public int? Create(Conversation conversation)
    {
        int? conversationId = _conversationData.Create(conversation);
        if(conversationId != null)
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

    public Conversation GetOne(int data)
    {
        throw new NotImplementedException();
    }

    public int? Remove(Conversation obj)
    {
        throw new NotImplementedException();
    }
}