namespace LOGIC;
public interface IDataToObject<Tone, Ttwo> //postsdb and usersdb implements this
{
    Tone GetOne(int id, Ttwo bjo);
}