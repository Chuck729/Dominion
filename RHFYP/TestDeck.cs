using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class TestDeck : IDeck
    {
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly List<Card> _cards = new List<Card>(); 

        public TestDeck(IEnumerable<Card> cards)
        {
            if (cards != null)
                _cards.AddRange(cards);
        }

        /// <summary>
        /// Suffles the given selection of cards into the list.
        /// </summary>
        /// <remarks>Passing null will result in just shuffling this list</remarks>
        public void ShuffleIn(ICollection<Card> otherCards)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shuffles the deck.
        /// </summary>
        public void Shuffle()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns true when the deck has been changed since the last time this method was called.
        /// </summary>
        /// <returns>True if the deck was changed since the last time this method was called.</returns>
        /// <remarks>Used by viewing objects to keep up with the decks. (But can only be used by one viewing object and that might want to be fixed.)</remarks>
        public bool WasDeckChanged()
        {
            return true;
        }

        /// <summary>
        /// Gets a list of all the cards in the desk.
        /// </summary>
        /// <returns>a list of all the cards in the deck.</returns>
        /// <remarks>May not be ordered.</remarks>
        public ICollection<Card> Cards()
        {
            return _cards;
        }

        /// <summary>
        /// Puts a card at the bottom of the deck.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pops the top card off the deck and returns it.
        /// </summary>
        /// <returns>The card on top of the deck.</returns>
        /// <remarks>
        /// This could just call DrawCards(1)
        /// Or if there are no cards in the deck to draw it could trigger an event?  So we could shuffle in another deck and then continue to draw.
        /// </remarks>
        public Card DrawCard()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pops the top n cards off the deck and returns them.
        /// </summary>
        /// <returns>The card on top of the deck.</returns>
        /// <remarks>
        /// Or if there are not enough cards in the deck to draw it could trigger an event?  So we could shuffle in another deck and then continue to draw.
        /// </remarks>
        public IList<Card> DrawCards(int n)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks to see if the given card instance is in this deck.
        /// </summary>
        /// <param name="card">The exact <see cref="Card"/> instance you want to look for.</param>
        /// <returns>True if a pointer to the given card exists in this deck.</returns>
        public bool InDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public Card GetFirstCard(Predicate<Card> pred)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Appends two decks together and returns a new deck with all the objects.
        /// </summary>
        /// <param name="deck">The deck.</param>
        /// <returns>New deck with pointers to all of the items</returns>
        public IDeck AppendDeck(IDeck deck)
        {
            return new TestDeck(Cards().Concat(deck.Cards()));
        }

        /// <summary>
        /// Returns a subdeck with only cards that satisfy the predicate.
        /// </summary>
        /// <param name="pred">Predicate.</param>
        /// <returns>The <see cref="IDeck"/> containing cards that pass the <paramref name="pred"/>.</returns>
        /// <remarks>Currently used by graphics to seperate decks by class.</remarks>
        public Deck SubDeck(Predicate<Card> pred)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the number of cards that are the same type as the passed in card in this deck.
        /// </summary>
        /// <returns>How many cards of the passed in type exist in the deck.  0 if no cards of that type exist in the deck.</returns>
        public int CountCardType(string type)
        {
            throw new NotImplementedException();
        }

        public int CardCount()
        {
            return _cards.Count;
        }

        public IEnumerable<object> Select(Func<object, Point> p)
        {
            throw new NotImplementedException();
        }

        public void ShuffleIn(IDeck otherCards)
        {
            throw new NotImplementedException();
        }
    }
}
