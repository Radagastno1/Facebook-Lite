using CORE;
namespace UI;
public class ConversationResult
{
    public List<Conversation> conversations{get;set;} = new();
    public bool ConversationExists {get;set;}
}