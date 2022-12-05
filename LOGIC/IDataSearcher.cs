namespace LOGIC;
public interface IDataSearcher<T>
{
    public List<T> GetSearches(string search);
}