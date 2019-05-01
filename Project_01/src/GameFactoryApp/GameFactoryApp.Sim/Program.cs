using GameFactoryApp.Lib;

namespace GameFactoryApp.Sim
{
internal class Program
{
    private static void Main()
    {
        Log.Initialize();

        var game = GameFactory.CreateGame();
        game.Play();

        Log.Close();
    }
}
}
