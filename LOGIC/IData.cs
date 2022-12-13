namespace LOGIC;
public interface IData<T>
{
    List<T> GetAll();
    List<T> GetById(int id);
    int? Create(T obj);
    int? Update(T obj);
    int? Delete(T obj);
}