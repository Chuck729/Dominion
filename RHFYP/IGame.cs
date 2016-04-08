using RHFYP.Cards;

namespace RHFYP
{
    //This class will control the interactions between the players and cards and handle all piles of cards available to buy
    public interface IGame
    {
        /// <summary>
        /// How many players are in the game.
        /// </summary>
        int NumberOfPlayers { get; set; }

        /// <summary>
        /// populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the game
        /// </summary>
        void GenerateCards();

        /// <summary>
        /// Creates players and deals them the proper number of cards.
        /// </summary>
        void SetupPlayers();

        /// <summary>
        /// method called when a card is bought and will take a card out of the deck passed in by the parameter
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="player"></param>
        ICard BuyCard(IDeck pile, IPlayer player);

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        IDeck BuyDeck { get; set; }
    }
}
