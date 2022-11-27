using CORE;
namespace UI;
public class UserService
{
    public void ShowUserOverView()
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

    public User UserSignUp()
    {
        string firstName = ConsoleInput.GetString("First name: ");
        string lastName = ConsoleInput.GetString("Last name: ");
        string email = ConsoleInput.GetEmail("Email: ");
        string password = ConsoleInput.GetPassword("Password(at least 6 characters, one uppercase letter and at least one digit):");
        //validera date metod i consoleinput
        string birthDate = ConsoleInput.GetString("Birthdate(YYYY-MM-DD): "); 
        //visa genders alternativ
        User user = new(firstName, lastName, email, password, birthDate);
        return user;
    }
}