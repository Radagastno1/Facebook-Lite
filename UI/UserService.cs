// using CORE;
// using LOGIC;
// namespace UI;
// public class UserService
// {
//     IManager<User> _userManager;
//     IManager<Post> _postManager;
//     IManager<Comment> _commentsManager;
//     IManager<Conversation> _conversationManager;
//     IManager<Message> _messageManager;
//     IIdManager<Conversation> _idManager;
//     PostService postService;
//     MessageService messageService;
//     ConversationService conversationService;
//     public UserService(IManager<User> userManager, IManager<Post> postManager, IManager<Comment> commentsManager, IManager<Conversation> conversationManager, IManager<Message> messageManager, IIdManager<Conversation> idManager)
//     {
//         _userManager = userManager;
//         _postManager = postManager;
//         _commentsManager = commentsManager;
//         _conversationManager = conversationManager;
//         _messageManager = messageManager;
//         _idManager = idManager;
//     }
//     public void ShowUserOverView(User user)
//     {
//         string[] overviewOptions = new string[]
//         { "[PUBLISH]","[SEARCH]","[CHAT]", "[MY PAGE]","[SETTINGS]" };
//         int menuOptions = 0;
//         while (true)
//         {
//             Console.Clear();
//             for (int i = 0; i < overviewOptions.Length; i++)
//             {
//                 Console.WriteLine((i == menuOptions ? "\t>>" : "\t") + overviewOptions[i]);
//             }
//             ConsoleKeyInfo keyPressed = Console.ReadKey();

//             if (keyPressed.Key == ConsoleKey.DownArrow && menuOptions != overviewOptions.Length - 1)
//             {
//                 menuOptions++;
//             }
//             else if (keyPressed.Key == ConsoleKey.UpArrow && menuOptions >= 1)
//             {
//                 menuOptions--;
//             }
//             else if (keyPressed.Key == ConsoleKey.Enter)
//             {
//                 switch (menuOptions)
//                 {
//                     case 0:
//                         postService = new(_postManager, _commentsManager);
//                         postService.MakePost(user);
//                         break;
//                     case 1:
//                         int conversationId = 0;
//                         postService = new(_postManager, _commentsManager);
//                         conversationService = new(_conversationManager, _messageManager, _idManager);

//                         string search = ConsoleInput.GetString("Search by name: ");
//                         ShowSearches(search);
//                         int id = ConsoleInput.GetInt("User to visit: ");
//                         ShowProfile(id);

//                         List<ConsoleKey> keys = new();
//                         keys.Add(ConsoleKey.M); keys.Add(ConsoleKey.P);
//                         ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[M] Message  [P] Posts", keys);
//                         if (pressedKey == ConsoleKey.M)
//                         {
//                             messageService = new(_messageManager);
//                             List<int> ids = new();
//                             ids.Add(id);
//                             bool conversationExists = conversationService.ConversationsExists(ids, user).ConversationExists;
//                             if (conversationExists == true)
//                             {
//                                 conversationService.ShowConversations(conversationService.ConversationsExists(ids, user).conversations);
//                                 conversationId = ConsoleInput.GetInt("Choose: ");
//                                 messageService.ShowMessages(conversationId);
//                             }
//                             else
//                             {
//                                 keys = new();
//                                 keys.Add(ConsoleKey.S); keys.Add(ConsoleKey.R);
//                                 pressedKey = ConsoleInput.GetPressedKey("[S]Start conversation  [R] Return", keys);
//                                 if (pressedKey == ConsoleKey.S)
//                                 {
//                                     ids = new();
//                                     ids.Add(id);
//                                     List<User> participants = GetUsersById(ids);
//                                     conversationId = StartNewConversation(participants, user);
//                                 }
//                                 else
//                                 {
//                                     break;
//                                 }
//                             }
//                             messageService.MakeMessage(user, conversationId);
//                         }
//                         else
//                         {
//                             int postId = postService.ShowPosts(id);
//                             if (postId != 0)
//                             {
//                                 ConsoleKey key = postService.ChooseIfComment();
//                                 if (key == ConsoleKey.C)
//                                 {
//                                     postService.CommentPost(user, postId);
//                                 }
//                                 else if (key == ConsoleKey.V)
//                                 {
//                                     postService.ShowCommentsOnPost(postId);
//                                 }
//                             }
//                         }
//                         Console.ReadKey();
//                         break;
//                     case 2:
//                         //CHATPAGE
//                         break;
//                     case 3:
//                         postService = new(_postManager, _commentsManager);
//                         ShowProfile(user.ID);
//                         postService.ShowPosts(user.ID);
//                         Console.ReadKey();
//                         break;
//                     case 4:
//                         //SETTINGSMENU
//                         EditInformation(user);
//                         user = _userManager.GetOne(user.ID, 0);
//                         break;
//                 }
//             }
//         }
//     }
   
//     public void ShowSearches(string search)
//     {
//         List<User> users = _userManager.GetBySearch(search);
//         if (users != null)
//         {
//             foreach (User item in users)
//             {
//                 Console.WriteLine(item.ToString());
//             }
//         }
//     }
    
//     public int StartNewConversation(List<User> participants, User user)
//     {
//         int conversationId = conversationService.StartConversation(user, participants).GetValueOrDefault();
//         //VISA KONVERSATIONEN SEDAN HÄR
//         return conversationId;
//     }
//     public List<User> GetUsersById(List<int> ids)
//     {
//         List<User> participants = new();
//         foreach (int id in ids)
//         {
//             User participant = _userManager.GetOne(id, 0);
//             participants.Add(participant);
//         }
//         return participants;
//     }
//     public void EditInformation(User user)
//     {
//         List<ConsoleKey> keys = new();
//         keys.Add(ConsoleKey.D1); keys.Add(ConsoleKey.D2); keys.Add(ConsoleKey.D3);
//         keys.Add(ConsoleKey.D4); keys.Add(ConsoleKey.D5);
//         ConsoleKey pressedKey = ConsoleInput.GetPressedKey("[1] First name  [2] Last name  [3] Email  [4] Password  [5] About me", keys);
//         switch (pressedKey)
//         {
//             case ConsoleKey.D1:
//                 user.FirstName = ConsoleInput.GetString("New first name: ");
//                 break;
//             case ConsoleKey.D2:
//                 user.LastName = ConsoleInput.GetString("New last name: ");
//                 break;
//             case ConsoleKey.D3:
//                 user.Email = ConsoleInput.GetEmail("New Email: ");
//                 break;
//             case ConsoleKey.D4:
//                 user.PassWord = ConsoleInput.GetPassword("New password: ");
//                 break;
//             case ConsoleKey.D5:
//                 user.AboutMe = ConsoleInput.GetString("About me: ");
//                 break;
//         }
//         if (_userManager.Update(user) > 0)
//         {
//             Console.WriteLine("Updated!");
//         }
//         else
//         {
//             Console.WriteLine("Something went wrong.");
//         }
//     }
// }