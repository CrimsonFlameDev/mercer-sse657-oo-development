using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using UnoCardColor = Uno.Library.UnoCard.UnoCardColor;
using UnoCardAction = Uno.Library.UnoCard.UnoCardAction;

namespace Uno.Library
{
public static class UnoGameFactory
{
    private const int UnoCardColors = 4;
    private const int UnoCardRanks = 9;
    private const int UnoCardActions = 3;
    private const int UnoWildCards = 4;
    private const int UnoWildCardActions = 2;

    private static readonly IUnityContainer _container = new UnityContainer();

    public static void Initialize()
    {
        _container.RegisterInstance(new UnoGame(CreateUnoCardDeck()),
            new ContainerControlledLifetimeManager());
        var uno = UnoGame;
        var min = UnoGame.MinNumOfPlayers;
        var max = UnoGame.MaxNumOfPlayers;

        Console.WriteLine("Welcome to the card game UNO!\n\n" +
                          "How many players will be playing ({0}-{1})?", min, max);

        int numOfPlayers;
        while ((!int.TryParse(Console.ReadLine(), out numOfPlayers)) ||
                (numOfPlayers < min || numOfPlayers > max))
        {
            Console.WriteLine("Please enter a valid number of players ({0}-{1}):",
                              min, max);
        }

        for (int i = 0; i < numOfPlayers; i++)
        {
            Console.WriteLine("Enter player {0}'s name: ", i + 1);
            uno.AddPlayer(Console.ReadLine());
        }

        Console.WriteLine();
    }

    public static UnoGame UnoGame
    {
        get { return _container.Resolve<UnoGame>(); }
    }

    private static List<UnoCard> CreateUnoCardDeck()
    {
        var newDeck = new List<UnoCard>();

        // Create the color cards.
        for (int color = 0; color < UnoCardColors; color++)
        {
            // Create the single zero card.
            newDeck.Add(new UnoCard((UnoCardColor) color, 0));

            // Create two instances of the 1-9 cards.
            for (int rank = 1; rank <= UnoCardRanks; rank++)
            {
                newDeck.Add(new UnoCard((UnoCardColor) color, rank));
                newDeck.Add(new UnoCard((UnoCardColor) color, rank));
            }

            // Create two instances of the action cards.
            for (int action = 1; action <= UnoCardActions; action++)
            {
                newDeck.Add(new UnoCard((UnoCardColor) color, (UnoCardAction) action));
                newDeck.Add(new UnoCard((UnoCardColor) color, (UnoCardAction) action));
            }
        }

        // Create the Wild cards.
        for (int wildCard = 0; wildCard < UnoWildCards; wildCard++)
        {
            for (int wildCardAction = 4;
                wildCardAction < UnoWildCardActions + 3;
                wildCardAction++)
            {
                newDeck.Add(new UnoCard(UnoCardColor.Black,
                            (UnoCardAction) wildCardAction));
                newDeck.Add(new UnoCard(UnoCardColor.Black,
                            (UnoCardAction) wildCardAction + 1));
            }
        }

        return newDeck;
    }
}
}
