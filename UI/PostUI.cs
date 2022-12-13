using CORE;
using LOGIC;
namespace UI;
public class PostUI
{
    IManager<Post> _postManager;
    IManager<Comment> _commentManager;
    public PostUI(IManager<Post> postManager, IManager<Comment> commentManager)
    {
        _postManager = postManager;
        _commentManager = commentManager;
    }
    public int MakePost(User user)
    {
        string content = ConsoleInput.GetString("What's on your mind?");
        Post post = new(content, DateTime.Now, user.ID);
        return _postManager.Create(post).GetValueOrDefault();
    }
    public void PublishPost(User user)
    {
        int postId = MakePost(user);
        ShowPostById(postId);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[E] Edit  [P] Publish", LogicTool.NewKeyList(ConsoleKey.E, ConsoleKey.P));
        if (pressedKey == ConsoleKey.E)
        {
            EditPost(postId);
        }
        else return;
    }
    public int ShowPosts(int userId)
    {
        List<Post> allPosts = new();
        try
        {
            allPosts = _postManager.GetAll(userId);
            foreach (Post item in allPosts)
            {
                Console.WriteLine($"{item.ToString()}\n");
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("\tNo posts yet..");
        }
        int postId = ConsoleInput.GetInt("[0] Return   [ChoosePost] See Post");
        return postId;
    }
    public void ShowPostById(int postId)
    {
        Post post = _postManager.GetOne(postId);
        Console.WriteLine(post.ToString());
    }
    public void CommentPost(User user, int postId)
    {
        string content = ConsoleInput.GetString("Leave a comment: ");
        Comment comment = new Comment(content, DateTime.Now, user.ID, postId);
        _commentManager.Create(comment);
    }
    public ConsoleKey ChooseIfComment()
    {
        List<ConsoleKey> keys = new();
        keys.Add(ConsoleKey.C); keys.Add(ConsoleKey.V);
        ConsoleKey key = ConsoleInput.GetPressedKey("\t[C] Comment   [V] View Comments", keys);
        return key;
    }
    public void ShowCommentsOnPost(int postId)
    {
        try
        {
            List<Comment> comments = _commentManager.GetAll(postId);
            foreach (Comment item in comments)
            {
                Console.WriteLine($"{item.ToString()}\n");
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("No comments yet..");
        }
    }
    public void EditPost(int postId)
    {
        Post post = _postManager.GetOne(postId);
        post.Content = ConsoleInput.GetString("Edit post: ");
        _postManager.Update(post);
    }

    public void DeletePost(User user, int postId)
    {
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[D] Delete post  [R] Return", LogicTool.NewKeyList(ConsoleKey.D, ConsoleKey.R));
        if (pressedKey == ConsoleKey.D)
        {
            Post post = _postManager.GetOne(postId);
            if (post.UserId == user.ID)
                _postManager.Remove(post);
        }
        else return;
    }
}