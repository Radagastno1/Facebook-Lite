using CORE;
namespace LOGIC;
public class LogicTool
{

    // public static List<Conversation> ConversationsPerID(List<Conversation> conversations)
    // {
    //      List<Conversation>conversationsPerId = new();
    //     List<Conversation>allConversations = conversations;
    //     foreach(Conversation item in allConversations)
    //     {
    //         int thisId = item.ID;
    //         foreach(Conversation conversation in allConversations)
    //         {
    //             if(conversation.ID == thisId)
    //             {
    //                 conversationsPerId.Add(conversation);
    //             }
    //             else
    //             {
    //                 break;
    //             }
    //         }
    //     }
    //     return conversationsPerId;
    // }
     public static List<ConsoleKey> NewKeyList(ConsoleKey key1, ConsoleKey key2)
    {
        List<ConsoleKey>keys = new();
        keys.Add(key1);
        keys.Add(key2);
        return keys;
    }
}