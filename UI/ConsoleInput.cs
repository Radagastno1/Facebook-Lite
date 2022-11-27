using System.Net.Mail;
namespace UI;
public class ConsoleInput
{
    public static int GetInt(string output)
    {
        int getInt = 0;
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
        } while(!success);
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
            if(getString.Length>=6 && getString.Any(char.IsUpper) && getString.Any(char.IsDigit))
            {
                success = true;
            }
        } while(!success);
        return getString;
    }
}