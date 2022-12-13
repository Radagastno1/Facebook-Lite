using LOGIC;
using CORE;
namespace UI;
public class UserUI
{
    static int? deleted = 0;
    IManager<User> _userManager;
    IManager<Post> _postManager;
    IManager<Conversation> _conversationManager;
    IManager<Message> _messageManager;
    IIdManager<Conversation> _idManager;
    IConnectingMultiple<User> _connectionManager;
    IManager<Comment> _commentManager;
    IDeletionManager<User> _deletionManager;
    IMultipleDataGetter<User, int> _multipleUserData;
    List<ConsoleKey> keys = new();
    public UserUI(IManager<User> userManager, IManager<Post> postManager, IManager<Conversation> conversationManager, IIdManager<Conversation> idManager, IManager<Message> messageManager, IConnectingMultiple<User> connectingManager, IManager<Comment> commentManager, IDeletionManager<User> deletionManager, IMultipleDataGetter<User, int> multipleUserData)
    {
        _userManager = userManager;
        _postManager = postManager;
        _conversationManager = conversationManager;
        _idManager = idManager;
        _messageManager = messageManager;
        _connectionManager = connectingManager;
        _commentManager = commentManager;
        _deletionManager = deletionManager;
        _multipleUserData = multipleUserData;
        deleted = _deletionManager.SetAsDeleted();
        ShowAccountDeleted();
    }
    public void ShowAccountDeleted()
    {
        Console.WriteLine(deleted + " accounts deleted.");  //för admin sen, kan vara kul 
    }
    public void ShowMyFacebook(User user)
    {
        PostUI postUI = new(_postManager, _commentManager);
        string[] overviewOptions = new string[]
        { "[PUBLISH]","[SEARCH]","[CHAT]", "[MY PAGE]","[SETTINGS]", "[LOG OUT]" };
        int menuOptions = 0;
        while (true)
        {
            menuOptions = ConsoleInput.GetMenuOptions(overviewOptions);
            switch (menuOptions)
            {
                case 0:
                    postUI.PublishPost(user);
                    Console.ReadKey();
                    break;
                case 1:
                    int id = Searcher(user);
                    if (id != 0) InteractWithUser(user, id);
                    Console.ReadKey();
                    break;
                case 2:
                    //CHATPAGE
                    Messenger(user);
                    Console.ReadKey();
                    break;
                case 3:
                    MyPage(user);
                    Console.ReadKey();
                    break;
                case 4:
                    //SETTINGSMENU
                    MySettings(user);
                    break;
                case 5:
                    return;
            }
        }
    }
    public int Searcher(User user)
    {
        int id = 0;
        string search = ConsoleInput.GetString("Search by name: ");
        if (ShowSearches(search))
        {
            id = ConsoleInput.GetInt("User: ");
        }
        return id;
    }
    public void InteractWithUser(User user, int id)
    {
        MessageUI messageUI = new(_messageManager);
        ConversationUI conversationUI = new(_conversationManager, _messageManager, _idManager);
        ShowProfile(id);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[M] Message  [P] Posts", LogicTool.NewKeyList(ConsoleKey.M, ConsoleKey.P));
        if (pressedKey == ConsoleKey.M)
        {
            int conversationId = conversationUI.ShowDialogue(user, id).GetValueOrDefault();
            if (conversationId < 1)
            {
                pressedKey = ConsoleInput.GetPressedKey("[S]Start conversation  [R] Return", LogicTool.NewKeyList(ConsoleKey.S, ConsoleKey.R));
                if (pressedKey == ConsoleKey.S)
                {
                    List<int> ids = new();
                    ids.Add(id);
                    List<User> participants = _multipleUserData.GetUsersById(ids);
                    conversationId = _connectionManager.MakeNew(participants, user).GetValueOrDefault();
                }
                else
                {
                    return;
                }
            }
            messageUI.MakeMessage(user, conversationId);
        }
        else
        {
            PostUI postUI = new(_postManager, _commentManager);
            postUI.ShowPosts(id);
            pressedKey = ConsoleInput.GetPressedKey("[C] Comments  [R] Return", LogicTool.NewKeyList(ConsoleKey.D, ConsoleKey.C, ConsoleKey.R));
            if (pressedKey == ConsoleKey.C) ChooseIfComment(id);
            else return;
        }
    }
    public void Messenger(User user)
    {
        MessageUI messageUI = new(_messageManager);
        List<int> ids = new();
        ids.Add(user.ID);
        ShowConversationParticipants(ids);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey($"[C] Choose conversation  [N] New Conversation", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.N));
        if (pressedKey == ConsoleKey.C)
        {
            int conversationId = ConsoleInput.GetInt("Choose: ");
            ShowMessages(conversationId);
            messageUI.MakeMessage(user, conversationId);
        }
        else
        {
            int newConversationId = AddPeopleToNewConversation(user);
            ShowMessages(newConversationId);
            messageUI.MakeMessage(user, newConversationId);
        }
    }
    public int AddPeopleToNewConversation(User user)
    {
        //fixa så att man inte kan göra en ny konversation mellan två om det redan finns en specifik mellan tvÅ
        List<int> userIds = new();
        ConsoleKey pressedKey = new();
        int conversationId = 0;
        do
        {
            pressedKey = ConsoleInput.GetPressedKey("[A] Add user  [D] Done", LogicTool.NewKeyList(ConsoleKey.A, ConsoleKey.D));
            if (pressedKey == ConsoleKey.A)
            {
                int id = Searcher(user);
                userIds.Add(id);
            }
        } while (pressedKey != ConsoleKey.D);
        List<User> participants = _multipleUserData.GetUsersById(userIds);
        conversationId = _connectionManager.MakeNew(participants, user).GetValueOrDefault();
        return conversationId;
    }
    public void MyPage(User user)
    {
        PostUI postUI = new(_postManager, _commentManager);
        ShowProfile(user.ID);
        postUI.ShowPosts(user.ID);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[D]Delete post  [C] Comments  [R] Return", LogicTool.NewKeyList(ConsoleKey.D, ConsoleKey.C, ConsoleKey.R));
        if (pressedKey == ConsoleKey.D) postUI.DeletePost(user);
        else if (pressedKey == ConsoleKey.C) ChooseIfComment(user.ID);
        else return;
    }
    public void ChooseIfComment(int userId)
    {
        PostUI postUI = new(_postManager, _commentManager);
        int postId = ConsoleInput.GetInt("Choose post: ");
        ConsoleKey key = ConsoleInput.GetPressedKey("\t[C] Comment   [V] View Comments", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.V));
        if (key == ConsoleKey.C)
        {
            postUI.CommentPost(userId, postId);
        }
        else if (key == ConsoleKey.V)
        {
            postUI.ShowCommentsOnPost(postId);
        }
    }

    public void MySettings(User user)
    {
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[E] Edit profile [D] Delete account", LogicTool.NewKeyList(ConsoleKey.E, ConsoleKey.D));
        if (pressedKey == ConsoleKey.E)
        {
            EditInformation(user);
            user = _userManager.GetOne(user.ID);
        }
        else
        {
            bool isDeleted = DeletingAccount(user);
            if (isDeleted == true) Environment.Exit(0);
        }
    }
    public bool ShowSearches(string name)
    {
        bool isResult = false;
        List<User> users = _userManager.GetBySearch(name);
        if (users.Count > 0)
        {
            foreach (User item in users)
            {
                Console.WriteLine(item.ToString());
            }
            isResult = true;
        }
        else
        {
            Console.WriteLine("No person found by search: " + name);
        }
        return isResult;
    }
    public void ShowProfile(int id)
    {
        User user = _userManager.GetOne(id);
        Console.Title = $"{user.FirstName} {user.LastName}";
        string[] userData = new string[]
           {
                $"\n\t{Console.Title}                 ",
                $"                                ",
                $"\tI N F O R M A T I O N            ",
                $"\tGender: {user.Gender}            ",
                $"\tAbout me: {user.AboutMe}          ",
                $"                                    "
           };
        if (user != null)
        {
            foreach (string row in userData)
            {
                Console.WriteLine(row);
            }
        }
    }
    public void ShowConversationParticipants(List<int> ids)
    {
        try
        {
            List<Conversation> conversations = _idManager.GetIds(ids).Conversations;
            List<int> conversationsIds = new();
            foreach (Conversation c in conversations)
            {
                conversationsIds.Add(c.ID);
            }
            List<Conversation> foundConversations = _idManager.GetById(conversationsIds);
            foreach (Conversation c in foundConversations)
            {
                Console.WriteLine(c.ToString());
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("No conversations yet..");
        }
    }
    public void EditInformation(User user)
    {
        List<ConsoleKey> keys = new();
        keys.Add(ConsoleKey.D1); keys.Add(ConsoleKey.D2); keys.Add(ConsoleKey.D3);
        keys.Add(ConsoleKey.D4); keys.Add(ConsoleKey.D5);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[1] First name  [2] Last name  [3] Email  [4] Password  [5] About me", keys);
        switch (pressedKey)
        {
            case ConsoleKey.D1:
                user.FirstName = ConsoleInput.GetString("New first name: ");
                break;
            case ConsoleKey.D2:
                user.LastName = ConsoleInput.GetString("New last name: ");
                break;
            case ConsoleKey.D3:
                user.Email = ConsoleInput.GetEmail("New Email: ");
                break;
            case ConsoleKey.D4:
                user.PassWord = ConsoleInput.GetPassword("New password: ");
                break;
            case ConsoleKey.D5:
                user.AboutMe = ConsoleInput.GetString("About me: ");
                break;
        }
        if (_userManager.Update(user) > 0)
        {
            Console.WriteLine("Updated!");
        }
        else
        {
            Console.WriteLine("Something went wrong.");
        }
    }
    public bool DeletingAccount(User user)
    {
        string password = ConsoleInput.GetString("Type your password to delete  your account.");
        if (user.PassWord == password)
        {
            _userManager.Remove(user);
            Console.WriteLine("Your account is now inactive. If you log in to your account you will be active again.");
            Console.WriteLine("Press any key to accept.");
            Console.ReadKey();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShowMessages(int conversationId)
    {
        List<Message> messages = _messageManager.GetAll(conversationId);
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

