using CORE;
namespace LOGIC;
public class PostsManager : IManager<Post, User>
{
    IData<Post> _postData;
    IIdData<Post> _postIdData;
    IDataToList<Post, User> _postDataToList;
    public PostsManager(IData<Post> postData, IIdData<Post> postIdData, IDataToList<Post, User> postDataToList)
    {
        _postData = postData;
        _postIdData = postIdData;
        _postDataToList = postDataToList;
    }
    public int? Create(Post post)
    {
        return _postData.Create(post);
    }
    public List<Post> GetBySearch(string search, User user)
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
         Post post = _postIdData.GetIds(postId);
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

    public List<Post> GetAll(int userId, User user)
    {
        try
        {
            List<Post> allPosts = _postDataToList.GetById(userId, user);
            return allPosts;
        }
        catch (NullReferenceException)
        {
            return null;
        }
    }
}