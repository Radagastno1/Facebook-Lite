using LOGIC;
using CORE;
namespace UI;
public class ConversationUI
{
    IManager<Conversation, User> _conversationManager;
    IManager<Message, User> _messageManager;
    IIdManager<Conversation, User> _idManager;
    IConnectingMultiple<User> _connectingMultiple;
    public ConversationUI(IManager<Conversation, User> conversationManager, IManager<Message, User> messageManager, IIdManager<Conversation, User> idManager, IConnectingMultiple<User> connectingMultiple)
    {
        _conversationManager = conversationManager;
        _messageManager = messageManager;
        _idManager = idManager;
        _connectingMultiple = connectingMultiple;
    }
    public void ShowMyConversations(User user)
    {
        try
        {
            List<int> ids = _idManager.GetAllMyConversationsIds(user);
            List<Conversation> foundConversations = _idManager.GetParticipantsPerConversation(ids);
            List<string> conversationToList = new();
            conversationToList.Add("[Return]");
            foundConversations.ForEach(c => conversationToList.Add(c.ToString()));
            string[] conversationsToArray = conversationToList.ToArray();
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
    public List<Conversation> GetAllMyConversations(User user)
    {
        try
        {
            List<int> conversationIds = _idManager.GetAllMyConversationsIds(user);
            List<Conversation> myConversations = _idManager.GetById(conversationIds);
            return myConversations;
        }
        catch(InvalidOperationException)
        {
            return null;
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
    public int MakeNewConversation(List<User> participants, User user)
    {
        int conversationId = _connectingMultiple.MakeNew(participants, user);
        if (conversationId > 0) return conversationId;
        else Console.WriteLine("Something went wrong."); return 0;
    }
    public void SearchConversation()
    {
        // användare söker om vissa personer är med i en konv 
    }
}