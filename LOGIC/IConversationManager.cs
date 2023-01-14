using CORE;
namespace LOGIC;
public interface IConversationManager  //conversationsmanager implementerar denna
{
    public int MakeNew(List<User> participants, User user);
    public ConversationResult GetIds(List<int> data);
    public List<Conversation> GetById(List<int> ids);
    public Conversation GetDialogueId(int userId, int id);
    public List<Conversation> GetParticipantsPerConversation(List<int> ids);
    public List<int> GetAllMyConversationsIds(User user);
}