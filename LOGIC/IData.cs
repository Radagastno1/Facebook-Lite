namespace LOGIC;
//inspo från Petrus interface i blodbanken-projekt
public interface IData<T>
{
    List<T> Get();
    int Create(T obj);
    int Update(T obj);
    int Delete(T obj);
}