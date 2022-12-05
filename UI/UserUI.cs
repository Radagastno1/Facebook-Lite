using LOGIC;
using CORE;
namespace UI;
public class UserUI
{
    IManager<User> _userManager;
    IManager<Post> _postManager;
    IManager<Conversation> _conversationManager;
    IManager<Message> _messageManager;
    IIdManager<Conversation> _idManager;
    IConnecting<User> _connectionManager;
    IManager<Comment> _commentManager;
    List<ConsoleKey> keys = new();
    public UserUI(IManager<User> userManager, IManager<Post> postManager, IManager<Conversation> conversationManager, IIdManager<Conversation> idManager, IManager<Message> messageManager, IConnecting<User> connectingManager, IManager<Comment> commentManager)
    {
        _userManager = userManager;
        _postManager = postManager;
        _conversationManager = conversationManager;
        _idManager = idManager;
        _messageManager = messageManager;
        _connectionManager = connectingManager;
        _commentManager = commentManager;
    }
    public List<ConsoleKey> NewKeyList(ConsoleKey key1, ConsoleKey key2)
    {
        keys = new();
        keys.Add(key1);
        keys.Add(key2);
        return keys;
    }
    public void ShowFacebookOverview(User user)
    {
        string[] overviewOptions = new string[]
        { "[PUBLISH]","[SEARCH]","[CHAT]", "[MY PAGE]","[SETTINGS]" };
        int menuOptions = 0;
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < overviewOptions.Length; i++)
            {
                Console.WriteLine((i == menuOptions ? "\t>>" : "\t") + overviewOptions[i]);
            }
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && menuOptions != overviewOptions.Length - 1)
            {
                menuOptions++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuOptions >= 1)
            {
                menuOptions--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                switch (menuOptions)
                {
                    case 0:
                        MakePost(user);
                        break;
                    case 1:
                        int conversationId = 0;
                        string search = ConsoleInput.GetString("Search by name: ");
                        ShowSearches(search);
                        int id = ConsoleInput.GetInt("User to visit: ");
                        ShowProfile(id);
                        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[M] Message  [P] Posts", NewKeyList(ConsoleKey.M, ConsoleKey.P));
                        if (pressedKey == ConsoleKey.M)
                        {
                            List<int> ids = new();
                            ids.Add(id);
                            ids.Add(user.ID);
                            List<Conversation>? conversations = GetConversations(ids);
                            if (conversations != null)
                            {
                                ShowConversations(conversations);
                                conversationId = ConsoleInput.GetInt("Choose: ");
                                ShowMessages(conversationId);
                            }
                            else
                            {
                                pressedKey = ConsoleInput.GetPressedKey("[S]Start conversation  [R] Return", NewKeyList(ConsoleKey.S, ConsoleKey.R));
                                if (pressedKey == ConsoleKey.S)
                                {
                                    ids = new();
                                    ids.Add(id);
                                    List<User> participants = GetUsersById(ids);
                                    conversationId = _connectionManager.MakeNew(participants, user).GetValueOrDefault();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            MakeMessage(user, conversationId);
                        }
                        else
                        {
                            int postId = ShowPosts(id);
                            if (postId != 0)
                            {
                                ConsoleKey key = ConsoleInput.GetPressedKey("\t[C] Comment   [V] View Comments", NewKeyList(ConsoleKey.C, ConsoleKey.V));
                                if (key == ConsoleKey.C)
                                {
                                    CommentPost(user, postId);
                                }
                                else if (key == ConsoleKey.V)
                                {
                                    ShowCommentsOnPost(postId);
                                }
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        //CHATPAGE
                        ShowConversationParticipants(user.ID);
                        pressedKey = ConsoleInput.GetPressedKey($"[C] Choose conversation  [N] New Conversation", NewKeyList(ConsoleKey.C, ConsoleKey.N));
                        if (pressedKey == ConsoleKey.C)
                        {
                            conversationId = ConsoleInput.GetInt("Choose: ");
                            ShowMessages(conversationId);
                            MakeMessage(user, conversationId);
                        }
                        else
                        {
                             //söka efter personer att lägga till
                            string userName = ConsoleInput.GetString($"Search for user by name: ");
                            //visa lista med deras idn och för och efternamn

                            //välja deras idn
                            //när jag sökt klart så ska man kunna skapa konv. mellan oss
                            //och därifrån skriva meddelanden inom konversationen
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        ShowProfile(user.ID);
                        ShowPosts(user.ID);
                        Console.ReadKey();
                        break;
                    case 4:
                        //SETTINGSMENU
                        EditInformation(user);
                        user = _userManager.GetOne(user.ID, 0);
                        break;
                }
            }
        }
    }
    public void ShowMyPage()
    {

    }
    public void ShowOverlook()
    {

    }
    public void ShowChat(int id)
    {
        List<int> ids = new();
        ids.Add(id);
        List<Conversation>? conversations = GetConversations(ids);
        Console.WriteLine($"Chats");
        foreach (Conversation item in conversations)
        {
            Console.WriteLine(item.ToString());
        }
        //id på konversationen, alla konversationer i asc ordning
    }
    public void MakePost(User user)
    {
        string content = ConsoleInput.GetString("What's on your mind?");
        Post post = new(content, DateTime.Now, user.ID);
        _postManager.Create(post);
    }
    public void ShowSearches(string search)
    {
        List<User> users = _userManager.GetBySearch(search);
        if (users != null)
        {
            foreach (User item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
    public void ShowProfile(int id)
    {
        User user = _userManager.GetOne(id, 0);
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
    public List<Conversation>? GetConversations(List<int> ids)
    {
        //denna i logik?

        if (_idManager.GetIds(ids).ConversationExists == true)
        {
            return _idManager.GetIds(ids).Conversations;
        }
        return null;
    }
    public void ShowConversationParticipants(int id)
    {
        //KOLLA DENNA CONVER.BLR NULL SISTA
        List<int> ids = new();
        ids.Add(id);
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
    {//DENNA SKA VARA I LOGIK
        List<User> participants = new();
        foreach (int id in ids)
        {
            User participant = _userManager.GetOne(id, 0);
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
    public int ShowPosts(int userId)
    {
        List<Post> allPosts = new();
        try
        {
            allPosts = _postManager.GetAll(userId);
            foreach (Post item in allPosts)
            {
                Console.WriteLine($"{item.ToString()}\n");
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("\tNo posts yet..");
        }
        int postId = ConsoleInput.GetInt("[0] Return   [ChoosePost] See Post");
        return postId;
    }
    public void CommentPost(User user, int postId)
    {
        string content = ConsoleInput.GetString("Leave a comment: ");
        Comment comment = new Comment(content, DateTime.Now, user.ID, postId);
        _commentManager.Create(comment);
    }
    public void ShowCommentsOnPost(int postId)
    {
        try
        {
            List<Comment> comments = _commentManager.GetAll(postId);
            foreach (Comment item in comments)
            {
                Console.WriteLine($"{item.ToString()}\n");
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("No comments yet..");
        }
    }
}