using RHFYP.Cards;

namespace RHFYP
{
    public class Game : IGame
    {
        public bool PlayerChanged { get; set; }

        public Player CurrentPlayer { get; set; }

        public Game()
        {
            BuyDeck = new Deck();
        }

        /// <summary>
        /// How many players are in the game.
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the game
        /// </summary>
        public void GenerateCards()
        {
            while(BuyDeck.DrawCard() != null) { }

            for (var i = 0; i < 60; i++)
            {
                BuyDeck.AddCard(new SmallBusiness());
            }

            for (var i = 0; i < 40; i++)
            {
                BuyDeck.AddCard(new Company());
            }

            for (var i = 0; i < 30; i++)
            {
                BuyDeck.AddCard(new Corporation());
            }

            for (var i = 0; i < 8 + 3 * NumberOfPlayers; i++)
            {
                BuyDeck.AddCard(new Purdue());
            }

            for (var i = 0; i < 8; i++)
            {
                BuyDeck.AddCard(new Mit());
            }

            for (var i = 0; i < (NumberOfPlayers - 1) * 10; i++)
            {
                BuyDeck.AddCard(new HippieCamp());
            }
        }

        /// <summary>
        /// method called when a card is bought and will take a card out of the deck passed in by the parameter
        /// </summary>
        /// <param name="pile"></param>
        public ICard BuyCard(IDeck pile, IPlayer player)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        public IDeck BuyDeck { get; set; }
    }
}