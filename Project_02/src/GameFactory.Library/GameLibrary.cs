using System.Collections.ObjectModel;
using System.Linq;
using GameFactory.SDK;

namespace GameFactory.Library
{
    using Utilities;

public class GameLibrary
{
    public ObservableCollection<IGame> Games { get; private set; }

    public GameLibrary()
    {
        Games = Utilities.LoadGames();
    }

    public void PlayGame(string selectedGame)
    {
        var gameToPlay = Games.FirstOrDefault(game => selectedGame == game.Title);

        if (gameToPlay == null) return;
        gameToPlay.Play();
    }
}
}
