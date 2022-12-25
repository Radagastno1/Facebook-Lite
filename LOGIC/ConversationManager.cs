using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation,User>, IConnectingMultiple<User>, IIdManager<Conversation>
{
    IData<Conversation> _conversationData;
    IDataToList<Conversation, User> _conversationsDataToList;
    IData<Message> _messageData;
    IExtraData<Conversation> _extraData;
    IIdData<ConversationResult> _getIdData;
    public ConversationManager(IData<Conversation> conversationData,IDataToList<Conversation, User> conversationsDataToList, IData<Message> messageData, IExtraData<Conversation> extraData, IIdData<ConversationResult> getIdData)
    {
        _conversationData = conversationData;
        _messageData = messageData;
        _extraData = extraData;
        _getIdData = getIdData;
        _conversationsDataToList = conversationsDataToList;
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
    public List<Conversation> GetAll(int data, User user)
    {
        List<Conversation> conversations = _conversationsDataToList.GetById(data, user);
        return conversations;
    }
    public List<Conversation> GetBySearch(string name, User user)
    {
        throw new NotImplementedException();    //ska kunna söka efter konversationer via namn i sin chatt
    }
    public Conversation GetOne(int data, User user)
    {
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
        conversationHolder = _extraData.GetByIdAndText(amountOfParticipants, sql);
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
    public int MakeNew(List<User> participants, User user)
    {
        Conversation conversation = new();
        conversation.CreatorId = user.ID;
        int conversationId = Create(conversation).GetValueOrDefault();
        //den som startar konversationen är en del av participants med:
        participants.Add(user);
        foreach (User item in participants)
        {
            conversation.ParticipantId = item.ID;
            Update(conversation);
        }
        return conversationId;
    }
    public List<Conversation> GetById(List<int> ids)
    {
        ConversationResult result = new();
        List<Conversation> conversations = new();
        bool success;
        foreach (int id in ids)
        {
            result.Conversation = _getIdData.GetIds(id).Conversation;
            success = _getIdData.GetIds(id).ConversationExists;
            if (success == true)
            {
                conversations.Add(result.Conversation);
            }
        }
        return conversations;
    }
    public Conversation GetDialogueId(int userId, int id)
    {
        try
        {
            Conversation dialogue = _extraData.GetDialogueId(userId, id);
            return dialogue;
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
}