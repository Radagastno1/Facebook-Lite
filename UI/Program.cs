using CORE;
using LOGIC;
using DATABASE;
using Dapper;
using MySqlConnector;
namespace UI;
internal class Program
{
    //KOLLA UPP EV TRIGGER ELLER ANNAT SOM KOLLAR AUTOMATISKT OM MAN ÄR BLOCKAD/INAKTIV/DELETED
    static UserManager userManager = new(new UsersDB(), new UsersDB(), new UsersDB(), new UsersDB());
    static PostsManager postsManager = new(new PostsDB(), new PostsDB(), new PostsDB());
    static CommentsManager commentsManager = new(new CommentsDB(), new CommentsDB());
    static ConversationManager conversationManager = new(new ConversationDB(), new ConversationDB(), new MessagesDB(), new ConversationDB(), new ConversationDB());
    static MessgageManager messageManager = new(new MessagesDB(), new MessagesDB());
    static FriendManager friendManager = new(new FriendsDB(), new FriendsDB());
    static BlockingManager blockingManager = new(new BlockingsDB());
    static SignUpUI signUpUI = new(userManager);
    static LogInManager logInManager = new(new LogInDB());
    static LogInUI logInUI = new(logInManager);
    static UserUI userUI = new(userManager, postsManager, conversationManager, conversationManager, messageManager, commentsManager, userManager, userManager, friendManager, blockingManager);
    static PostUI postUI = new(postsManager, commentsManager);
    static ConversationUI conversationUI = new(conversationManager, messageManager, conversationManager, conversationManager);
    static MessageUI messageUI = new(messageManager);
    //ATT FIXA
    //1. Fixa mer i UI, rensa ut metoder till conversationservice osv
    //2. om man är inaktiv/raderad och har en dialog-konversation ska den stå som is_visible = false 
    //2.5 lägg till is_visible på conversations table
    //4. om man blockar eller blir blockad ska vänskapen deletas från båda håll!
    //5. fixa in string title som inparamter i menymetoden! så det blir som login sidan med title = facebook
    private static void Main(string[] args)
    {
        // dessa under ska inte vara delegat, var bara som övning i början 
        userUI.OnDialogue += conversationUI.ShowDialogue;
        userUI.OnMakeMessage += messageUI.MakeMessage;
        userUI.OnMakeConversation += conversationUI.MakeNewConversation;
        // userUI.OnShow += postUI.ShowPosts;
        UsersDB usersDB = new();
        FriendsDB friendsDB = new();
        userManager.OnDelete += usersDB.UpdateToDeleted;
        logInUI.OnLoggedIn += friendManager.SetToFriends;
        logInUI.OnLoggedIn += friendManager.LoadMyFriends;
        userUI.LoadFriends += friendManager.SetToFriends;
        userUI.LoadFriends += friendManager.LoadMyFriends;
        blockingManager.OnBlockUser += friendsDB.Delete;

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
                            // userUI.ShowMyFacebook(user);
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
                    userUI.Messenger(user);
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
}