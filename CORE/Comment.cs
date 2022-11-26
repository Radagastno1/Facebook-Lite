namespace CORE;
public class Comment : Post
{
    public int OnPostId{get; private set;}
    public Comment(string aContent, DateOnly aDateCreated, User aUser, int aOnPostId) : base(aContent, aDateCreated, aUser)
    {
        OnPostId = aOnPostId;
    }

}