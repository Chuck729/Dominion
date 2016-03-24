using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.CardList = new List<Card>();
        }

        public Deck(IEnumerable<Card> cards)
        {
            if(cards != null)
                CardList.AddRange(cards);
        }

        public void AddCard(Card card)
        {
            CardList.Add(card);
            WasChanged = true;
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

        public int CountCardType(string type)
        {
            throw new NotImplementedException();
        }

        public Card DrawCard()
        {
            if(CardList.Count == 0)
            {
                
                return null; //TODO needs to shuffle but this handles the error for now
            }

            
            Card c = CardList[0];
            CardList.RemoveAt(0);
            WasChanged = true;
            return c;
        }

        public IList<Card> DrawCards(int n)
        {
            List<Card> nextCards = new List<Card>();

            for (int x = 0; x < n; x++)
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
            foreach (Card c in CardList)
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
            
        }
        public void ShuffleIn(ICollection<Card> otherCards)
        {
            throw new NotImplementedException();
        }

        public IDeck SubDeck(Predicate<Card> pred)
        {
            throw new NotImplementedException();
        }

        public bool WasDeckChanged()
        {
            bool value = WasChanged;
            WasChanged = false;
            return value;
        }
    }
}
