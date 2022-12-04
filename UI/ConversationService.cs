// using LOGIC;
// using CORE;
// namespace UI;
// public class ConversationService
// {
//     IManager<Conversation> _conversationManager;
//     IManager<Message> _messageManager;
//     IIdManager<Conversation> _idManager;
//     public ConversationService(IManager<Conversation> conversationManager, IManager<Message> messageManager, IIdManager<Conversation> idManager)
//     {
//         _conversationManager = conversationManager;
//         _messageManager = messageManager;
//         _idManager = idManager;
//     }
   
//     public void ShowConversations(List<Conversation>conversations)
//     {
//         foreach (Conversation item in conversations)
//         {
//             Console.WriteLine($"Conversation [{item.ID}]");
//         }
//     }
//     public ConversationResult ConversationsExists(List<int> ids, User user)
//     {
//         ConversationResult result = new();
//         List<int> participantIds = new();
//         participantIds.Add(user.ID);
//         foreach (int id in ids)
//         {
//             participantIds.Add(id);
//         }
//         List<Conversation> foundConversations = new();
//         foundConversations = _idManager.GetIds(participantIds);
//         if(foundConversations.Count > 0)
//         {
//             result.conversations = foundConversations;
//             result.ConversationExists = true;
//         }
//         else
//         {
//             result.ConversationExists = false;
//         }
//         return result;
//     }
// }