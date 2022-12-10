using LOGIC;
using CORE;
namespace UI;
public class SignUpUI
{
    IManager<User> _userManager;
    public SignUpUI(IManager<User> userManager)
    {
        _userManager = userManager;
    }
    public User UserSignUp()
    {
        string firstName = ConsoleInput.GetString("First name: ");
        string lastName = ConsoleInput.GetString("Last name: ");
        string email = ConsoleInput.GetEmail("Email: ");
        string password = ConsoleInput.GetPassword("Password(at least 6 characters, one uppercase letter and at least one digit):");
        string birthDate = ConsoleInput.GetBirthDate("Birthdate(YYYY-MM-DD): ");
        string gender = ConsoleInput.GetGender();
        User user = new(firstName, lastName, email, password, birthDate, gender);
        _userManager.Create(user);
        return user;
    }
}