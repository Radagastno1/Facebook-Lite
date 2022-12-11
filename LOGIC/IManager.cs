namespace LOGIC;
public interface IManager<T>
{
    public int? Create(T obj);
    public List<T>GetBySearch(string search);
    public int? Remove(T obj);
    public T GetOne(int data);
    public List<T> GetAll(int data);
    public int? Update(T obj);

}