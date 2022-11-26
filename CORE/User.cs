using System.ComponentModel;
namespace CORE;
public class User
{
    public readonly int ID;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PassWord { get; set; }
    public DateOnly BirthDate { get; set; }
    public Genders Gender { get; private set; }
    public Roles Role { get; private set; }
    public string AboutMe { get; private set; }
    public User() { }
    public void SetGender(int enumNumber)
    {
        if (Enum.IsDefined(typeof(Genders), enumNumber))
        {
            Gender = (Genders)enumNumber;
        }
    }
    public void SetRole(int enumNumber)
    {
        if (Enum.IsDefined(typeof(Roles), enumNumber))
        {
            Role = (Roles)enumNumber;
        }
    }
    public enum Genders
    {
        Undecided,
        Woman,
        Man,
        Agender,
        Bigender,
        Nonbinary
    }
    public enum Roles
    {
        Undecided,
        Member,
        [Description("Customer Service")] CustomerService,
        Editor,
        Administrator,
        [Description("Super Admin")] SuperAdmin
    }
}