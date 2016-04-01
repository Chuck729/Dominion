using RHFYP.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    //This class will control the interactions between the players and cards and handle all piles of cards available to buy
    public interface IGame
    {
        /// <summary>
        /// populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the game
        /// </summary>
        void generateCards();

        /// <summary>
        /// method called when a card is bought and will take a card out of the deck passed in by the parameter
        /// </summary>
        /// <param name="pile"></param>
        ICard buyCard(IDeck pile, IPlayer player);

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        IDeck BuyDeck { get; set; }
    }
}
