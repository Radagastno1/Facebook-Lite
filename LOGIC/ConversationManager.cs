using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation>, IConnecting<User>, IIdManager<Conversation>
{
    IData<Conversation> _conversationData;
    IData<Message> _messageData;
    IExtraData<Conversation> _extraData;
    IIdData<Conversation> _getIdData;
    public ConversationManager(IData<Conversation> conversationData, IData<Message> messageData, IExtraData<Conversation> extraData, IIdData<Conversation> getIdData)
    {
        _conversationData = conversationData;
        _messageData = messageData;
        _extraData = extraData;
        _getIdData = getIdData;
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
        List<Conversation>conversations = _getIdData.GetById(data);
        return conversations;
    }
    public List<Conversation> GetBySearch(string search)
    {
        throw new NotImplementedException();
    }
    public Conversation GetOne(int data1, int data2)
    {
        //     try
        //     {
        //         Conversation conversation = _getIdData.GetById(data1);
        //         return conversation;
        //     }
        //     catch (NullReferenceException)
        //     {
        //         return null;
        //     }
        return new Conversation();
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
            result.Conversations = conversationHolder;
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

    public List<Conversation> GetById(List<int> ids)
    {
        ConversationResult result = new();
        List<Conversation>conversations = new();
        bool success;
        foreach (int id in ids)
        {
            result.Conversation = _getIdData.GetIds(id).Conversation;
            success =_getIdData.GetIds(id).ConversationExists;
            if (success == true)
            {
                conversations.Add(result.Conversation);
            }
        }
        return conversations;
    }
}