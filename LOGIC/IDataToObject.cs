namespace LOGIC;
public interface IDataToObject<T>
{
    T GetById(int id, T bjo);
}