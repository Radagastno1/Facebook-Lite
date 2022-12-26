using LOGIC;
using CORE;
namespace UI;
public class UserUI
{
    static int? deleted = 0;
    IManager<User, User> _userManager;
    IManager<Post, User> _postManager;
    IManager<Conversation, User> _conversationManager;
    IManager<Message, User> _messageManager;
    IIdManager<Conversation> _idManager;
    IManager<Comment, User> _commentManager;
    IDeletionManager<User> _deletionManager;
    IMultipleDataGetter<User, int> _multipleUserData;
    IFriendManager _friendManager;
    public Func<User, int, int> OnDialogue;
    public Func<List<User>, User, int> OnMakeConversation;
    public Action<User, int> OnMakeMessage;
    public Action<int, User> OnStart;
    List<ConsoleKey> keys = new();
    public Action<User> LoadFriends;

    public UserUI(IManager<User, User> userManager, IManager<Post, User> postManager, IManager<Conversation, User> conversationManager, IIdManager<Conversation> idManager, IManager<Message, User> messageManager, IManager<Comment, User> commentManager, IDeletionManager<User> deletionManager, IMultipleDataGetter<User, int> multipleUserData, IFriendManager friendManager)
    {
        _userManager = userManager;
        _postManager = postManager;
        _conversationManager = conversationManager;
        _idManager = idManager;
        _messageManager = messageManager;
        _commentManager = commentManager;
        _deletionManager = deletionManager;
        _multipleUserData = multipleUserData;
        deleted = _deletionManager.SetAsDeleted();  //deletar users som varit inaktiva i mer än 30 dagar när den startar
        _friendManager = friendManager;
    }
    public int Searcher(User user)
    {
        int id = 0;
        string search = ConsoleInput.GetString("Search by name: ");
        if (ShowSearches(search, user) == true)
        {
            id = ConsoleInput.GetInt("User: ");
        }
        return id;
    }
    public void InteractWithUser(User user, int id)  //döpa till meny? sätta som statisk i program?
    {
        while (true)
        {
            if (!ShowProfile(id, user)) return;
            ConsoleKey key = ConsoleInput.GetPressedKey("[C] Check out user   [R] Return", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.R));
            if (key == ConsoleKey.R)
            {
                return;
            }
            string[] overviewOptions = new string[]
              {"[POSTS]","[MESSAGE]","[RETURN]"};
            int menuOptions = 0;

            FriendsUI friendsUI = new(_friendManager, user);
            int status = friendsUI.GetFriendShipStatus(user, id);
            if (status == 1) overviewOptions = overviewOptions.Concat(new string[] { "[ADD FRIEND]" }).ToArray();
            else if (status == 3) overviewOptions = overviewOptions.Concat(new string[] { "[CONFIRM FRIEND REQUEST]" }).ToArray();

            menuOptions = ConsoleInput.GetMenuOptions(overviewOptions);
            switch (menuOptions)
            {
                case 0:
                    PostUI postUI = new(_postManager, _commentManager);
                    postUI.ShowPosts(id, user);
                    key = ConsoleInput.GetPressedKey("[C] Comments  [R] Return", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.R));
                    if (key == ConsoleKey.C)
                    {
                        ChooseIfComment(user);
                    }
                    else if (key == ConsoleKey.R)
                    {
                        continue;
                    }
                    Console.ReadKey();
                    break;
                case 1:
                    int conversationId = (int)OnDialogue?.Invoke(user, id);
                    if (conversationId < 1)
                    {
                        key = ConsoleInput.GetPressedKey("[S]Start conversation  [R] Return", LogicTool.NewKeyList(ConsoleKey.S, ConsoleKey.R));
                        if (key == ConsoleKey.S)
                        {
                            List<int> ids = new();
                            ids.Add(id);
                            List<User> participants = _multipleUserData.GetUsersById(ids, user);
                            conversationId = (int)OnMakeConversation?.Invoke(participants, user);
                        }
                        else
                        {
                            return;
                        }
                    }
                    OnMakeMessage?.Invoke(user, conversationId);
                    Console.ReadKey();
                    break;
                case 2:
                    continue;
                case 3:
                    if (status == 1 || status == 3)
                    {
                        friendsUI.FriendRequest(user, id);
                        LoadFriends?.Invoke(user);
                    }
                    Console.ReadKey();
                    break;
            }
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
            ShowMessages(conversationId, user);
            messageUI.MakeMessage(user, conversationId);
        }
        else
        {
            int newConversationId = AddPeopleToNewConversation(user);
            ShowMessages(newConversationId, user);
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
        List<User> participants = _multipleUserData.GetUsersById(userIds, user);
        // DENNA DELEGAT ANVÄNDS ANDRA GÅNGEN HÄR
        conversationId = (int)OnMakeConversation?.Invoke(participants, user);
        // conversationId = _connectionManager.MakeNew(participants, user);
        return conversationId;
    }
    public void MyPage(User user)
    {
        PostUI postUI = new(_postManager, _commentManager);
        ShowProfile(user.ID, user);
        Console.WriteLine("[Press any key]");
        Console.ReadLine();
        string[] overviewOptions = new string[]
        { "[MY POSTS]","[MY FRIENDS]","[RETURN]"};
        int menuOptions = 0;
        menuOptions = ConsoleInput.GetMenuOptions(overviewOptions);
        switch (menuOptions)
        {
            case 0:
                postUI.ShowPosts(user.ID, user);
                ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[D]Delete post  [C] Comments  [R] Return", LogicTool.NewKeyList(ConsoleKey.D, ConsoleKey.C, ConsoleKey.R));
                if (pressedKey == ConsoleKey.D) postUI.DeletePost(user);
                else if (pressedKey == ConsoleKey.C) ChooseIfComment(user);
                else return;
                break;
            case 1:
                FriendsUI.ShowMyFriends(user);
                break;
            case 2:
                return;
        }
    }
    public void ChooseIfComment(User user)
    {
        PostUI postUI = new(_postManager, _commentManager);
        int postId = ConsoleInput.GetInt("Choose post: ");
        ConsoleKey key = ConsoleInput.GetPressedKey("\t[C] Comment   [V] View Comments", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.V));
        if (key == ConsoleKey.C)
        {
            postUI.CommentPost(user.ID, postId);
        }
        else if (key == ConsoleKey.V)
        {
            postUI.ShowCommentsOnPost(postId, user);
        }
    }
    public void MySettings(User user)
    {
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[E] Edit profile [D] Delete account", LogicTool.NewKeyList(ConsoleKey.E, ConsoleKey.D));
        if (pressedKey == ConsoleKey.E)
        {
            EditInformation(user);
            user = _userManager.GetOne(user.ID, user);
        }
        else
        {
            bool isDeleted = DeletingAccount(user);
            if (isDeleted == true) Environment.Exit(0);
        }
    }
    public bool ShowSearches(string name, User user)
    {
        bool isResult = false;
        List<User> users = _userManager.GetBySearch(name, user);
        if (users == null || users.Count < 1)
        {
            Console.WriteLine("No person by " + name + " found.");
            isResult = false;
        }
        else
        {
            foreach (User item in users)
            {
                Console.WriteLine(item.ToString());
            }
            isResult = true;
        }
        return isResult;
    }
    public string MakeViewName(string firstName, string lastName)
    {
        string joinedNames = firstName + " " + lastName;
        string viewName = string.Empty;
        for(int i = 0; i < joinedNames.Length; i++)
        {
            viewName += joinedNames[i] + " ";
        }
        return viewName;
    }

    public bool ShowProfile(int id, User user)
    {   
        FriendsUI friendsUI = new(_friendManager, user);
        User userToShow = _userManager.GetOne(id, user);
        if (userToShow == null) return false;
        Console.Title = $"{MakeViewName(userToShow.FirstName, userToShow.LastName)}";
        string[] userData = new string[]
           {
                $"\n\t{Console.Title}                 ",
                $"                                ",
                $"\tI N F O R M A T I O N            ",
                $"\tGender: {userToShow.Gender}            ",
                $"\tAbout me: {userToShow.AboutMe}          ",
                $"                                    ",
                $"{friendsUI.ShowFriendShipStatus(id, user)} ",
           };
        if (userToShow != null)
        {
            foreach (string row in userData)
            {
                Console.WriteLine(row);
            }
        }
        return true;
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

