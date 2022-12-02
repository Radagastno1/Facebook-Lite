namespace LOGIC;
public interface IExtraData<T>
{
    public List<T> GetManyByData (int data, string text);
}