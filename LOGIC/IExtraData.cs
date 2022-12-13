namespace LOGIC;
public interface IExtraData<T>
{
    public List<T> GetByIdAndText (int data, string text);
    public T GetDialogueId(int userId, int id);
}