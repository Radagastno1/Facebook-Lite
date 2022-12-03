using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation>, IIdManager<Conversation>
{
    IData<Conversation> _conversationData;
    IData<Message> _messageData;
    IExtraData<Conversation> _extraData;
    public ConversationManager(IData<Conversation> conversationData, IData<Message> messageData, IExtraData<Conversation> extraData)
    {
        _conversationData = conversationData;
        _messageData = messageData;
        _extraData = extraData;
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
    public List<Conversation> GetIds(List<int> participantIds)
    {
        int amountOfParticipants = participantIds.Count();
        List<Conversation> conversationHolder = new();
        string sql = "";
        foreach(int id in participantIds)
        {
            sql += $" AND id = {id} ";
            conversationHolder.Add(_extraData.GetOneByData(id, sql));
        }
        return conversationHolder;
    }
}