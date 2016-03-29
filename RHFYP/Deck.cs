using System;
using System.Collections.Generic;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Deck : IDeck
    {
        // TODO: Need a WasDeckChanged() method
        // TODO: Need a List<Card> LookAtDeck() method

        public List<Card> CardList { get; set; }
        public bool WasChanged { get; set; }

        public Deck()
        {
            CardList = new List<Card>();
        }

        public Deck(IEnumerable<Card> cards)
        {
            CardList = new List<Card>();
            if(cards != null)
                CardList.AddRange(cards);
        }

        public void AddCard(Card card)
        {
            if (card.IsAddable)
            {
                CardList.Add(card);
                card.IsAddable = false;
                WasChanged = true;
            } else
            {
                throw new Exception("Card is not addable");
            }
        }

        public IDeck AppendDeck(IDeck deck)
        {
            return new Deck(Cards().Concat(deck.Cards()));
        }

        public int CardCount()
        {
           return CardList.Count;
        }

        public ICollection<Card> Cards()
        {
            return CardList;
        }

        public Card DrawCard()
        {
            if(CardList.Count == 0)
            {
                return null; //TODO needs to shuffle in discard deck but this handles the error for now
            }

            var c = CardList[0];
            c.IsAddable = true;
            CardList.RemoveAt(0);
            WasChanged = true;
            return c;
        }

        public IList<Card> DrawCards(int n)
        {
            var nextCards = new List<Card>();

            for (var x = 0; x < n; x++)
            {
                nextCards.Add(DrawCard());
            }
            return nextCards;
        }

        /// <summary>
        /// Removes the first card that meets the given condition
        /// </summary>
        /// <param name="pred"></param> Condition that must be met
        /// <returns></returns>
        public Card GetFirstCard(Predicate<Card> pred)
        {
            foreach (var c in CardList)
            {
                if(pred.Invoke(c))
                {
                    CardList.RemoveAt(CardList.IndexOf(c));
                    return c;
                }
            }
            return null;
        }

        public bool InDeck(Card card)
        {
           return CardList.Contains(card);
        }

        public void Shuffle()
        {
            var shuffledCards = new List<Card>();
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
            foreach (var drawn in otherCards.Cards().Select(c => otherCards.DrawCard()))
            {
                AddCard(drawn);
            }
            Shuffle();
        }

        public Deck SubDeck(Predicate<Card> pred)
        {
            var subCards = CardList.Where(pred.Invoke).ToList();
            return new Deck(subCards);
        }

        public bool WasDeckChanged()
        {
            var value = WasChanged;
            WasChanged = false;
            return value;
        }
    }
}
