namespace CORE;
public class Conversation
{
    public int ID{get;private set;}
    public DateTime DateCreated{get;set;}
    public User Creator{get;set;}
    List<User> participants = new();
    List<Message> messages = new();
    public Conversation(){}
    public Conversation(User aCreator)
    {
        Creator = aCreator;
        DateCreated = DateTime.Now;
    }
}