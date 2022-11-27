namespace LOGIC;
public interface IManager<T>
{
    public int? Create(T obj);
    public List<T>GetBySearch(string search);
    public int? Remove(T obj);
    public T GetOne();
    public T Update();

}