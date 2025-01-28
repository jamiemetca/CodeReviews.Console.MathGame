using MathGame.Models.Enums;

namespace MathGame.Models.Classes
{
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
}
