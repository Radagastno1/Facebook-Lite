using CORE;
namespace LOGIC;
public class ConversationResult
{
    public List<Conversation> conversations{get;set;} = new();
    public bool ConversationExists {get;set;}
}