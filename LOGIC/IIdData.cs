namespace LOGIC;
public interface IIdData<T>
{
    public ConversationResult GetIds(int data);
    public List<T>GetById(int data);
}