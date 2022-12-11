namespace LOGIC;
public interface IDeletionData<T>
{
    public List<T> GetInactive();
    public int? UpdateToDeleted(T obj); 
}