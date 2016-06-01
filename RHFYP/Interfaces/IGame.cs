using System.Collections.Generic;
using RHFYP.Cards;

namespace RHFYP.Interfaces
{
    public enum GameState
    {
        InProgress,
        Ended
    }

    public enum UserResponse
    {
        Yes,
        No,
        Discard,
        PutOnDeck,
        Trash,
        Steal,
        CardInHand
    }

    //This class will control the interactions between the players and cards and handle all piles of cards available to buy.
    public interface IGame
    {

        /// <summary>
        /// Gets or sets a value indicating whether the game needs user input.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the game needs user input; otherwise, <c>false</c>.
        /// </value>
        bool NeedUserInput { get; set; }

        /// <summary>
        /// Gets or sets the user input prompt.
        /// </summary>
        /// <value>
        /// The user input prompt.
        /// </value>
        string UserInputPrompt { get; set; }

        /// <summary>
        /// Gets or sets the possible responses for the games current input request.
        /// </summary>
        /// <value>
        /// The possible responses.
        /// </value>
        ICollection<UserResponse> PossibleUserResponses { get; set; }

        /// <summary>
        /// Gets or sets the users response.
        /// </summary>
        /// <value>
        /// The users response.
        /// </value>
        UserResponse UserResponse { get; set; }

        /// <summary>
        /// Gets or sets the card that a simple yes no dialog card viewer can display.
        /// </summary>
        /// <value>
        /// The yes no dialog card viewer.
        /// </value>
        ICard PublicCardForUiUserInput { get; set; }

        /// <summary>
        ///     Keeps track of the player who's turn it currently is.
        /// </summary>
        int CurrentPlayer { get; }

        /// <summary>
        ///     A list of all the players in the Game.
        /// </summary>
        List<Player> Players { get; }

        /// <summary>
        ///     The number of players in the Game.
        /// </summary>
        int NumberOfPlayers { get; }

        /// <summary>
        ///     The current state of the game.
        /// </summary>
        GameState GameState { get; set; }

        /// <summary>
        ///     The deck of cards that are in all the "draw" piles.
        /// </summary>
        IDeck BuyDeck { get; set; }

        /// <summary>
        ///     The games global trash deck.
        /// </summary>
        /// <remarks>All players nee da reference to this object.</remarks>
        IDeck TrashDeck { get; set; }

        /// <summary>
        ///     Populates decks with 10 action cards (from the list or random if list is null)
        ///     , 3 treasure cards, and 6 victory cards for the Game.
        /// </summary>
        void GenerateCards(List<ICard> actionCardList);

        /// <summary>
        ///     Creates players and deals them the proper number of cards.
        /// </summary>
        void SetupPlayers(string[] names);

        /// <summary>
        ///     This method is called when a card is bought and will take a card out of the deck passed in by the parameter.
        /// </summary>
        /// <param name="name">The game of the card you want to sell the player.</param>
        /// <param name="player">The player you want to sell the card to.</param>
        /// <param name="x">The x cord the player wants the tile at.</param>
        /// <param name="y">The y cord the player wants the tile at.</param>
        /// <returns>True if the card was bought.</returns>
        bool BuyCard(string name, IPlayer player, int x, int y);

        /// <summary>
        ///     Starts the turn of the next player in the Game.
        /// </summary>
        void NextTurn();

        /// <summary>
        ///     Does all the nessesary things to end a game after a win
        ///     condition has been detected.
        /// </summary>
        void EndGame();

        /// <summary>
        ///     Calls the GenerateCards(List<ICard> actionCardList) with the null parameter
        /// </summary>
        void GenerateCards();

        /// <summary>
        /// Blocking call to get input from the user.  Posts a response request that the UI has to pick up on and respond to
        /// </summary>
        /// <param name="possibleResponses">The possible responses the user can give.</param>
        /// <returns>The response that the user chose.</returns>
        UserResponse GetUserResponse(ICollection<UserResponse> possibleResponses);
    }
}