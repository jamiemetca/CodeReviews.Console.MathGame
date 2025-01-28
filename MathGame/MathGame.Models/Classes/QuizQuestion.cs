using MathGame.Models.Enums;

namespace MathGame.Models.Classes
{
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
}
