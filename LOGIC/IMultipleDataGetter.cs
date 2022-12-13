namespace LOGIC;
public interface IMultipleDataGetter<Tone, Ttwo>
{
    public List<Tone> GetUsersById(List<Ttwo> data);
}

