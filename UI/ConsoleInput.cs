using System.Net.Mail;
namespace UI;
public class ConsoleInput
{
    public static int GetInt(string output)
    {
        int getInt;
        bool success = false;
        do
        {
            Console.WriteLine(output);
            success = int.TryParse(Console.ReadLine(), out getInt);
        } while (!success);
        return getInt;
    }
    public static string GetString(string output)
    {
        string getString = string.Empty;
        do
        {
            Console.WriteLine(output);
            getString = Console.ReadLine();
        } while (string.IsNullOrEmpty(getString));
        return getString;
    }
    public static string GetBirthDate(string output)
    {
        //dela upp till tre ints
        bool success;
        string dateString = string.Empty;
        do
        {
            Console.WriteLine(output);
            dateString = Console.ReadLine();
            string[] array = dateString.Split('-');
            if (array.Length == 3)
            {
                success = true;
            }
            else
            {
                success = false;
            }
        } while (!success);
        return dateString;
    }
    public static string GetEmail(string output)
    {
        string emailString = string.Empty;
        bool success = true;
        MailAddress mailAdress = new MailAddress("test@hotmail.com");
        do
        {
            try
            {
                Console.WriteLine(output);
                emailString = Console.ReadLine();
                mailAdress = new MailAddress(emailString);
                success = true;
            }
            catch
            {
                success = false;
            }
        } while (!success);
        return mailAdress.ToString();
    }
    public static string GetPassword(string output)
    {
        //måste innehålla minst 6 långt, en stor bokstav, minst en siffra
        string getString = string.Empty;
        bool success = false;
        do
        {
            Console.WriteLine(output);
            getString = Console.ReadLine();
            if (getString.Length >= 6 && getString.Any(char.IsUpper) && getString.Any(char.IsDigit))
            {
                success = true;
            }
        } while (!success);
        return getString;
    }

    public static ConsoleKey GetPressedKey(string output, List<ConsoleKey> keys)
    {
        bool success = false;
        ConsoleKey key;
        do
        {
            Console.WriteLine(output);
            key = Console.ReadKey().Key;
            foreach (ConsoleKey item in keys)
            {
                if (item == key)
                {
                    success = true;
                }
            }
        } while (!success);
        return key;
    }

    public static string GetGender()
    {
        int index = 0;
        int genderInt = 0;
        bool success = false;
        CORE.User.Genders genders = CORE.User.Genders.Undecided;
        foreach (string Name in Enum.GetNames(typeof(CORE.User.Genders)))
        {
            Console.WriteLine($"[{index}] {Name}");
            index++;
        }
        do
        {
            genderInt = ConsoleInput.GetInt("Choose gender: ");
            foreach (int Number in Enum.GetValues(typeof(CORE.User.Genders)))
            {
                if (genderInt == Number)
                {
                    genders = (CORE.User.Genders)genderInt;
                    success = true;
                    break;
                }
            }
        } while (!success);
        string gender = ((CORE.User.Genders)genderInt).ToString();
        return gender;
    }

}