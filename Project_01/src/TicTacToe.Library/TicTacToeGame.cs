using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TicTacToe.Library
{
public class TicTacToeGame
{
    private readonly List<string> _players = new List<string>(2);
    private bool?[,] _grid;
    private bool? _isGameWon;

    /// <summary>
    /// Indicates if the game has been won by anyone or not.
    /// </summary>
    private bool IsGameWon
    {
        get { return _isGameWon.HasValue && _isGameWon.Value; }
    }

    protected TicTacToeGame()
    {
    }

    public TicTacToeGame(string playerX, string playerO)
    {
        _players.Add(playerX);
        _players.Add(playerO);
    }

    /// <summary>
    /// This method executes the play of the Tic-Tac-Toe game.
    /// </summary>
    public void Play()
    {
        while (true)
        {
            _grid = new bool?[3, 3];
            var isPlayerO = false;
            _isGameWon = null;
            do
            {
                var player = Convert.ToInt16(isPlayerO);

                // Update the UI.
                Console.WriteLine();
                Console.WriteLine("{0}'s turn...", _players[player]);

                // Retrieve and validate the input.
                string[] input = { };
                ValidateInput(ref input);

                Console.WriteLine();

                // Place the X or O in the appropriate grid cell.
                var row = Convert.ToInt16(input.First());
                var col = Convert.ToInt16(input.Last());
                _grid[row - 1, col - 1] = isPlayerO;

                // Display the grid.
                for (var r = 0; r < 3; r++)
                {
                    for (var c = 0; c < 3; c++)
                    {
                        var cell = _grid[r, c];
                        var cellValue = cell.HasValue
                            ? ((cell.Value ? "_O_" : "_X_"))
                            : "___";
                        Console.Write("{0}{1}", cellValue, (c < 2) ? "|" : "");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                // Check to see if there is a winner.
                CheckForWinner();

                // Switch to the other player.
                isPlayerO = !isPlayerO;

                // If the game has been won, exit the loop.
                if (IsGameWon) break;

                // Check for a draw game.
                _isGameWon = false;
                foreach (var cell in _grid)
                {
                    if (cell.HasValue) continue;

                    _isGameWon = null;
                    break;
                }
            } while (!_isGameWon.HasValue);

            Console.WriteLine();

            // Prompt the user to continue playing.
            Console.WriteLine("Play again? (y/n)");
            if (!Console.ReadKey().Key.Equals(ConsoleKey.Y))
                break;
            Console.WriteLine();
        }
    }

    /// <summary>
    /// This method checks the grid for a winning pattern
    /// (i.e three X's or three O's in a row).
    /// </summary>
    private void CheckForWinner()
    {
        // Check for 3-in-a-row on rows.
        CheckForThree();

        // Check for 3-in-a-row on columns.
        CheckForThree(true);

        // Check for 3-in-a-row diagonally.
        CheckForThreeDiagonal();
    }

    /// <summary>
    /// This method checks the grid (vertically or horizontally) for a
    /// winning pattern (i.e three X's or three O's in a row).
    /// </summary>
    /// <param name="checkVertically">If true, checks the grid vertically for the
    /// winning pattern; otherwise it checks the grid horizontally.</param>
    private void CheckForThree(bool checkVertically = false)
    {
        for (var c = 0; (c < 3) && !IsGameWon; c++)
        {
            var row = (checkVertically) ? 1 : c;
            var col = (checkVertically) ? c : 1;

            var midPt = _grid[row, col];
            if (!midPt.HasValue) continue;
            CheckForThreeFromMidpoint(row, col, checkVertically);
        }
    }

    /// <summary>
    /// This method determines if a point is in the middle of three in a row.  If so,
    /// the game is marked as won.
    /// </summary>
    /// <param name="row">The row of the midpoint.</param>
    /// <param name="col">The column of the midpoint.</param>
    /// <param name="checkVertically">Indicates if the check should be performed 
    /// vertically or horizontally.  If true, the check should be performed vertically;
    /// otherwise the check is done horizontally.</param>
    private void CheckForThreeFromMidpoint(int row, int col, bool checkVertically)
    {
        var beforeMid = (checkVertically) ? _grid[row - 1, col] : _grid[row, col - 1];
        var afterMid = (checkVertically) ? _grid[row + 1, col] : _grid[row, col + 1];
        CheckForMatch(beforeMid, _grid[row, col], afterMid);
    }

    /// <summary>
    /// This method checks the grid for a winning diagonal pattern
    /// (i.e three X's or three O's in a row).  If a winning patern
    /// is found, the game is marked as won.
    /// </summary>
    private void CheckForThreeDiagonal()
    {
        var midPt = _grid[1, 1];

        if (!midPt.HasValue) return;

        CheckForMatch(_grid[0, 0], midPt, _grid[2, 2]);

        if (IsGameWon) return;

        CheckForMatch(_grid[2, 0], midPt, _grid[0, 2]);
    }

    /// <summary>
    /// This method determines if three points are the same.
    /// If they are, the game is marked as won.
    /// </summary>
    private void CheckForMatch(bool? left, bool? midPt, bool? right)
    {
        if ((!left.HasValue) || (!right.HasValue)) return;

        if (left == midPt && midPt == right)
        {
            _isGameWon = true;
        }
    }

    /// <summary>
    /// This method validates the input entered by the player.
    /// If invalid input is entered, the user is informed of the
    /// error and prompted to enter another set of input.
    /// </summary>
    /// <param name="input"></param>
    private void ValidateInput(ref string[] input)
    {
        do
        {
            try
            {
                input = Console.ReadLine().Split(' ');

                if (input.Length != 2) throw new FormatException();

                var row = short.Parse(input[0]);
                var col = short.Parse(input[1]);

                if ((row < 1 || row > 3) || (col < 1 || col > 3))
                    throw new FormatException();

                if (_grid[row - 1, col - 1].HasValue) throw new ReadOnlyException();
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Try again...");
            }
            catch (ReadOnlyException)
            {
                Console.WriteLine("This cell has a value!  Try again...");
            }
        } while (true);
    }
}
}
