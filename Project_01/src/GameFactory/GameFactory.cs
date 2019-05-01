using System;
using System.Collections.Generic;
using TicTacToe.Library;

namespace GameFactory
{
    public static class GameFactory
    {
        public static TicTacToeGame CreateGame()
        {
            var numOfPlayers = 2;
            var players = new List<string>(numOfPlayers);
            for (var i = 0; i < numOfPlayers; i++)
            {
                Console.WriteLine("Enter player {0}'s name: ", i + 1);
                var name = Console.ReadLine();
                if (name.Length > 25) name = name.Substring(0, 25);
                players.Add(name);
            }

            return new TicTacToeGame(players[0], players[1]);
        }
    }
}
