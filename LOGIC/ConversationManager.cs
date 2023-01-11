using CORE;
namespace LOGIC;

public class ConversationManager : IManager<Conversation, User>, IConnectingMultiple<User>, IIdManager<Conversation, User>
{
    IData<Conversation> _conversationData;
    IDataToList<Conversation, User> _conversationsDataToList;
    IData<Message> _messageData;
    IConversationData<Conversation, ConversationResult> _specificConversationData;
    public ConversationManager(IData<Conversation> conversationData, IDataToList<Conversation, User> conversationsDataToList, IData<Message> messageData, IConversationData<Conversation, ConversationResult> specificConversationData)
    {
        _conversationData = conversationData;
        _messageData = messageData;
        _specificConversationData = specificConversationData;
        _conversationsDataToList = conversationsDataToList;
    }
    public int? Create(Conversation conversation)
    {
        int? conversationId = _conversationData.Create(conversation, QueryGenerator<Conversation>.InsertQuery(conversation));
        if (conversationId != null)
        {         
            conversation.ID = conversationId.GetValueOrDefault();
        }
        return conversationId;
    }
    public int? Update(Conversation conversation)
    {
        int? usersConversationId = _conversationData.Update(conversation, QueryGenerator<Conversation>.UpdateQuery(conversation));
        return usersConversationId;
    }
    public List<Conversation> GetAll(int data, User user)
    {
        List<Conversation> conversations = _conversationsDataToList.GetById(data, user);
        return conversations;
    }
    public List<Conversation> GetBySearch(string name, User user)
    {
        throw new NotImplementedException();//ska kunna söka efter konversationer via namn i sin chatt
    }
    public Conversation GetOne(int data, User user)
    {
        return new Conversation();
    }
    public int? Remove(Conversation obj)
    {
        throw new NotImplementedException();//man ska kunna lämna en konversation
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
        conversationHolder = _specificConversationData.GetConversationsOfSpecificParticipants(amountOfParticipants, sql);
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
    public List<int> GetAllMyConversationsIds(User user)
    {
        List<Conversation> conversations = _conversationData.GetAll(user, QueryGenerator<Conversation>.SelectQuery(new Conversation()));
        List<int> conversationIds = new();
        conversations.ForEach(c => conversationIds.Add(c.ID));
        return conversationIds;
    }       
    public int MakeNew(List<User> participants, User user)
    {
        Conversation conversation = new();
        conversation.CreatorId = user.ID;
        int conversationId = Create(conversation).GetValueOrDefault();
        participants.Add(user);
        foreach (User item in participants)
        {
            conversation.ParticipantId = item.ID;
            Update(conversation);
        }
        return conversationId;
    }
    public List<Conversation> GetParticipantsPerConversation(List<int> ids)
    {
        //användares id ska komma in och det ska kollas mot db om det finns en konv mellan dessa
        // try
        // {
        //     List<Conversation> conversations = GetIds(ids).Conversations;
        //     List<int> conversationsIds = new();
        //     //konversations id läggs i en lista
        //     conversations.ForEach(c => conversationsIds.Add(c.ID));
        //     //konversationerna som fanns med alla deltarares namn hämtas 
        //     List<Conversation> foundConversations = GetById(conversationsIds);
        //     return foundConversations;
        // }
        // catch (InvalidOperationException)
        // {
        //     return null;
        // }
        // catch(NullReferenceException)
        // {
        //     return null;
        // }
        throw new NotImplementedException();
    }
    public List<Conversation> GetById(List<int> ids)
    {
        ConversationResult result = new();
        List<Conversation> conversations = new();
        bool success;
        foreach (int id in ids)
        {
            result.Conversation = _specificConversationData.GetConversationIdAndParticipantNames(id).Conversation;
            success = _specificConversationData.GetConversationIdAndParticipantNames(id).ConversationExists;
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
            Conversation dialogue = _specificConversationData.GetDialogueId(userId, id);
            return dialogue;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}