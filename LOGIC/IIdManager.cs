namespace LOGIC;
public interface IIdManager<T>
{
    public ConversationResult GetIds(List<int>data);
    public List<T> GetById(List<int>ids);
}