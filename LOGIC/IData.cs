namespace LOGIC;
public interface IData<T>
{
    List<T> GetAll();
    int? Create(T obj);
    int? Update(T obj);
    int? Delete(T obj);
}