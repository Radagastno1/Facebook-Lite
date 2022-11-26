using System.ComponentModel;
namespace CORE;
public class Post
{
    public readonly int ID;
    public string Content{get;set;}
    public DateOnly DateCreated{get;set;}
    public int UserId{get;private set;} // detta istället för user
    public User User{get;private set;}
    List<Comment>comments = new();
    public Post(string aContent, DateOnly aDateCreated, User aUser)
    {
        Content = aContent;
        DateCreated = aDateCreated;
        User = aUser;
    }
    public override string ToString()
    {
        return $"\t{User.FirstName} {User.LastName}\n\r\t{DateCreated.ToShortDateString()}\n\r\t{Content}";
    }
}