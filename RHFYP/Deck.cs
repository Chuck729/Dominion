using System;
using System.Collections.Generic;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Deck : IDeck
    {

        public List<ICard> CardList { get; set; }

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

        public virtual void AddCard(ICard card)
        {
            if (card == null)
            {
                throw  new ArgumentNullException(nameof(card));
            }

            if (!card.IsAddable)
            {
                throw new CardAddException();
            }

            CardList.Add(card);
            card.IsAddable = false;
        }

        public class CardAddException : Exception
        {
        }

        public IDeck AppendDeck(IDeck deck)
        {
            return deck == null ? new Deck(Cards()) : new Deck(Cards().Concat(deck.Cards()));
        }

        public int CardCount()
        {
           return CardList.Count;
        }

        public ICollection<ICard> Cards()
        {
            return CardList;
        }

        public ICard DrawCard()
        {
            if(CardList.Count == 0)
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

        /// <summary>
        /// Removes the first card that meets the given condition
        /// </summary>
        /// <param name="pred"></param> Condition that must be met
        /// <returns></returns>
      
        public ICard GetFirstCard(Predicate<ICard> pred)
        {
            var c = CardList.Find(pred);
            if (c == null)
                return null;
            CardList.RemoveAt(CardList.IndexOf(c));
            c.IsAddable = true;
            return c;
        }


        public bool InDeck(ICard card)
        {
           return CardList.Contains(card);
        }

        public void Shuffle()
        {
            var shuffledCards = new List<ICard>();
            var rnd = new Random();
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

        public void ShuffleIn(IDeck otherCards)
        {
            if (otherCards == null) throw new ArgumentNullException(nameof(otherCards));
            for(var i = otherCards.CardCount() - 1; i >= 0; i--)
            {
                var drawn = otherCards.DrawCard();
                AddCard(drawn);
            }

            otherCards.CardList.Clear();
         
            Shuffle();
        }

        public Deck SubDeck(Predicate<ICard> pred)
        {
           
            var subCards = CardList.Where(pred.Invoke).ToList();
            return new Deck(subCards);
        }
    }
}
