using CORE;
namespace LOGIC;
public class PostsManager : IManager<Post>
{
    IData<Post> _postData;
    public PostsManager(IData<Post> postData)
    {
        _postData = postData;
    }
    public int? Create(Post post)
    {
        return _postData.Create(post);
    }
    public List<Post> GetBySearch(string search)
    {
        List<Post> searchedPosts = new();
        try
        {
            List<Post> allPosts = _postData.Get();
            foreach (Post post in allPosts)
            {
                if (post.Content.ToLower().Contains(search.ToLower()))
                {
                    searchedPosts.Add(post);
                }
            }
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
        }
        return searchedPosts;
    }
    public Post GetOne(int id, int data2)
    {
        List<Post> allPosts = _postData.Get();
        Post post = new();
        foreach (Post item in allPosts)
        {
            if (item.ID == id)
            {
                post = item;
            }
        }
        return post;
    }
    public int? Remove(Post post)
    {
        return _postData.Delete(post);
    }
    public int? Update(Post post)
    {
        throw new NotImplementedException();
    }

    public List<Post> GetAll(int data)
    {
        List<Post> chosenPosts = new();
        try
        {
            List<Post> allPosts = _postData.Get();
            foreach (Post item in allPosts)
            {
                if (item.UserId == data)
                {
                    chosenPosts.Add(item);
                }
            }
            return chosenPosts;
        }
        catch(NullReferenceException)
        {
            return null;
        }
    }
}