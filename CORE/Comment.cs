namespace CORE;
public class Comment : Post
{
    public int OnPostId{get; private set;}
    public Comment(string aContent, DateTime aDateCreated, int aUserId, int aOnPostId) : base(aContent, aDateCreated, aUserId)
    {
        OnPostId = aOnPostId;
    }

}