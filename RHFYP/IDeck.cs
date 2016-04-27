using System;
using System.Collections.Generic;
using RHFYP.Cards;

namespace RHFYP
{
    public interface IDeck
    {
        /// <summary>
        ///     The list of cards that this deck started as.
        /// </summary>
        List<ICard> DefaultCardList { get; set; }

        /// <summary>
        ///     The list of cards inside the deck
        /// </summary>
        List<ICard> CardList { get; set; }

        /// <summary>
        ///     Suffles the given selection of cards into the list.
        /// </summary>
        /// <param name="otherCards">The <see cref="IDeck" /> that has the card you want to shuffle in.</param>
        /// <param name="seed">The seed for the random shuffle.</param>
        /// <remarks>Passing null will result in just shuffling this list.</remarks>
        void ShuffleIn(IDeck otherCards, int seed);

        /// <summary>
        ///     Shuffles the deck.
        /// </summary>
        void Shuffle(int seed);

        /// <summary>
        ///     Gets a list of all the cards in the desk.
        /// </summary>
        /// <returns>a list of all the cards in the deck.</returns>
        /// <remarks>May not be ordered.</remarks>
        ICollection<ICard> Cards();

        /// <summary>
        ///     Puts a card at the bottom of the deck.
        /// </summary>
        /// <param name="card">The <see cref="ICard" /> object you was to add.</param>
        void AddCard(ICard card);

        /// <summary>
        ///     Pops the top card off the deck and returns it.
        /// </summary>
        /// <returns>The card on top of the deck.</returns>
        /// <remarks>
        ///     This could just call DrawCards(1)
        ///     Or if there are no cards in the deck to draw it could trigger an event?  So we could shuffle in another deck and
        ///     then continue to draw.
        /// </remarks>
        ICard DrawCard();

        /// <summary>
        ///     Pops the top n cards off the deck and returns them.
        /// </summary>
        /// <param name="n">Number of card to draw.</param>
        /// <returns>The card on top of the deck.</returns>
        /// <remarks>
        ///     Or if there are not enough cards in the deck to draw it could trigger an event?  So we could shuffle in another
        ///     deck and then continue to draw.
        /// </remarks>
        IDeck DrawCards(int n);

        /// <summary>
        ///     Checks to see if the given card instance is in this deck.
        /// </summary>
        /// <param name="card">The exact <see cref="ICard" /> instance you want to look for.</param>
        /// <returns>True if a pointer to the given card exists in this deck.</returns>
        bool InDeck(ICard card);

        /// <summary>
        ///     Removes the first card that meets the given condition
        /// </summary>
        /// <param name="pred">The condition that the card must meet.</param>
        /// <returns>The card if the card exists.</returns>
        ICard GetFirstCard(Predicate<ICard> pred);

        /// <summary>
        ///     Appends two decks together and returns a new deck with all the objects.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <returns>New deck with pointers to all of the items</returns>
        IDeck AppendDeck(IDeck deck);

        /// <summary>
        ///     Returns a subdeck with only cards that satisfy the predicate.
        /// </summary>
        /// <param name="pred">Predicate.</param>
        /// <returns>The <see cref="IDeck" /> containing cards that pass the <paramref name="pred" />.</returns>
        /// <remarks>Currently used by graphics to seperate decks by class.</remarks>
        Deck SubDeck(Predicate<ICard> pred);

        /// <summary>
        /// Clears the decks card list and set it to the default card list.
        /// </summary>
        void ResetToDefault();

        /// <summary>
        /// Returns the number of types where at least one card of that type existed
        /// in the default card list but no card of that type still remain in the card list.
        /// </summary>
        /// <returns>Number of depleted types.</returns>
        int NumberOfDepletedNames();
    }
}