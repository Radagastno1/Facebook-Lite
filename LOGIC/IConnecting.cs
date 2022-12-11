namespace LOGIC;
public interface IConnectingMultiple<T>
{
    public int? MakeNew(List<T> objects, T obj);
}