using CORE;
using LOGIC;
namespace UI;
public class PostService
{
    IManager<Post> _postManager;
    public PostService(IManager<Post> postManager)
    {
        _postManager = postManager;
    }
    public void ShowPosts(int userId)
    {
        List<Post> allPosts = new();
        try
        {
            allPosts = _postManager.GetAll(userId);
            foreach (Post item in allPosts)
            {
                Console.WriteLine(item.ToString());
            }
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("\tNo posts yet..");
        }
    }
}