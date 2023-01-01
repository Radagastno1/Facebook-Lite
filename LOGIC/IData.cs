namespace LOGIC;
public interface IData<Tone, Ttwo>
{
    List<Tone> GetAll(Ttwo obj);
    int? Create(Tone obj);
    int? Update(Tone obj);
    int? Delete(Tone obj);
}