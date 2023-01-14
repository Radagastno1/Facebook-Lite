namespace CORE;
public class Notification
{
    public int Id{get;set;}
    public string NotificationType{get;set;}
    public string Description{get;set;}
    public User FromUser{get;set;}
    public User ToUser{get;set;}
    public Notification()
    {

    }
    public override string ToString()
    {
        return $"{FromUser.FirstName} {FromUser.LastName} {Description}";
    }
}