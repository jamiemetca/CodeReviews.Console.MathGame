using MathGame.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame.Models.Classes
{
    public static class Menu
    {
        public static string WelcomeUser()
        {
            Console.Clear();

            Console.WriteLine("Welcome to Math Game");
            Console.WriteLine("What should I call you?");

            var username = Console.ReadLine();

            return string.IsNullOrEmpty(username) ? "User" : username;
        }

        public static string MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Main menu");
            Console.WriteLine("s - Start Game");
            Console.WriteLine("h - Game History");
            Console.WriteLine("q - Quit");

            return Console.ReadLine();
        }

        public static Game GameSetup(string username)
        {
            var game = new Game(username);

            game.Type = Utilities.GetUserSelection<GameType>("Choose game type?");
            game.Difficulty = Utilities.GetUserSelection<Difficulty>("Choose game difficulty?");

            return game;
        }

        public static Game PlayGame(Game game)
        {
            var quiz = new Quiz(game.Type, game.Difficulty);

            Console.Clear();

            Console.WriteLine("Are you ready?");
            Console.WriteLine("Hit any key to start");
            Console.ReadLine();

            game.StartGame();
            game.AchievedScore = quiz.AskQuestions();
            game.MaxPossibleScore = quiz.GetMaxScore();
            game.EndGame();

            return game;
        }

        public static void GameSummary(Game details)
        {
            Console.Clear();

            Console.WriteLine("Game summary");
            Console.WriteLine($"Player: {details.Username} - Difficulty: {details.Difficulty} - GameType: {details.Type} - Score: {details.AchievedScore}/{details.MaxPossibleScore} - Time: {details.GameLength()}");
            Console.ReadLine();
        }

        public static bool PlayAgain()
        {
            Console.Clear();

            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("y - Yes");
            Console.WriteLine("n - No");
            var playAgain = Console.ReadLine();

            return playAgain.ToLower() == "y";
        }

        public static void DisplayHistory(History history)
        {
            Console.Clear();

            Console.WriteLine("Game History");
            foreach(var game in history.GetGames())
            {
                Console.WriteLine($"Player: {game.Username} - Difficulty: {game.Difficulty} - GameType: {game.Type} - Score: {game.AchievedScore}/{game.MaxPossibleScore} - Time: {game.GameLength()}");
            }
            Console.ReadLine();
        }

        public static string ConfirmQuit()
        {
            Console.Clear();

            Console.WriteLine("Are you sure you want to quit?");
            Console.WriteLine("y - Yes");
            Console.WriteLine("n - No");

            return Console.ReadLine();
        }

        public static void Sorry()
        {
            Console.Clear();

            Console.WriteLine("I'm sorry, I didn't understand your choice");
            Console.WriteLine("Please select an option");

            Thread.Sleep(1000);
        }
    }
}
