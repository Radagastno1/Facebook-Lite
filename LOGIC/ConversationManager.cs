using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation>, IIdManager<Conversation>, IConnecting<User>
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
    public ConversationResult GetIds(List<int> participantIds)
    {
        ConversationResult result = new();
        List<Conversation> conversationHolder = new();
        int amountOfParticipants = participantIds.Count();
        string sql = "";
        foreach (int id in participantIds)
        {
            if (id != participantIds.Last())
            {
                sql += $"{id}, ";
            }
            else
            {
                sql += $"{id}";
            }
        }
        conversationHolder = _extraData.GetManyByData(amountOfParticipants, sql);
        if (conversationHolder.Count > 0)
        {
            result.conversations = conversationHolder;
            result.ConversationExists = true;
        }
        else
        {
            result.ConversationExists = false;
        }
        return result;
    }
    public int? MakeNew(List<User> participants, User user)
    {
        //DENNA I LOGIK!
        Conversation conversation = new();
        conversation.CreatorId = user.ID;
        int? conversationId = Create(conversation);
        //den som startar konversationen är en del av participants med:
        participants.Add(user);
        foreach (User item in participants)
        {
            conversation.ParticipantId = item.ID;
            Update(conversation);
        }
        // insert into conversations(user.id)
        //sedan insert into users_conversations user.id, conversation.id
        //foreach user in participants: 
        //sedan insert into users_conversations particpant.id, conversation.id
        return conversationId;
    }

}