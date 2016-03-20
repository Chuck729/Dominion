using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    interface IDeck
    {
        /// <summary>
        /// Suffles the deck.
        /// </summary>
        void Shuffle();

        /// <summary>
        /// Returns true when the deck has been changed since the last time this method was called.
        /// </summary>
        /// <returns>True if the deck was changed since the last time this method was called.</returns>
        /// <remarks>Used by viewing objects to keep up with the decks.</remarks>
        bool DeckChanged();

        /// <summary>
        /// Gets a list of all the cards in the desk.
        /// </summary>
        /// <returns>a list of all the cards in the deck.</returns>
        /// <remarks>May not be ordered.</remarks>
        ICollection<Card> Cards();

        /// <summary>
        /// Puts a card at the bottom of the deck.
        /// </summary>
        /// <param name="card"></param>
        void AddCard(Card card);

        /// <summary>
        /// Pops the top card off the deck and returns it.
        /// </summary>
        /// <returns>The card on top of the deck.</returns>
        Card DrawCard();

    }
}
