namespace LOGIC;
public interface IRelationsData<T>
{
    public int Create(T obj, int id);
    public int Delete(T obj, int id);
    public List<T> GetMine(T obj);
    public int Update(T obj, int id);
}