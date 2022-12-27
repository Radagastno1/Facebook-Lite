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
    public void ShowMyBlockedUsers(User user)
    {
        try
        {
            List<User> blockedUsers = _blockingsManager.GetMyBlockedUsers(user);
            if(blockedUsers != null && blockedUsers.Count() > 0)
            {
                blockedUsers.ForEach(u => Console.WriteLine($"[{u.ID}] {u.FirstName} {u.LastName}"));
            }
            else throw new InvalidOperationException();
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("No blocked users!");
        }

    }
}