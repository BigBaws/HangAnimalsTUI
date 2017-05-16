using GalgelegTUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GalgelegTUI.Controllers
{

    public class GameController
    {
        private User UserLoggedIn;
        private RESTConnector Connector;
        private SingleplayerModel Singleplayer;
        private bool GameOver = false;
        private int Wrongs;
        private Guess Guess = null;
        private bool GameWon;
        private char PlayAgain;

        public GameController()
        {
            Connector = new RESTConnector();
            GameWon = false;
        }

        public async Task<SingleplayerModel> CreateGame()
        {
            var url =/* Url + */"singleplayer/create"; /*?token=" + UserLoggedIn.Token + "&userid=" + UserLoggedIn.StudentNumber; */

            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                { "token", UserLoggedIn.Token },
                { "userid", UserLoggedIn.Userid },
                { "gameid", "0" }
            };
            var singleplayerGame = await Connector.RestPUT<SingleplayerModel>(url, values);

            Singleplayer = new SingleplayerModel
            {
                GameID = singleplayerGame.GameID,
                Combo_active = singleplayerGame.Combo_active,
                Combo = singleplayerGame.Combo,
                Usedletters = singleplayerGame.Usedletters,
                Userid = singleplayerGame.Userid,
                Word = singleplayerGame.Word,
                GameScore = singleplayerGame.GameScore
            };

            return Singleplayer;
        }

        public void Leave(Dictionary<string, string> values)
        {
            var url = "singleplayer/leave";

            Connector.LeaveRoom(url, values);

        }

        public async Task<Guess> GuessLetter(Dictionary<string, string> values)
        {

            var url = "singleplayer/guess";

             var tmp = await Connector.RestPOST<Guess>(url, values);

            var guess = new Guess()
            {
                Score = tmp.Score,
                Used = tmp.Used,
                Word = tmp.Word,
                Wrongs = tmp.Wrongs
            };

            return guess;
            
        }

        public void RunGame()
        {
            do
            {
                do
                {
                    Singleplayer = CreateGame().Result;
                    if(Singleplayer.Word.Equals(""))
                    {
                        Dictionary<string, string> values = new Dictionary<string, string>()
                        {
                            { "token", UserLoggedIn.Token },
                            { "userid", UserLoggedIn.Userid },
                            { "gameid", Singleplayer.GameID }
                        };
                        Leave(values);
                    }
                } while (Singleplayer.Word.Equals(""));

                Wrongs = 0;

                Console.Clear();
                PrintGallow(Wrongs);

                do
                {
                    Console.WriteLine("Guess a letter");
                    char key = Console.ReadKey().KeyChar;

                    if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122))
                    {
                        Dictionary<string, string> values = new Dictionary<string, string>()
                    {
                        { "token", UserLoggedIn.Token },
                        { "userid", UserLoggedIn.Userid },
                        { "gameid", Singleplayer.GameID },
                        { "letter", key.ToString() }
                    };

                        Guess = GuessLetter(values).Result;
                        Wrongs = Guess.Wrongs;

                        Console.Clear();
                        PrintGallow(Wrongs);

                        if (Guess.Wrongs >= 6 || !Guess.Word.Contains("*"))
                        {
                            if (!Guess.Word.Contains("*"))
                            {
                                GameWon = true;
                            }
                            else
                            {
                                GameWon = false;
                            }
                            GameOver = true;
                        }
                        else
                        {

                            GameOver = false;
                        }
                    }
                } while (!GameOver);

                if (GameWon)
                {
                    Console.WriteLine("\nYou won!");
                }
                else
                {
                    Console.WriteLine("\nYou lost!");
                }

                Dictionary<string, string> leaveValues = new Dictionary<string, string>()
                {
                    { "token", UserLoggedIn.Token },
                    { "userid", Singleplayer.Userid },
                    { "gameid", Singleplayer.GameID }
                };
                Leave(leaveValues);
                Guess = null;
                Console.WriteLine("Do you want to play again? [Y/N]");
                PlayAgain = Console.ReadKey().KeyChar;
            } while (PlayAgain == 'Y' || PlayAgain == 'y');

            Console.WriteLine("\nSee you later!");
            Console.WriteLine("Press any key to close the application...");
            Console.Read();    

        }


        private void PrintGallow(int wrongs)
        {
            switch (wrongs)
            {
                case 0:
                    Console.WriteLine("____________");
                    Console.WriteLine("|          | ");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    Console.WriteLine("");
                    if (Guess == null)
                    {
                        Console.WriteLine(Singleplayer.Word);
                        Console.WriteLine("");
                        Console.WriteLine("Used letters: ");
                    } else
                    {
                        Console.WriteLine(Guess.Word);
                        Console.WriteLine("");
                        Console.WriteLine("Used letters: " + Guess.PrintUsed());
                    }
                    break;
                case 1:
                    Console.WriteLine("____________ ");
                    Console.WriteLine("|          | ");
                    Console.WriteLine("|          O ");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
                case 2:
                    Console.WriteLine("____________ ");
                    Console.WriteLine("|          |");
                    Console.WriteLine("|          O");
                    Console.WriteLine("|          |");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
                case 3:
                    Console.WriteLine("____________ ");
                    Console.WriteLine("|          | ");
                    Console.WriteLine("|          O ");
                    Console.WriteLine("|         /| ");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
                case 4:
                    Console.WriteLine("____________");
                    Console.WriteLine("|          |");
                    Console.WriteLine("|          O");
                    Console.WriteLine("|         /|\\");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
                case 5:
                    Console.WriteLine("____________ ");
                    Console.WriteLine("|          |");
                    Console.WriteLine("|          O");
                    Console.WriteLine("|         /|\\");
                    Console.WriteLine("|         /");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
                case 6:
                    Console.WriteLine("____________ ");
                    Console.WriteLine("|          |");
                    Console.WriteLine("|          O");
                    Console.WriteLine("|         /|\\");
                    Console.WriteLine("|         / \\");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
                case 7:
                    Console.WriteLine("_____________");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|_______");
                    break;
            }
            if (wrongs > 0)
            {
                Console.WriteLine("");
                Console.WriteLine(Guess.Word);
                Console.WriteLine("");
                Console.WriteLine("Used letters: " + Guess.PrintUsed());
            }
        }

        public void SetUserLoggedIn(User user)
        {
            UserLoggedIn = user;
        }

    }
}
