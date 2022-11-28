using CORE;
using LOGIC;
using DATABASE; //ska ui känna till databas?? pga usermanager
namespace UI;
internal class Program
{
    //MÅSTE FÅ POSTS ATT VISAS!!!!
    private static void Main(string[] args)
    {
        UserManager userManager = new(new UsersDB());
        PostsManager postsManager = new(new PostsDB());
        UserService userService = new(userManager, postsManager);
        PostService postService = new(postsManager);
        //MENYN INSPIRERAD AV PETRUS BLODBANKEN PROJEKT
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
                        User user = new();
                        LogInService logInService = new();
                        user = logInService.LogIn();
                        if (user != null)
                        {
                            userService.ShowUserOverView(user);
                        }
                        // om det ej var lyckat så är user null här, så i userpage får man kolla om user
                        //är null eller ej
                        break;
                    case 1:
                        userService.UserSignUp();
                        //SIGN UP - MAKE NEW USER
                        break;
                    case 2:
                        //FORGOT PASSWORD - EMAIL-SERVICE
                        break;
                }
            }
        }


    }
}