using System;
using System.Collections.Generic;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Deck : IDeck
    {

        public Deck()
        {
            CardList = new List<ICard>();
        }

        public Deck(IEnumerable<ICard> cards)
        {
            CardList = new List<ICard>();
            if (cards == null) return;
            var cardsArray = cards as ICard[] ?? cards.ToArray();
            if (cardsArray.Any(card => card == null))
            {
                throw new ArgumentNullException(nameof(cards));
            }
            CardList.AddRange(cardsArray);
        }

        public List<ICard> CardList { get; set; }

        public virtual void AddCard(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            if (!card.IsAddable)
            {
                throw new CardAddException("Card is not addable.  It might be in another deck.");
            }

            CardList.Add(card);
            card.IsAddable = false;
        }

        public IDeck AppendDeck(IDeck deck)
        {
            return deck == null ? new Deck(Cards()) : new Deck(Cards().Concat(deck.Cards()));
        }

        public ICollection<ICard> Cards()
        {
            return CardList;
        }

        public ICard DrawCard()
        {
            if (CardList.Count == 0)
            {
                return null; //TODO needs to shuffle in discard deck but this handles the error for now
            }

            var c = CardList[0];
            c.IsAddable = true;
            CardList.RemoveAt(0);
            return c;
        }

        public IDeck DrawCards(int n)
        {
            IDeck nextCards = new Deck();

            for (var x = 0; x < n; x++)
            {
                nextCards.AddCard(DrawCard());
            }

            return nextCards;
        }

        public ICard GetFirstCard(Predicate<ICard> pred)
        {
            if (pred == null) throw new ArgumentNullException(nameof(pred));

            var c = CardList.Find(pred);
            if (c == null)
                return null;
            CardList.RemoveAt(CardList.IndexOf(c));
            c.IsAddable = true;
            return c;
        }


        public bool InDeck(ICard card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));

            return CardList.Contains(card);
        }

        public void Shuffle(int seed = 0)
        {
            var shuffledCards = new List<ICard>();
            var rnd = new Random(seed);
            while (CardList.Count > 1)
            {
                var index = rnd.Next(0, CardList.Count); //pick a random item from the master list
                shuffledCards.Add(CardList[index]); //place it at the end of the randomized list
                CardList.RemoveAt(index);
            }
            shuffledCards.Add(CardList[0]); // unnecessary to call rnd.Next(0,1) because
            // it will always return 0
            CardList.RemoveAt(0);
            CardList = shuffledCards;
        }

        public void ShuffleIn(IDeck otherCards, int seed = 0)
        {
            if (otherCards == null) throw new ArgumentNullException(nameof(otherCards));
            for (var i = otherCards.CardList.Count() - 1; i >= 0; i--)
            {
                var drawn = otherCards.DrawCard();
                AddCard(drawn);
            }

            otherCards.CardList.Clear();

            Shuffle(seed);
        }

        public Deck SubDeck(Predicate<ICard> pred)
        {
            if (pred == null) throw new ArgumentNullException(nameof(pred));
            var subCards = CardList.Where(pred.Invoke).ToList();
            return new Deck(subCards);
        }

        /// <summary>
        ///     Thrown when there is an exception while adding a card.
        /// </summary>
        private class CardAddException : Exception
        {
            public CardAddException(string message) : base(message)
            {
            }
        }
    }
}