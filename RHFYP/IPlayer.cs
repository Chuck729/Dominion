using RHFYP.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{

    public enum PlayerState
    {
        Action,
        Buy,
        TurnOver
    }

    interface IPlayer
    {
        /// <summary>
        /// The current state the player is in.
        /// </summary>
        PlayerState PlayerState { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// The deck from which a player draws cards from and puts
        /// them into their hand.
        /// </summary>
        Deck DrawPile { get; set; }

        /// <summary>
        /// The deck that holds cards that the player currently has
        /// available to use.
        /// </summary>
        Deck Hand { get; set; }

        /// <summary>
        /// The deck that a player adds their discarded cards to.
        /// </summary>
        Deck DiscardPile { get; set; }

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
        /// Game will call this method when a player's turn begins. The
        /// PlayerState is set to Action.
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
        /// </summary>
        void PlayCard(ICard card);


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
        /// <param name="card"></param>
        void BuyCard(ICard card);
    }
}