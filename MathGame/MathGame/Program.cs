// See https://aka.ms/new-console-template for more information
string user = Menu.WelcomeUser();
var history = new History();

while(true)
{
    var mainMenuOption = Menu.MainMenu();

    if (mainMenuOption.ToLower() == "s")
    {
        var game = Menu.GameSetup(user);
        var gameResult = Menu.PlayGame(game);

        // This will accept the game and will parse the propeties into a string
        Menu.GameSummary("winner");
        continue;
    }
    else if (mainMenuOption.ToLower() == "h")
    {
        Menu.DisplayHistory();
        continue;
    }
    else if (mainMenuOption.ToLower() == "q")
    {
        var response = Menu.ConfirmQuit();

        if (response.ToLower() == "y")
        {
            Environment.Exit(0);
        }

        continue;
    }
    else
    {
        Menu.Sorry();
    }
}

public static class Menu
{
    public static string WelcomeUser()
    {
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

        var gameType = Utilities.GetUserSelection<GameType>("Choose game type?");
        var difficulty = Utilities.GetUserSelection<Difficulty>("Choose game difficulty?");

        game.Type = gameType;
        game.Difficulty = difficulty;

        return game;
    }

    public static Game PlayGame(Game game)
    {
        Console.Clear();

        // User to confirm ready
        Console.WriteLine("Are you ready?");
        Console.WriteLine("Hit any key to start");
        Console.ReadLine();

        // start game
        game.StartGame();

        Console.WriteLine("Playing game");
        var quiz = new Quiz(game.Type, game.Difficulty);
        // write logic for questions
        // division
        // The result must be a whole number
        // the dividend must be greater than or equal to the divider
        // increment the dividend until it's a multiple of the dividend
        // but must still be less than the max
        Console.ReadLine();

        // end game
        game.EndGame();
        Console.ReadLine();

        return game;
    }

    public static void GameSummary(string details)
    {
        Console.Clear();

        Console.WriteLine(details);
        Console.ReadLine();
    }

    public static void DisplayHistory()
    {
        Console.Clear();

        Console.WriteLine("You played some games");
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

public class Game
{
    public Game(string username)
    {
        Username = username;
    }

    public void StartGame()
    {
        StartTime = DateTime.Now;
        Console.WriteLine($"Started at {StartTime}");
    }

    public void EndGame()
    {
        EndTime = DateTime.Now;
        Console.WriteLine($"Ended at {EndTime}");
    }

    private string Username { get; set; }
    private DateTime StartTime { get; set; }
    private DateTime EndTime { get; set; }
    private int AvailableScore { get; set; }
    private int AchievedScore { get; set; }
    public Difficulty Difficulty { get; set; }
    public GameType Type { get; set; }
}

public enum GameType
{
    // Create game types, Addition, Subtraction
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Mixed
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class History
{
    private List<Game> Games { get; set; } = new List<Game>();
}

public static class Utilities
{
    public static T GetUserSelection<T>(string displayText) where T : struct
    {
        Console.Clear();
        
        while (true)
        {
            Console.WriteLine(displayText);

            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();

            // Display options to the user
            var i = 0;
            foreach (T value in enumValues)
            {
                Console.WriteLine($"{i++} - {value}");
            }

            string userSelection = Console.ReadLine();

            // Attempt to parse user input
            var isOptionParsable = Enum.TryParse<T>(userSelection, out T result);
            var isOptionDefined = Enum.IsDefined(typeof(T), result);
            if (isOptionParsable && isOptionDefined)
            {
                return result;
            }

            // Handle invalid input
            Console.Clear();
            Console.WriteLine($"{result} is an invalid option. Please enter a valid option.");
        }
    }
}

public class Quiz
{
    public Quiz(GameType gameType, Difficulty difficulty)
    {
        var minNumber = 0;
        var maxNumber = 10;
        var numberOfQuestions = 5;

        if (difficulty == Difficulty.Medium)
        {
            maxNumber = 100;
            numberOfQuestions = 10;
        }
        else if (difficulty == Difficulty.Hard)
        {
            maxNumber = 1000;
            numberOfQuestions = 15;
        }

        for(var i = 0; i < numberOfQuestions; i++)
        {
            Random random = new Random();
            decimal firstNumber = random.Next(minNumber, maxNumber + 1);
            decimal secondNumber = random.Next(minNumber, maxNumber + 1);

            Console.WriteLine($"{firstNumber} {secondNumber} ({gameType})");
            var ans = firstNumber / secondNumber;
            Console.WriteLine($"Ans: {ans}. Is whole number: {ans % 1}");

            var question = new QuizQuestion((int) firstNumber, (int) secondNumber, gameType);
        }

    }

    private GameType gameType { get; set; }
    private Difficulty Difficulty { get; set; }
    private List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();

}

public class QuizQuestion
{
    public QuizQuestion(int firstNumber, int secondNumber, GameType gameType)
    {
        FirstNumber = firstNumber;
        SecondNumber = secondNumber;
        GameType = gameType;
    }
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public GameType GameType { get; set; }
}
