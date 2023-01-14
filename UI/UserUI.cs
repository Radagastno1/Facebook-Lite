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
    IConversationManager _conversationExtraManager;
    IManager<Comment, User> _commentManager;
    IDeletionManager<User> _deletionManager;
    IMultipleDataGetter<User, int> _multipleUserData;
    IRelationsManager<User> _friendRelationsManager;
    IRelationsManager<User> _blockRelationsManager;
    IFriendManager<User> _friendManager;
    public Func<User, int, int> OnDialogue;
    public Func<List<User>, User, int> OnMakeConversation;
    public Action<User, int> OnMakeMessage;
    public Action<int, User> OnStart;
    List<ConsoleKey> keys = new();
    public Action<User> LoadFriends;

    public UserUI(IManager<User, User> userManager, IManager<Post, User> postManager, IManager<Conversation, User> conversationManager, IManager<Message, User> messageManager, IManager<Comment, User> commentManager, IDeletionManager<User> deletionManager, IMultipleDataGetter<User, int> multipleUserData, IRelationsManager<User> friendRelationsManager, IRelationsManager<User> blockRelationsManager, IFriendManager<User> friendManager, IConversationManager conversationExtraManager)
    {
        _userManager = userManager;
        _postManager = postManager;
        _conversationManager = conversationManager;
        _messageManager = messageManager;
        _commentManager = commentManager;
        _deletionManager = deletionManager;
        _multipleUserData = multipleUserData;
        _friendManager = friendManager;
        _friendRelationsManager = friendRelationsManager;
        _blockRelationsManager = blockRelationsManager;
        _conversationExtraManager = conversationExtraManager;
        deleted = _deletionManager.SetAsDeleted();  //deletar users som varit inaktiva i mer än 30 dagar när den startar
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
        if (user.ID == id)
        {
            MyPage(user);
            return;
        }
        while (true)
        {
            if (!ShowProfile(id, user)) return;
            ConsoleKey key = ConsoleInput.GetPressedKey("[C] Check out user   [R] Return", LogicTool.NewKeyList(ConsoleKey.C, ConsoleKey.R));
            if (key == ConsoleKey.R)
            {
                return;
            }
            string[] overviewOptions = new string[]
              {"[POSTS]","[MESSAGE]","[RETURN]", "[BLOCK USER]"};
            int menuOptions = 0;
            //denna nedan i friendsui getfriendshipstatus, eller showfriendshipstatus?;
            FriendsUI friendsUI = new(_friendRelationsManager, _friendManager, user);
            int status = friendsUI.GetFriendShipStatus(user, id);
            if (status == 1) overviewOptions = overviewOptions.Concat(new string[] { "[ADD FRIEND]" }).ToArray();
            else if (status == 3) overviewOptions = overviewOptions.Concat(new string[] { "[CONFIRM FRIEND REQUEST]" }).ToArray();
            else if (status == 4) overviewOptions = overviewOptions.Concat(new string[] { "[DELETE FRIEND]" }).ToArray();

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
                            // fixa nedanför bort med delegaten
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
                    BlockingsUI blockingsUI = new(_blockRelationsManager);
                    blockingsUI.BlockUser(user, id);
                    Console.ReadKey();
                    break;
                case 4:
                    if (status == 1 || status == 3)
                    {
                        friendsUI.FriendRequest(user, id);
                        LoadFriends?.Invoke(user);
                    }
                    else if (status == 4)
                    {
                        friendsUI.DeleteFriendship(user, id);
                    }
                    Console.ReadKey();
                    break;
            }
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
        conversationId = _conversationExtraManager.MakeNew(participants, user);
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
                FriendsUI friendsUI = new(_friendRelationsManager, _friendManager, user);
                friendsUI.ShowMyFriends(user);
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
        string[] overviewOptions = new string[]
        { "[EDIT PROFILE]","[BLOCKED USERS]","[DELETE ACCOUNT]", "[RETURN]"};
        int menuOptions = 0;

        menuOptions = ConsoleInput.GetMenuOptions(overviewOptions);
        switch (menuOptions)
        {
            case 0:
                EditInformation(user);
                user = _userManager.GetOne(user.ID, user);
                break;
            case 1:
                BlockingsUI blockingsUI = new(_blockRelationsManager);
                blockingsUI.ShowMyBlockedUsers(user);
                break;
            case 2:
                bool isDeleted = DeletingAccount(user);
                if (isDeleted == true) Environment.Exit(0);
                break;
            case 3:
                return;
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
        for (int i = 0; i < joinedNames.Length; i++)
        {
            viewName += joinedNames[i] + " ";
        }
        return viewName;
    }

    public bool ShowProfile(int id, User user)
    {
        FriendsUI friendsUI = new(_friendRelationsManager, _friendManager, user);
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
            List<Conversation> foundConversations = _conversationExtraManager.GetParticipantsPerConversation(ids);
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
        try
        {
            _userManager.Update(user);
            Console.WriteLine("Updated!");
        }
        catch(Exception e)
        {
            Console.WriteLine("Something went wrong. Error message: " + e);
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
}

