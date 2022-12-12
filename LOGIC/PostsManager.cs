using CORE;
namespace LOGIC;
public class PostsManager : IManager<Post>
{
    IData<Post> _postData;
    IIdData<Post> _postIdData;
    public PostsManager(IData<Post> postData, IIdData<Post> postIdData)
    {
        _postData = postData;
        _postIdData = postIdData;
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
            List<Post> allPosts = _postData.GetAll();
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
    public Post GetOne(int postId)  //ska lösas via sql
    {
        // List<Post> allPosts 
         Post post = _postIdData.GetIds(postId);
        // foreach (Post item in allPosts)
        // {
        //     if (item.ID == id)
        //     {
        //         post = item;
        //     }
        // }
        return post;
    }
    public int? Remove(Post post)   //man ska kunna radera sin post, alltså sätta till ej synlig
    {
        return _postData.Delete(post);
    }
    public int? Update(Post post)   //redigera sin post och lägg till is_edited i table
    {
        return _postData.Update(post);
    }

    public List<Post> GetAll(int userId)
    {
        try
        {
            List<Post> allPosts = _postData.GetById(userId);
            return allPosts;
        }
        catch (NullReferenceException)
        {
            return null;
        }
    }
}