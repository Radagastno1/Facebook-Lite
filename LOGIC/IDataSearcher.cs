namespace LOGIC;
public interface IDataSearcher<T>  //BARA USERDB SOM IMPLEMENTERAR
{
    public List<T> GetSearches(string search);
}