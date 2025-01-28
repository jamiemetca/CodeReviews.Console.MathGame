using MathGame.Models.Enums;

namespace MathGame.Models.Classes
{
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

                var answer = question.FirstNumber + question.SecondNumber;

                if (question.QuestionType == GameType.Subtraction)
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
}
