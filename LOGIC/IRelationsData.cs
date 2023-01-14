namespace LOGIC;
public interface IRelationsData<T>   //BLOCKINGDB AND FRIENDDB IMPLEMENTS THIS 
{
    public int Create(T obj, int id);
    public int Delete(T obj, int id);
    public List<T> GetMine(T obj);
    public int Update(T obj, int id);
}