using RHFYP.Cards;

namespace RHFYP.Interfaces
{
    /// <summary>
    /// all possible actions player can be in
    /// </summary>
    public enum PlayerState
    {
        Action,
        Buy,
        TurnOver
    }

    public interface IPlayer
    {
        /// <summary>
        /// The game the that player in in.
        /// </summary>
        IGame Game { get; set; }

        /// <summary>
        /// The current state the player is in.
        /// </summary>
        PlayerState PlayerState { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The deck from which a player draws cards from and puts
        /// them into their hand.
        /// </summary>
        IDeck DrawPile { get; set; }

        /// <summary>
        /// The deck that holds cards that the player currently has
        /// available to use.
        /// </summary>
        IDeck Hand { get; set; }

        /// <summary>
        /// The deck that a player adds their discarded cards to.
        /// </summary>
        IDeck DiscardPile { get; set; }

        /// <summary>
        /// Amount of money a player is currently able to spend on their
        /// turn.
        /// </summary>
        int Gold { get; set; }

        /// <summary>
        /// The number of action cards a player is currently able to 
        /// play during their turn.
        /// </summary>
        int Managers { get; set; }

        /// <summary>
        /// The number of cards a player is currently able to buy on
        /// their turn.
        /// </summary>
        int Investments { get; set; }
        
        /// <summary>
        /// Calculates the number of victory points this player has.
        /// </summary>
        int VictoryPoints { get; }

        /// <summary>
        /// If the player is a winner this is true.
        /// </summary>
        bool Winner { get; set; }

        /// <summary>
        /// Game will call this method when a player's turn begins. The
        /// PlayerState is set to Action. The player begins with one
        /// Manager, zero Gold, and 1 Investment.
        /// </summary>
        void StartTurn();

        /// <summary>
        /// Once the player whose turn it currently is lets the GUI 
        /// know that their turn is over, via button, the PlayerState
        /// will switch from Buy to TurnOver. All of the cards in the
        /// player's hand will be transfered to the player's discard
        /// pile.
        /// </summary>
        void EndTurn();

        /// <summary>
        /// When a player clicks on a card during their turn, and this card
        /// is able to be played currently, this card will by played. Once
        /// the card is played, the card is removed from the player's hand
        /// and added to the player's discard pile.
        /// 
        /// This method also checks to make sure that the card is allowed to
        /// be played given the state of the game.
        /// </summary>
        bool PlayCard(ICard card);


        /// <summary>
        /// When a player's PlayerState is currently Action and it is time for
        /// the player's PlayerState to switch to Buy, either because the player
        /// presses the "End Actions" button or because the player's hand does not
        /// currently hold any action cards, this method changes the PlayerState
        /// from Action to Buy.
        /// </summary>
        void EndActions();


        /// <summary>
        /// When a player clicks on the "Play All Treasures" button, all treasure
        /// cards that are currently in their hand are played.
        /// </summary>
        void PlayAllTreasures();


        /// <summary>
        /// When a player purchases a card, an instance of that card is removed from
        /// that type of card's pile and added to the player's DiscardPile. The amount
        /// of money that is spent on the card is removed from the player's Gold, and
        /// Investment is decreased by one.
        /// </summary>
        /// <param name="card"></param> Returns true if card was bought, false otherwise.
        bool GiveCard(ICard card);

        /// <summary>
        /// Looks through all of the players cards, in no particular order, and looks for
        /// <param name="card"></param>.  If it finds the <param name="card"></param> then
        /// It will move that <param name="card"></param> to the trash pile.
        /// </summary>
        /// <param name="card">The card to trash.</param>
        /// <returns>True if the card was found and trashed.</returns>
        bool TrashCard(ICard card);

        /// <summary>
        /// Takes a hand from the players draw pile and puts it into the players hand.
        /// </summary>
        /// <returns>True if a card was drawn.</returns>
        /// <remarks>The discard deck should be shuffled into the players hand if there are no more cards.</remarks>
        bool DrawCard();

        /// <summary>
        /// Returns true if the player has at least one card of <see cref="CardType"/> action.
        /// </summary>
        bool ActionCardsInHand { get; }

        bool TreasureCardsInHand { get; }

        bool CanAfford(ICard card);
    }
}