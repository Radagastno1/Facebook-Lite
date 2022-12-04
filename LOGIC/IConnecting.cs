namespace LOGIC;
public interface IConnecting<T>
{
    public int? MakeNew(List<T> objects, T obj);
}