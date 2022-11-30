namespace LOGIC;
//inspo från Petrus interface i blodbanken-projekt
public interface IData<T>
{
    List<T> Get();
    T GetById(int data1, int data2);
    int? Create(T obj);
    int? Update(T obj);
    int? Delete(T obj);
}