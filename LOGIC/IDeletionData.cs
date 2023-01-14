namespace LOGIC;
public interface IDeletionData<T>  //userdb implementerar denna
{
    public List<T> GetInactive();
    public void UpdateToDeleted(T obj); 
}