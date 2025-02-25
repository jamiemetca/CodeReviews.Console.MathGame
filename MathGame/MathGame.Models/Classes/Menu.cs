using MathGame.Models.Enums;
using System.Text;

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

            return Console.ReadLine() ?? "";
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
            Console.Clear();

            Console.WriteLine("Are you ready?");
            Console.WriteLine("Hit any key to start");
            Console.ReadLine();

            var quiz = new Quiz(game.Type, game.Difficulty);

            game.StartGame();
            game.AchievedScore = quiz.AskQuestions();
            game.MaxPossibleScore = quiz.GetMaxScore();
            game.EndGame();

            return game;
        }

        public static void GameSummary(Game game)
        {
            Console.Clear();

            Console.WriteLine("Game summary");
            Console.WriteLine(Summarise(game));
            Console.ReadLine();
        }

        public static bool PlayAgain()
        {
            Console.Clear();

            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("y - Yes");
            Console.WriteLine("n - No");
            var userChoice = Console.ReadLine() ?? "";

            return string.Equals(userChoice.ToLower(), "y");
        }

        public static void DisplayHistory(History history)
        {
            Console.Clear();

            Console.WriteLine("Game History");
            foreach(var game in history.GetGames())
            {
                Console.WriteLine(Summarise(game));
            }
            Console.ReadLine();
        }

        public static string ConfirmQuit()
        {
            Console.Clear();

            Console.WriteLine("Are you sure you want to quit?");
            Console.WriteLine("y - Yes");
            Console.WriteLine("n - No");

            return Console.ReadLine() ?? "";
        }

        public static void RepeatOptions()
        {
            Console.Clear();

            Console.WriteLine("I'm sorry, I didn't understand your choice");
            Console.WriteLine("Please select an option");

            Thread.Sleep(1000);
        }

        private static string Summarise(Game game)
        {
            var summary = new StringBuilder();
            summary.Append($"Player: {game.Username} - ");
            summary.Append($"Difficulty: {game.Difficulty} - ");
            summary.Append($"GameType: {game.Type} - ");
            summary.Append($"Score: {game.AchievedScore}/{game.MaxPossibleScore} - ");
            summary.Append($"Time: {game.GameLength()}");

            return summary.ToString();
        }
    }
}
