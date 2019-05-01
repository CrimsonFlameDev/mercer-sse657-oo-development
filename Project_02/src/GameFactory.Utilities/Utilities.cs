using System.Collections.ObjectModel;
using System.Windows.Forms;
using GameFactory.SDK;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace GameFactory.Utilities
{
public class Utilities
{
    private static IUnityContainer _container;
    public static ObservableCollection<IGame> LoadGames()
    {
        return new ObservableCollection<IGame>()
        {
            new MockGame("Tic-Tac-Toe"),
            new MockGame("UNO"),
            new MockGame("Black Jack"),
            new MockGame("Monopoly"),
            new MockGame("Checkers"),
            new MockGame("Battleship"),
        };

        // Use Inversion of Control to dynamically load games from configuration file.
        _container = _container ?? new UnityContainer().LoadConfiguration();
        var games = _container.ResolveAll<IGame>();
        return new ObservableCollection<IGame>(games);
    }
}

internal class MockGame : IGame
{
    public string Title { get; private set; }

    private MockGame()
    {
    }

    internal MockGame(string title)
    {
        Title = title;
    }

    public void Play()
    {
        var msg = string.Format("Playing {0}!!!", Title);
        MessageBox.Show(msg, Title, MessageBoxButtons.OK);
    }
}
}