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
    public List<Comment> GetAll(int data)
    {
        try
        {
            List<Comment> allComments = _commentsData.Get();
            List<Comment> selectedComments = new();
            foreach (Comment item in allComments)
            {
                if (item.OnPostId == data)
                {
                    selectedComments.Add(item);
                }
            }
            return selectedComments;
        }
        catch(NullReferenceException)
        {
            return null;
        }
    }
    public List<Comment> GetBySearch(string search)
    {
        throw new NotImplementedException();
    }
    public Comment GetOne(int data)
    {
        throw new NotImplementedException();
    }
    public int? Remove(Comment obj)
    {
        throw new NotImplementedException();
    }
    public Comment Update()
    {
        throw new NotImplementedException();
    }
}