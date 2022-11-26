namespace UI;
public class UserPage
{
    public void ShowUserOverView()
    {
        //ska denna vara i main? som main menyn typ när man loggat in?
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

            if (keyPressed.Key == ConsoleKey.DownArrow && menuOptions != overviewOptions.Length-1)
            {
                menuOptions++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuOptions >= 1)
            {
                menuOptions--;
            }
            else if(keyPressed.Key == ConsoleKey.Enter)
            {
                switch(menuOptions)
                {
                    case 0:
                    //MAKEWALLPOST
                    break;
                    case 1:
                    //SEARCHFORUSERS
                    break;
                    case 2:
                    //CHATPAGE
                    break;
                    case 3:
                    //SHOWMYPAGE
                    break;
                    case 4:
                    //SETTINGSMENU
                    break;
                }
            }
        }

    }
}