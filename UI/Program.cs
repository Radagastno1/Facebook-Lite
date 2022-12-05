﻿using CORE;
using LOGIC;
using DATABASE; 
namespace UI;
internal class Program
{
    //KOMMENTARER VISAS SOM POSTS PÅ SIN MYPAGE!
    private static void Main(string[] args)
    {
        UserManager userManager = new(new UsersDB(), new UsersDB());
        PostsManager postsManager = new(new PostsDB());
        CommentsManager commentsManager = new(new CommentsDB());
        ConversationManager conversationManager = new(new ConversationDB(), new MessagesDB(), new ConversationDB(), new ConversationDB());
        MessgageManager messageManager = new(new MessagesDB(), new MessagesDB());
        SignUpUI signUpUI = new(userManager);
        UserUI userUI = new(userManager, postsManager, conversationManager, conversationManager, messageManager, conversationManager, commentsManager);
        //MENYN INSPIRERAD AV PETRUS BLODBANKEN PROJEKT
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.BackgroundColor = ConsoleColor.White;
        string title = @"   _____ _    ____ _____ ____   ___   ___  _  __  _     ___ _____ _____ 
  |  ___/ \  / ___| ____| __ ) / _ \ / _ \| |/ / | |   |_ _|_   _| ____|
  | |_ / _ \| |   |  _| |  _ \| | | | | | | ' /  | |    | |  | | |  _|  
  |  _/ ___ \ |___| |___| |_) | |_| | |_| | . \  | |___ | |  | | | |___ 
  |_|/_/   \_\____|_____|____/ \___/ \___/|_|\_\ |_____|___| |_| |_____|";

        string[] logInOptions = new string[]
        { "[LOG IN]","[SIGN UP]","[FORGOT PASSWORD?]" };
        int menuOptions = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);
            for (int i = 0; i < logInOptions.Length; i++)
            {
                Console.WriteLine((i == menuOptions ? "\t>>" : "\t") + logInOptions[i]);
            }
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && menuOptions != logInOptions.Length - 1)
            {
                menuOptions++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuOptions >= 1)
            {
                menuOptions--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                switch (menuOptions)
                {
                    case 0:
                        User? user = new();
                        LogInService logInService = new();
                        user = logInService.LogIn();
                        if (user != null)
                        {
                            userUI.ShowFacebookOverview(user);
                        }
                        break;
                    case 1:
                        signUpUI.UserSignUp();
                        //returnerar user för nu, vill att mail ska skickas ut till 
                        //userns mail sedan eventuellt för att validera
                        break;
                    case 2:
                        //FORGOT PASSWORD - EMAIL-SERVICE för utveckling
                        break;
                }
            }
        }
    }
}