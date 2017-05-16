using GalgelegTUI.Controllers;
using GalgelegTUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalgelegTUI
{
    public class Program
    {
        private static readonly GameController GameController = new GameController();
        
        public static void Main(string[] args)
        {
            User UserLoggedIn = new User();
            do
            {
                Console.WriteLine("Please enter your username");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter your password");
                string password = Console.ReadLine();

                Login login = new Login();

                try
                {
                    UserLoggedIn = login.UserLogin(username, password).Result;
                    GameController.SetUserLoggedIn(UserLoggedIn);
                } catch(Exception e)
                {
                    Console.WriteLine("Wrong username or password. Please try again.");
                }
                
            } while (UserLoggedIn.Token == null);

            GameController.RunGame();

        }
    }
}
