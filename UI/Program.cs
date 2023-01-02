using CORE;
using LOGIC;
using DATABASE;
namespace UI;
internal class Program
{
    //KOLLA UPP EV TRIGGER ELLER ANNAT SOM KOLLAR AUTOMATISKT OM MAN ÄR BLOCKAD/INAKTIV/DELETED
    static UserManager userManager = new(new UsersDB(), new UsersDB(), new UsersDB(), new UsersDB());
    static PostsManager postsManager = new(new PostsDB(), new PostsDB(), new PostsDB());
    static CommentsManager commentsManager = new(new CommentsDB(), new CommentsDB());
    static ConversationManager conversationManager = new(new ConversationDB(), new ConversationDB(), new MessagesDB(), new ConversationDB());
    static MessgageManager messageManager = new(new MessagesDB(), new MessagesDB());
    static FriendManager friendManager = new(new FriendsDB(), new FriendsDB());
    static BlockingManager blockingManager = new(new BlockingsDB());
    static SignUpUI signUpUI = new(userManager);
    static LogInManager logInManager = new(new LogInDB());
    static LogInUI logInUI = new(logInManager);
    static UserUI userUI = new(userManager, postsManager, conversationManager, conversationManager, messageManager, commentsManager, userManager, userManager, friendManager, blockingManager, friendManager, conversationManager);
    static PostUI postUI = new(postsManager, commentsManager);
    static ConversationUI conversationUI = new(conversationManager, messageManager, conversationManager, conversationManager);
    static MessageUI messageUI = new(messageManager, conversationManager);
    //ATT FIXA
    //2. om man är inaktiv/raderad och har en dialog-konversation ska den stå som is_visible = false 
    //2.5 lägg till is_visible på conversations tables
    //4. om man blockar eller blir blockad ska man ej kunna se kommentarer osv + ladda in vänner på nytt
    //5. fixa in string title som inparamter i menymetoden! så det blir som login sidan med title = facebook
    //6. när man är blockad/har blockat ska man kunna se konv. man haft men ej kunna skriva fler medd så länge
    private static void Main(string[] args)
    {
        UsersDB usersDB = new();
        FriendsDB friendsDB = new();
        BlockingsUI blockingsUI = new(blockingManager);
        // dessa under ska inte vara delegat, var bara som övning i början 
        userUI.OnDialogue += conversationUI.ShowDialogue;
        userUI.OnMakeMessage += messageUI.MakeMessage;
        userUI.OnMakeConversation += conversationUI.MakeNewConversation;

        userManager.OnDelete += usersDB.UpdateToDeleted;
        logInUI.OnLoggedIn += friendManager.Update;
        logInUI.OnLoggedIn += friendManager.LoadFriends;
        userUI.LoadFriends += friendManager.Update;
        userUI.LoadFriends += friendManager.LoadFriends;
        blockingManager.OnBlockUser += friendsDB.Delete;
        // blockingsUI.OnBlockUser += friendManager.LoadFriends;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.BackgroundColor = ConsoleColor.White;
        string title = @"   _____ _    ____ _____ ____   ___   ___  _  __  _     ___ _____ _____ 
  |  ___/ \  / ___| ____| __ ) / _ \ / _ \| |/ / | |   |_ _|_   _| ____|
  | |_ / _ \| |   |  _| |  _ \| | | | | | | ' /  | |    | |  | | |  _|  
  |  _/ ___ \ |___| |___| |_) | |_| | |_| | . \  | |___ | |  | | | |___ 
  |_|/_/   \_\____|_____|____/ \___/ \___/|_|\_\ |_____|___| |_| |_____|";

        string[] logInOptions = new string[]
        { "[LOG IN]","[SIGN UP]","[FORGOT PASSWORD?]" };
        int menuOptions = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);
            for (int i = 0; i < logInOptions.Length; i++)
            {
                Console.WriteLine((i == menuOptions ? "\t>>" : "\t") + logInOptions[i]);
            }
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && menuOptions != logInOptions.Length - 1)
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
                        User? user = new();
                        user = logInUI.LogIn();
                        if (user != null)
                        {
                            FriendsUI friendsUI = new(friendManager, friendManager, user);
                            friendsUI.LoadFriends += friendManager.Update;
                            friendsUI.LoadFriends += friendManager.LoadFriends;
                            ShowMyFacebook(user);
                        }
                        break;
                    case 1:
                        signUpUI.UserSignUp();
                        //returnerar user för nu, vill att mail ska skickas ut till 
                        //userns mail sedan eventuellt för att validera
                        break;
                    case 2:
                        //FORGOT PASSWORD - EMAIL-SERVICE för utveckling
                        break;
                }
            }
        }
    }
    public static void ShowMyFacebook(User user)
    {
        // FriendsUI friendsUI = new(friendManager, user);
        // friendsUI.OnFriendUI += friendManager.LoadMyFriends;
        string[] overviewOptions = new string[]
        { "[PUBLISH]","[SEARCH]","[MESSENGER]", "[MY PAGE]","[SETTINGS]", "[LOG OUT]" };
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
                    int id = userUI.Searcher(user);
                    if (id != 0) userUI.InteractWithUser(user, id);
                    Console.ReadKey();
                    break;
                case 2:
                    Messenger(user);
                    Console.ReadKey();
                    break;
                case 3:
                    userUI.MyPage(user);
                    Console.ReadKey();
                    break;
                case 4:
                    userUI.MySettings(user);
                    Console.ReadKey();
                    break;
                case 5:
                    return;
            }
        }
    }
    public static void Messenger(User user)
    {
        List<int> ids = new();
        ids.Add(user.ID);
        List<Conversation> foundConversations = new();
        List<string> conversationToList = new();
        conversationToList.Add("[New Conversation]");
        conversationToList.Add("[Return]");

        foundConversations = conversationManager.GetParticipantsPerConversation(ids);
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
                    userUI.AddPeopleToNewConversation(user);
                    break;
                case 1:
                    return;
                case int n when (n > 1):
                    int conversationsId = foundConversations[n - 2].ID;
                    messageUI.ShowMessages(conversationsId, user);
                    messageUI.MakeMessage(user, conversationsId);
                    Console.ReadKey();
                    break;
            }
        }
    }
}