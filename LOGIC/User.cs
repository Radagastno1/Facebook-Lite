namespace LOGIC;
public class User
{
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string Email{get;set;}
    public string PassWord{get;set;}
    public DateOnly BirthDate{get;set;}
    public Genders Gender{get; private set;}
    public string AboutMe{get;set;}
    public User(){}
    public void SetGender(int enumNumber)
    {
        if(Enum.IsDefined(typeof(Genders), enumNumber))
        {
            Gender = (Genders)enumNumber;
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
}