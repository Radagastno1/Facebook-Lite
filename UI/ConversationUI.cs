using LOGIC;
using CORE;
namespace UI;
public class ConversationUI
{
    IManager<Conversation, User> _conversationManager;
    IManager<Message, User> _messageManager;
    IIdManager<Conversation> _idManager;
    IConnectingMultiple<User> _connectingMultiple;
    public ConversationUI(IManager<Conversation, User> conversationManager, IManager<Message, User> messageManager, IIdManager<Conversation> idManager, IConnectingMultiple<User> connectingMultiple)
    {
        _conversationManager = conversationManager;
        _messageManager = messageManager;
        _idManager = idManager;
        _connectingMultiple = connectingMultiple;
    }
    public void ShowConversationParticipants(int id)
    {
        List<int> ids = new();
        ids.Add(id);
        try
        {
            List<Conversation> foundConversations = _idManager.GetParticipantsPerConversation(ids);
            List<string> conversationToList = new();
            conversationToList.Add("[Return]");
            foreach (Conversation c in foundConversations)
            {
                conversationToList.Add(c.ToString());
            }
            string [] conversationsToArray = conversationToList.ToArray();
            int amountOfChoices = conversationsToArray.Length;
            int menuOptions = 0;
            while (true)
            {
                menuOptions = ConsoleInput.GetMenuOptions(conversationsToArray);
                switch (menuOptions)
                {
                    case 0:
                        return;
                    case int n when (n > 0):
                    Console.WriteLine("Show messages");
                    Console.ReadKey();
                        break;
                }
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("No conversations yet..");
        }
    }
    public int ShowDialogue(User user, int id)
    {
        MessageUI messageUI = new(_messageManager, _idManager);
        Conversation conversation = _idManager.GetDialogueId(user.ID, id);
        if (conversation != null)
        {
            messageUI.ShowMessages(conversation.ID, user);
            return conversation.ID;
        }
        else
        {
            return 0;
        }
    }
    public void ShowConversations(List<Conversation> conversations)
    {
        foreach (Conversation item in conversations)
        {
            Console.WriteLine(item.ToString());
        }
    }
    public int MakeNewConversation(List<User> participants, User user)
    {
        int conversationId = _connectingMultiple.MakeNew(participants, user);
        if (conversationId > 0) return conversationId;
        else Console.WriteLine("Something went wrong."); return 0;
    }


    // public void ShowConversations(List<Conversation>conversations)
    // {
    //     foreach (Conversation item in conversations)
    //     {
    //         Console.WriteLine($"Conversation [{item.ID}]");
    //     }
    // }
    // public ConversationResult ConversationsExists(List<int> ids, User user)
    // {
    //     ConversationResult result = new();
    //     List<int> participantIds = new();
    //     participantIds.Add(user.ID);
    //     foreach (int id in ids)
    //     {
    //         participantIds.Add(id);
    //     }
    //     List<Conversation> foundConversations = new();
    //     foundConversations = _idManager.GetIds(participantIds);
    //     if(foundConversations.Count > 0)
    //     {
    //         result.conversations = foundConversations;
    //         result.ConversationExists = true;
    //     }
    //     else
    //     {
    //         result.ConversationExists = false;
    //     }
    //     return result;
    // }
}