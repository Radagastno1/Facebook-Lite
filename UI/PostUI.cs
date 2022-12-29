using CORE;
using LOGIC;
namespace UI;
public class PostUI
{
    IManager<Post, User> _postManager;
    IManager<Comment, User> _commentManager;
    public PostUI(IManager<Post, User> postManager, IManager<Comment,User> commentManager)
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
        ShowPostById(postId, user);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[E] Edit  [P] Publish", LogicTool.NewKeyList(ConsoleKey.E, ConsoleKey.P));
        if (pressedKey == ConsoleKey.E)
        {
            EditPost(postId, user);
        }
        else return;
    }
    public void ShowPosts(int userId, User user)
    {
        Console.Clear();
        List<Post> allPosts = new();
        try
        {
            allPosts = _postManager.GetAll(userId, user);
            foreach (Post item in allPosts)
            {
                Console.WriteLine($"{item.ToString()}\n");
            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("\tNo posts yet..");
        }
    }
    public void ShowPostById(int postId, User user)
    {
        Post post = _postManager.GetOne(postId, user);
        Console.WriteLine(post.ToString());
    }
    public void CommentPost(int userId, int postId)
    {
        string content = ConsoleInput.GetString("Leave a comment: ");
        Comment comment = new Comment(content, DateTime.Now, userId, postId);
        _commentManager.Create(comment);
    }
    public ConsoleKey ChooseIfComment()
    {
        List<ConsoleKey> keys = new();
        keys.Add(ConsoleKey.C); keys.Add(ConsoleKey.V);
        ConsoleKey key = ConsoleInput.GetPressedKey("\t[C] Comment   [V] View Comments", keys);
        return key;
    }
    public void ShowCommentsOnPost(int postId, User user)
    {
        try
        {
            List<Comment> comments = _commentManager.GetAll(postId, user);
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
    public void EditPost(int postId, User user)
    {
        Post post = _postManager.GetOne(postId, user);
        post.Content = ConsoleInput.GetString("Edit post: ");
        _postManager.Update(post);
    }

    public void DeletePost(User user)
    {
        int postId = ConsoleInput.GetInt("Post to delete: ");
        Post post = _postManager.GetOne(postId, user);
        ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[D] Delete post  [R] Return", LogicTool.NewKeyList(ConsoleKey.D, ConsoleKey.R));
        if (pressedKey == ConsoleKey.D)
        {
            if (post.UserId == user.ID)
                _postManager.Remove(post);
                Console.WriteLine("Post deleted!");
        }
        else return;
    }
}