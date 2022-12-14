using CORE;
using LOGIC;
namespace UI;
public class BlockingsUI
{
    IRelationsManager<User> _blockingsManager;
    public Action<User> OnBlockUser;
    public BlockingsUI(IRelationsManager<User> blockingsManager)
    {
        _blockingsManager = blockingsManager;
    }
    public void BlockUser(User user, int friendId)
    {
        ConsoleKey answerKey = ConsoleInput.GetPressedKey("Are you sure you want to block this user? Friendships will be deleted. Y/N", LogicTool.NewKeyList(ConsoleKey.Y, ConsoleKey.N));
        if (answerKey == ConsoleKey.Y)
        {
            if (_blockingsManager.Create(user, friendId) > 0)
            {
                OnBlockUser?.Invoke(user);
                Console.WriteLine("User blocked.");
            }
            else
            {
                Console.WriteLine("Something went wrong.");
            }
        }
        else return;
    }
    public void ShowMyBlockedUsers(User user)
    {
        try
        {
            List<User> blockedUsers = _blockingsManager.GetMine(user);
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