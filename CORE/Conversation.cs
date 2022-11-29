namespace CORE;
public class Conversation
{
    public int ID{get;set;}
    public DateTime DateCreated{get;set;}
    public User Creator{get;set;}
    List<User> Participants{get;set;}
    List<Message> messages = new();
    public int ParticipantId{get;set;}
    public int CreatorId{get;set;}
    public Conversation(){}
    public Conversation(User aCreator, List<User> aParticipants)
    {
        Creator = aCreator;
        DateCreated = DateTime.Now;
        Participants = aParticipants;
    }
}