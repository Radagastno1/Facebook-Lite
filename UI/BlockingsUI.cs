using CORE;
using LOGIC;
namespace UI;
public class BlockingsUI
{
    IBlockingsManager<User> _blockingsManager;
    public BlockingsUI(IBlockingsManager<User> blockingsManager)
    {
        _blockingsManager = blockingsManager;
    }
    public void BlockUser(User user, int friendId)
    {
        string answer = ConsoleInput.GetString("Are you sure you want to block this user? Friendships will be deleted. Y/N");
        if (answer.ToLower() == "y")
        {
            if (_blockingsManager.BlockUser(user, friendId) > 0)
                Console.WriteLine("User blocked.");
        }
        else return;
    }
    public void ShowMyBlockedUsers(User user)
    {
        try
        {
            List<User> blockedUsers = _blockingsManager.GetMyBlockedUsers(user);
            if (blockedUsers == null || blockedUsers.Count() < 1)
            {
                throw new InvalidOperationException();
            }
            else
            {
                blockedUsers.ForEach(u => Console.WriteLine($"[{u.ID}] {u.FirstName} {u.LastName}"));
            }
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("No blocked users!");
        }

    }

}