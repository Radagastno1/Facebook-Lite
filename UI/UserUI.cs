using LOGIC;
using CORE;
namespace UI;
public class UserUI
{
    //RENSA INTERFACES STRUKTURERA db och logik, vad används?
    static int? deleted = 0;
    IManager<User> _userManager;
    IManager<Post> _postManager;
    IManager<Conversation> _conversationManager;
    IManager<Message> _messageManager;
    IIdManager<Conversation> _idManager;
    IConnectingMultiple<User> _connectionManager;
    IManager<Comment> _commentManager;
    IDeletionManager<User> _deletionManager;
    List<ConsoleKey> keys = new();
    public UserUI(IManager<User> userManager, IManager<Post> postManager, IManager<Conversation> conversationManager, IIdManager<Conversation> idManager, IManager<Message> messageManager, IConnectingMultiple<User> connectingManager, IManager<Comment> commentManager, IDeletionManager<User> deletionManager)
    {
        _userManager = userManager;
        _postManager = postManager;
        _conversationManager = conversationManager;
        _idManager = idManager;
        _messageManager = messageManager;
        _connectionManager = connectingManager;
        _commentManager = commentManager;
        _deletionManager = deletionManager;
        deleted = _deletionManager.SetAsDeleted();
        ShowAccountDeleted();
    }
    public void ShowAccountDeleted()
    {
        Console.WriteLine(deleted + " accounts deleted.");
        Console.ReadKey();
    }
    public void ShowFacebookOverview(User user)
    {
        string[] overviewOptions = new string[]
        { "[PUBLISH]","[SEARCH]","[CHAT]", "[MY PAGE]","[SETTINGS]", "[LOG OUT]" };
        int menuOptions = 0;
        while (true)
        {
            menuOptions = ConsoleInput.GetMenuOptions(overviewOptions);
            switch (menuOptions)
            {
                case 0:
                    PublishPost(user);
                    Console.ReadKey();
                    break;
                case 1:
                    Searcher(user);
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

    public void PublishPost(User user)
    {
        PostService postService = new(_postManager, _commentManager);
        int postId = postService.MakePost(user);
        postService.ShowPostById(postId);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[E] Edit  [P] Publish", LogicTool.NewKeyList(ConsoleKey.E, ConsoleKey.P));
        if (pressedKey == ConsoleKey.E)
        {
            postService.EditPost(postId);
        }
        else return;
    }
    public void Searcher(User user)
    {
        int conversationId = 0;
        string search = ConsoleInput.GetString("Search by name: ");
        ShowSearches(search);
        int id = ConsoleInput.GetInt("User to visit: ");
        ShowProfile(id);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[M] Message  [P] Posts", LogicTool.NewKeyList(ConsoleKey.M, ConsoleKey.P));
        if (pressedKey == ConsoleKey.M)
        {
            //ENDAST ENSKILDA DIALOGEN NÄR MAN GÅR IN VIA NÅGONS PROFIL TILL MEDD.
            Conversation conversation = _idManager.GetDialogueId(user.ID, id);
            if (conversation != null)
            {
                // ShowConversations(conversations);
                // conversationId = ConsoleInput.GetInt("Choose: ");
                ShowMessages(conversation.ID);
            }
            else
            {
                pressedKey = ConsoleInput.GetPressedKey("[S]Start conversation  [R] Return", LogicTool.NewKeyList(ConsoleKey.S, ConsoleKey.R));
                if (pressedKey == ConsoleKey.S)
                {
                    List<int>ids = new();
                    ids.Add(id);
                    List<User> participants = GetUsersById(ids);
                    conversationId = _connectionManager.MakeNew(participants, user).GetValueOrDefault();
                }
                else
                {
                    return;
                }
            }
            MakeMessage(user, conversationId);
        }
    }
    public void Messenger(User user)
    {
        List<int>ids = new();
        ids.Add(user.ID);
        ShowConversationParticipants(ids);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey($"[C] Choose conversation  [N] New Conversation", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.N));
        if (pressedKey == ConsoleKey.C)
        {
            int conversationId = ConsoleInput.GetInt("Choose: ");
                ShowMessages(conversationId);
                MakeMessage(user, conversationId);
        }
        else
        {
            
            int newConversationId = AddPeopleToNewConversation(user);
            ShowMessages(newConversationId);
            MakeMessage(user, newConversationId);
        }
    }
    public void MyPage(User user)
    {
        PostService postService = new(_postManager, _commentManager);
        ShowProfile(user.ID);
        int postId = postService.ShowPosts(user.ID);
        if (postId != 0)
        {
            ConsoleKey key = ConsoleInput.GetPressedKey("\t[C] Comment   [V] View Comments", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.V));
            if (key == ConsoleKey.C)
            {
                postService.CommentPost(user, postId);
            }
            else if (key == ConsoleKey.V)
            {
                postService.ShowCommentsOnPost(postId);
            }
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
            if(isDeleted == true) Environment.Exit(0);
        }
    }
    public void ShowSearches(string name)
    {
        List<User> users = _userManager.GetBySearch(name);
        if (users != null)
        {
            foreach (User item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
    public int AddPeopleToNewConversation(User user)  // I CONVERSATIONSERVICE
    {
        List<int> userIds = new();
        ConsoleKey pressedKey = new();
        int conversationId = 0;
        do
        {
            pressedKey = ConsoleInput.GetPressedKey("[A] Add user  [R] Return", LogicTool.NewKeyList(ConsoleKey.A, ConsoleKey.R));
            if (pressedKey == ConsoleKey.A)
            {
                string userName = ConsoleInput.GetString($"Search for user by name: ");
                ShowSearches(userName);
                int id = ConsoleInput.GetInt("Choose by ID");
                userIds.Add(id);
            }

        } while (pressedKey != ConsoleKey.R);
        List<User> participants = GetUsersById(userIds);
        conversationId = _connectionManager.MakeNew(participants, user).GetValueOrDefault();
        return conversationId;
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
    public void ShowConversationParticipants(List<int>ids) 
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
    public void ShowConversations(List<Conversation> conversations)
    {
        foreach (Conversation item in conversations)
        {
            Console.WriteLine(item.ToString());
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
    public List<User> GetUsersById(List<int> ids)
    {//DENNA SKA VARA I LOGIK?
        List<User> participants = new();
        foreach (int id in ids)
        {
            User participant = _userManager.GetOne(id);
            participants.Add(participant);
        }
        return participants;
    }
    public void MakeMessage(User user, int conversationId)
    {
        string content = ConsoleInput.GetString("Message: ");
        Message message = new(content, user.ID, conversationId);
        _messageManager.Create(message);
    }
}