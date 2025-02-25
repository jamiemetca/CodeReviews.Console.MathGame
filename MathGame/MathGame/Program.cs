using MathGame.Models.Classes;

string username = Menu.WelcomeUser();
var history = new History();

while(true)
{
    var mainMenuOption = Menu.MainMenu();

    if (string.Equals(mainMenuOption.ToLower(), "s"))
    {
        while(true)
        {
            var game = Menu.GameSetup(username);
            Menu.PlayGame(game);
            Menu.GameSummary(game);
            history.AddGame(game);

            if (!Menu.PlayAgain())
            {
                break;
            }
        }

        continue;
    }
    else if (string.Equals(mainMenuOption.ToLower(), "h"))
    {
        Menu.DisplayHistory(history);
        continue;
    }
    else if (string.Equals(mainMenuOption.ToLower(), "q"))
    {
        var response = Menu.ConfirmQuit();

        if (string.Equals(response.ToLower(), "y"))
        {
            Environment.Exit(0);
        }

        continue;
    }
    else
    {
        Menu.RepeatOptions();
    }
}

