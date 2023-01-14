namespace LOGIC;
public interface IMultipleDataGetter<Tone, Ttwo>  //USER MANAGER IMPLEMENTS THIS
{
    public List<Tone> GetUsersById(List<Ttwo> data, Tone obj);
}

