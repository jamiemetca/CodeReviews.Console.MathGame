namespace MathGame.Models.Classes
{
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
}
