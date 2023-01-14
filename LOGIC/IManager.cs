namespace LOGIC;
public interface IManager<Tone, Ttwo>  //de vanliga managers implementerar denna
{
    public int? Create(Tone obj);
    public List<Tone>GetBySearch(string search, Ttwo obj);
    public int? Remove(Tone obj);
    public Tone GetOne(int data, Ttwo obj);
    public List<Tone> GetAll(int data, Ttwo obj);
    public int? Update(Tone obj);

}