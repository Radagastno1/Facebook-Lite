using CORE;
using LOGIC;

public class CommentsManager : IManager<Comment, User>
{
    IData<Comment> _commentsData;
    IDataToList<Comment, User> _commentsList;
    public CommentsManager(IData<Comment> commentsData,  IDataToList<Comment, User> commentsList)
    {
        _commentsData = commentsData;
        _commentsList = commentsList;
    }
    public int? Create(Comment obj)
    {
        return _commentsData.Create(obj);
    }
    public List<Comment> GetAll(int postId, User user)
    {
        try
        {
            List<Comment> allComments = _commentsList.GetById(postId, user);
            return allComments;
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }
    public List<Comment> GetBySearch(string name, User user)
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