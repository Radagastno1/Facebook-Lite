namespace LOGIC;
public interface IData<Tone, Ttwo>
{
    List<Tone> GetAll();
    List<Tone> GetById(int id, Ttwo bjo);
    int? Create(Tone obj);
    int? Update(Tone obj);
    int? Delete(Tone obj);
}