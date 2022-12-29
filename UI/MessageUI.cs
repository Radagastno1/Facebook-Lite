using CORE;
using LOGIC;
namespace UI;

public class MessageUI
{
    IManager<Message, User> _messageManager;
       IIdManager<Conversation> _idManager;

    public MessageUI(IManager<Message, User> messageManager, IIdManager<Conversation> idManager)
    {
        _messageManager = messageManager;
        _idManager = idManager;
    }
    public void MakeMessage(User user, int conversationId)
    {
        //man väljer en persons id
        //startar en konversation direkt som är tom först OM inte det redan finns en konversation
        //därefter kan man göra ett meddelande och skriva
        string content = ConsoleInput.GetString("Message: ");
        Message message = new(content, user.ID, conversationId);
        _messageManager.Create(message);
    }
    public void Messenger(User user)
    {
        List<int> ids = new();
        ids.Add(user.ID);
        List<Conversation> foundConversations = new();
        List<string> conversationToList = new();
        conversationToList.Add("[Return]");

        foundConversations = _idManager.GetParticipantsPerConversation(ids);
        if (foundConversations != null)
        {
            foreach (Conversation c in foundConversations)
            {
                conversationToList.Add(c.ToString());
            }
        }
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
                    int conversationsId = foundConversations[n - 1].ID;
                    ShowMessages(conversationsId, user);
                    MakeMessage(user, conversationsId);
                    Console.ReadKey();
                    break;
            }
        }
    }
    public void ShowMessages(int conversationId, User user)
    {
        List<Message> messages = _messageManager.GetAll(conversationId, user);
        if (messages == null || messages.Count() < 1)
        {
            Console.WriteLine("No messages here yet..");
        }
        else if (messages.Count() > 0)
        {
            foreach (Message item in messages)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}