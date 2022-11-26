namespace UI;
internal class Program
{
    private static void Main(string[] args)
    {
        string title = @"   _____ _    ____ _____ ____   ___   ___  _  __  _     ___ _____ _____ 
  |  ___/ \  / ___| ____| __ ) / _ \ / _ \| |/ / | |   |_ _|_   _| ____|
  | |_ / _ \| |   |  _| |  _ \| | | | | | | ' /  | |    | |  | | |  _|  
  |  _/ ___ \ |___| |___| |_) | |_| | |_| | . \  | |___ | |  | | | |___ 
  |_|/_/   \_\____|_____|____/ \___/ \___/|_|\_\ |_____|___| |_| |_____|";
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine(title);
        while (true)
        {
            UserPage userPage = new();
            userPage.ShowUserOverView();
        }


    }
}