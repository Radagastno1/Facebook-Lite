namespace LOGIC;
public interface IDataToList<Tone, Ttwo>  //comments, conversation, message, post-db implementerar denna
{
      List<Tone> GetById(int id, Ttwo bjo);
}