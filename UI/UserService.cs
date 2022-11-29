using CORE;
using LOGIC;
namespace UI;
public class UserService
{
    IManager<User> _userManager;
    IManager<Post> _postManager;
    IManager<Comment> _commentsManager;
    PostService postService;
    public UserService(IManager<User> userManager, IManager<Post> postManager, IManager<Comment> commentsManager)
    {
        _userManager = userManager;
        _postManager = postManager;
        _commentsManager = commentsManager;
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
                        postService = new(_postManager,_commentsManager);
                        postService.MakePost(user);
                        break;
                    case 1:
                        postService = new(_postManager, _commentsManager);
                        string search = ConsoleInput.GetString("Search by name: ");
                        ShowSearches(search);
                        int id = ConsoleInput.GetInt("User to visit: ");
                        ShowProfile(id);
                        int postId = postService.ShowPosts(id);
                        if(postId != 0)
                        {
                            ConsoleKey key = postService.ChooseIfComment();
                            if(key == ConsoleKey.C)
                            {
                                postService.CommentPost(user, postId);
                            }
                            else if(key == ConsoleKey.V)
                            {
                                // visa kommentarer på posten
                                postService.ShowCommentsOnPost(postId);
                            }
                        }
                        else
                        {
                            continue;
                        }
                        //FIXA POST ID FRÅN POSTEN DEN Väljer
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
        User user = new(firstName, lastName, email, password, birthDate);
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
}