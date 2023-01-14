using LOGIC;
using CORE;
namespace UI;
public class ConversationUI
{
    IManager<Conversation, User> _manager;
    IManager<Message, User> _messageManager;
    IConversationManager _conversationManager;
    public ConversationUI(IManager<Conversation, User> manager, IManager<Message, User> messageManager, IConversationManager conversationManager)
    {
        _manager = manager;
        _messageManager = messageManager;
        _conversationManager = conversationManager;
       
    }
    public void ShowMyConversations(User user)
    {
        try
        {
            List<int> ids = _conversationManager.GetAllMyConversationsIds(user);
            List<Conversation> foundConversations = _conversationManager.GetParticipantsPerConversation(ids);
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
            List<int> conversationIds = _conversationManager.GetAllMyConversationsIds(user);
            List<Conversation> myConversations = _conversationManager.GetById(conversationIds);
            return myConversations;
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
    public int ShowDialogue(User user, int id)
    {
        MessageUI messageUI = new(_messageManager);
        Conversation conversation = _conversationManager.GetDialogueId(user.ID, id);
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
        int conversationId = _conversationManager.MakeNew(participants, user);
        if (conversationId > 0) return conversationId;
        else Console.WriteLine("Something went wrong."); return 0;
    }
    public void SearchConversation()
    {
        // användare söker om vissa personer är med i en konv 
    }
}