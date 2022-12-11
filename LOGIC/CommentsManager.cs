using CORE;
using LOGIC;

public class CommentsManager : IManager<Comment>
{
    IData<Comment> _commentsData;
    public CommentsManager(IData<Comment> commentsData)
    {
        _commentsData = commentsData;
    }
    public int? Create(Comment obj)
    {
        return _commentsData.Create(obj);
    }
    public List<Comment> GetAll(int postId)
    {
        try
        {
            List<Comment> allComments = _commentsData.GetById(postId);
            return allComments;
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
    public List<Comment> GetBySearch(string name)
    {
        throw new NotImplementedException();  //sök efter kommentarer i en post via namn?
    }
    public Comment GetOne(int data)
    {
        throw new NotImplementedException();  //hämta specifik kommentar
    }
    public int? Remove(Comment obj)
    {
        throw new NotImplementedException();  //måste kunna radera sin egen kommentar ELLER om den är på sitt inlägg
    }
    public int? Update(Comment comment)
    {
        throw new NotImplementedException();  //redigera sin egna kommentar
    }
}