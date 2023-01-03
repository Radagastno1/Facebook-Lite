namespace LOGIC;
public interface IIdManager<Tone, Ttwo>
{
    public ConversationResult GetIds(List<int> data);
    public List<Tone> GetById(List<int> ids);
    public Tone GetDialogueId(int userId, int id);
    public List<Tone> GetParticipantsPerConversation(List<int> ids);
    public List<int> GetAllMyConversationsIds(Ttwo obj);
}