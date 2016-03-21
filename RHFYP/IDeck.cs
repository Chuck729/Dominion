using System.Collections.Generic;

namespace RHFYP
{
    interface IDeck : ICollection<Card>
    {
        /// <summary>
        /// Suffles the given selection of cards into the list.
        /// </summary>
        /// <remarks>Passing null will result in just shuffling this list</remarks>
        void ShuffleIn(ICollection<Card> otherCards);

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

        /// <summary>
        /// Checks to see if the given card instance is in this deck.
        /// </summary>
        /// <param name="card">The exact <see cref="Card"/> instance you want to look for.</param>
        /// <returns>True if a pointer to the given card exists in this deck.</returns>
        bool InDeck(Card card);

        /// <summary>
        /// Appends two decks together and returns a new deck with all the objects.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <returns>New deck with pointers to all of the items</returns>
        IDeck AppendDeck(IDeck deck);

        /// <summary>
        /// Returns the number of cards that are the same type as the passed in card in this deck.
        /// </summary>
        /// <returns>How many cards of the passed in type exist in the deck.  0 if no cards of that type exist in the deck.</returns>
        int CountCardType(string type);

        int CardCount();

        //TODO: There should be a "GetCardsOfType" method that gets all treasure cards or all resource cards etc.

    }
}
