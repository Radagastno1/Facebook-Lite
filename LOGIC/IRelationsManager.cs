namespace LOGIC;
public interface IRelationsManager<T>  //BLOCKINGMANAGER OCH FRIENDMANAGER SOM IMPLEMENTERAR
{
    public int Create(T obj, int id); 
    public List<T> GetMine(T obj); 
    public void Update(T obj); 
    public int Delete(T obj, int id); 
}