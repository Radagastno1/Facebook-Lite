namespace LOGIC;
public interface IConversationData<Tone, Ttwo>
{
    public List<Tone> GetConversationsOfSpecificParticipants (int data, string text);
    public Tone GetDialogueId(int userId, int id);
     public Ttwo GetConversationIdAndParticipantNames(int data);
}