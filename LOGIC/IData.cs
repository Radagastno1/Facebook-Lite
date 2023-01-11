using CORE;
namespace LOGIC;
public interface IData<Tone>
{
    List<Tone> GetAll(User user, string query);
    int? Create(Tone obj, string query);
    int? Update(Tone obj, string query);
    int? Delete(Tone obj, string query);
}