// See https://aka.ms/new-console-template for more information
string user = Menu.WelcomeUser();
var history = new History();

while(true)
{
    var mainMenuOption = Menu.MainMenu();

    if (mainMenuOption.ToLower() == "s")
    {
        var game = Menu.GameSetup(user);
        Menu.PlayGame(game);
        Menu.GameSummary(game);
        history.AddGame(game);

        continue;
    }
    else if (mainMenuOption.ToLower() == "h")
    {
        Menu.DisplayHistory(history);
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
        // TODO: limit to 3 significant figures (milliseconds)
        Console.WriteLine($"Player: {details.Username} - Difficulty: {details.Difficulty} - GameType: {details.Type} - Score: {details.AchievedScore}/{details.MaxPossibleScore} - Time: {details.GameLength()}");
        Console.ReadLine();
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

public class Game
{
    public Game(string username)
    {
        Username = username;
    }

    public void StartGame()
    {
        StartTime = DateTime.Now;
    }

    public void EndGame()
    {
        EndTime = DateTime.Now;
    }

    public TimeSpan GameLength()
    {
        return EndTime - StartTime;
    }

    public string Username { get; set; }
    private DateTime StartTime { get; set; }
    private DateTime EndTime { get; set; }
    public int MaxPossibleScore { get; set; }
    public int AchievedScore { get; set; }
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
    public void AddGame(Game game)
    {
        Games.Add(game);
    }

    public List<Game> GetGames()
    {
        return Games;
    }

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
        int minNumber = 1;
        int maxNumber = 10;
        int numberOfQuestions = 5;
        GameType questionType = gameType;

        if (difficulty == Difficulty.Medium)
        {
            maxNumber = 20;
            numberOfQuestions = 10;
        }
        else if (difficulty == Difficulty.Hard)
        {
            maxNumber = 100;
            numberOfQuestions = 15;
        }

        for(var i = 0; i < numberOfQuestions; i++)
        {
            Random random = new Random();
            int firstNumber = random.Next(minNumber, maxNumber + 1);
            int secondNumber = random.Next(minNumber, maxNumber + 1);

            if (gameType == GameType.Mixed)
            {
                questionType = GetRandomOperator();
            }

            if (questionType == GameType.Division)
            {
                secondNumber = CalculateDivisor(firstNumber);
            }

            Questions.Add(new QuizQuestion((int) firstNumber, (int) secondNumber, questionType));
        }
    }

    public int AskQuestions()
    {
        var score = 0;
        foreach(var question in Questions)
        {
            Console.WriteLine($"What is {question.FirstNumber} {ConvertToOperator(question.QuestionType)} {question.SecondNumber}");
            int.TryParse(Console.ReadLine(), out int input);

            var answer = 0;
            if (question.QuestionType == GameType.Addition)
            {
                answer = question.FirstNumber + question.SecondNumber;
            }
            else if (question.QuestionType == GameType.Subtraction)
            {
                answer = question.FirstNumber - question.SecondNumber;
            }
            if (question.QuestionType == GameType.Division)
            {
                answer = question.FirstNumber / question.SecondNumber;
            }
            else if (question.QuestionType == GameType.Multiplication)
            {
                answer = question.FirstNumber * question.SecondNumber;
            }

            if (input == answer)
            {
                Console.WriteLine("Correct");
                score++;
                continue;
            }

            Console.WriteLine("Incorrect");
        }

        return score;
    }

    public int GetMaxScore()
    {
        return Questions.Count();
    }

    private static int CalculateDivisor(int dividend)
    {
        Random random = new Random();
        var secondNumber = random.Next(1, dividend);
        
        for(decimal j = secondNumber; j > 0; j--)
        {
            if (dividend % j == 0)
            {
                secondNumber = Convert.ToInt32(j);
                break;
            }
        }

        return 1;
    }

    private static GameType GetRandomOperator()
    {
        Random random = new Random();
        // Don't include mixed in options
        var numberOfOptions = Enum.GetNames(typeof(GameType)).Length - 1;
        return (GameType) random.Next(0, numberOfOptions);
    }

    private static char ConvertToOperator(GameType type)
    {
        switch(type)
        {
            case GameType.Division:
                return '/';
            case GameType.Multiplication:
                return '*';
            case GameType.Subtraction:
                return '-';
            default:
                return '+';
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
        QuestionType = gameType;
    }
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public GameType QuestionType { get; set; }
}
