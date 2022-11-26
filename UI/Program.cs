namespace UI;
internal class Program
{
    private static void Main(string[] args)
    {
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
                        //LOG IN PAGE
                        break;
                    case 1:
                    UserService userService = new();
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