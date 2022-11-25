namespace LOGIC;
public class Message
{
    public readonly int ID;
    public string Content{get;set;}
    public DateOnly DateCreated{get;set;}
    public string Reciever{get;set;}
    public string Sender{get;set;}
    public int SenderId{get;set;}
    public int ConversationId{get;set;}
    public List<User>participants = new();
    public Message(){}

}