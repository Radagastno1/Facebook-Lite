namespace LOGIC;
public interface IBlockingData<T>
{
    public int Create(T obj, int id);
    public int Delete(T obj, int id);
    public List<T> GetMine(T obj);

}