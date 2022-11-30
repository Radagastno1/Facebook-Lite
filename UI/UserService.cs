using CORE;
using LOGIC;
namespace UI;
public class UserService
{
    IManager<User> _userManager;
    IManager<Post> _postManager;
    IManager<Comment> _commentsManager;
    IManager<Conversation> _conversationManager;
    IManager<Message> _messageManager;
    PostService postService;
    MessageService messageService;
    public UserService(IManager<User> userManager, IManager<Post> postManager, IManager<Comment> commentsManager, IManager<Conversation> conversationManager, IManager<Message> messageManager)
    {
        _userManager = userManager;
        _postManager = postManager;
        _commentsManager = commentsManager;
        _conversationManager = conversationManager;
        _messageManager = messageManager;
    }
    public void ShowUserOverView(User user)
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
                        postService = new(_postManager, _commentsManager);
                        postService.MakePost(user);
                        break;
                    case 1:
                        postService = new(_postManager, _commentsManager);
                        string search = ConsoleInput.GetString("Search by name: ");
                        ShowSearches(search);
                        int id = ConsoleInput.GetInt("User to visit: ");
                        ShowProfile(id);
                        List<ConsoleKey> keys = new();
                        keys.Add(ConsoleKey.M); keys.Add(ConsoleKey.P);
                        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[M] Message  [P] Posts", keys);
                        if (pressedKey == ConsoleKey.M)
                        {
                            Console.WriteLine("startar konversation här");
                            ConversationService conversationService = new(_conversationManager);
                            //hämtar personen som man besöker och skickar in i konversation
                            User participant = _userManager.GetOne(id, 0);
                            //kolla om konversation finns, annars starta en ny med denna person!SEN gör detta
                            List<User> participants = new();
                            participants.Add(participant);
                            int conversationId = conversationService.StartConversation(user, participants).GetValueOrDefault();
                            //kolla om konversation finns, annars starta en ny med denna person!SEN gör detta över
                            //VISA KONVERSATIONEN SEDAN HÄR
                            messageService = new(_messageManager);
                            messageService.MakeMessage(user, conversationId);
                        }
                        else
                        {
                            int postId = postService.ShowPosts(id);
                            if (postId != 0)
                            {
                                ConsoleKey key = postService.ChooseIfComment();
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
                        Console.ReadLine();
                        break;
                    case 2:
                        //CHATPAGE
                        break;
                    case 3:
                        postService = new(_postManager, _commentsManager);
                        ShowProfile(user.ID);
                        postService.ShowPosts(user.ID);
                        Console.ReadLine();
                        break;
                    case 4:
                        //SETTINGSMENU
                        EditInformation(user);
                        //hämta alla uppdaterade uppgifter till usern
                        user = _userManager.GetOne(user.ID, 0);
                        break;
                }
            }
        }
    }
    public User UserSignUp()
    {
        string firstName = ConsoleInput.GetString("First name: ");
        string lastName = ConsoleInput.GetString("Last name: ");
        string email = ConsoleInput.GetEmail("Email: ");
        string password = ConsoleInput.GetPassword("Password(at least 6 characters, one uppercase letter and at least one digit):");
        //validera date metod i consoleinput
        string birthDate = ConsoleInput.GetBirthDate("Birthdate(YYYY-MM-DD): ");
        //visa genders alternativ
        string gender = ConsoleInput.GetGender();
        User user = new(firstName, lastName, email, password, birthDate, gender);
        _userManager.Create(user);
        return user;
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
        PostService postService = new(_postManager, _commentsManager);
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
}