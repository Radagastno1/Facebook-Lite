namespace LOGIC;
public interface IDeletionData<T>
{
    public List<T> GetInactive();
    public void UpdateToDeleted(T obj); 
}