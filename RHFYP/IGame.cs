using System.Collections.Generic;
using RHFYP.Cards;

namespace RHFYP
{
    //This class will control the interactions between the players and cards and handle all piles of cards available to buy.
    public interface IGame
    {
        /// <summary>
        /// Keeps track of the player who's turn it currently is.
        /// </summary>
        int CurrentPlayer { get; set; }

        /// <summary>
        /// A list of all the players in the Game.
        /// </summary>
        List<Player> Players { get; }

        /// <summary>
        /// The number of players in the Game.
        /// </summary>
        int NumberOfPlayers { get; set; }

        /// <summary>
        /// Populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the Game.
        /// </summary>
        void GenerateCards();

        /// <summary>
        /// Creates players and deals them the proper number of cards.
        /// </summary>
        void SetupPlayers(string[] names);

        /// <summary>
        /// This method is called when a card is bought and will take a card out of the deck passed in by the parameter.
        /// </summary>
        /// <param name="name">The game of the card you want to sell the player.</param>
        /// <param name="player">The player you want to sell the card to.</param>
        /// <param name="x">The x cord the player wants the tile at.</param>
        /// <param name="y">The y cord the player wants the tile at.</param>
        /// <returns>True if the card was bought.</returns>
        bool BuyCard(string name, IPlayer player, int x, int y);

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        IDeck BuyDeck { get; set; }

        /// <summary>
        ///     Starts the turn of the next player in the Game.
        /// </summary>
        void NextTurn();
    }
}
