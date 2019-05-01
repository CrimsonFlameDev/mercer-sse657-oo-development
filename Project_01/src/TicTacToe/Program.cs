namespace TicTacToe.Sim
{
    using GameFactory;
class Program
{
    static void Main()
    {
        Log.Initialize();

        var game = GameFactory.CreateGame();
        game.Play();

        Log.Close();
    }
}
}
