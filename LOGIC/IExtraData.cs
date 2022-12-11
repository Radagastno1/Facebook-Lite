namespace LOGIC;
public interface IExtraData<T>
{
    public List<T> GetByIdAndText (int data, string text);
}