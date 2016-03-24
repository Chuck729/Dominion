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

        public Deck()
        {
            this.CardList = new List<Card>();
        }

        public void AddCard(Card card)
        {
            CardList.Add(card);
        }

        public IDeck AppendDeck(IDeck deck)
        {
            throw new NotImplementedException();
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
                //do something
            }

            int index = CardList.Count - 1;
            Card c = CardList[index];
            CardList.RemoveAt(index);
            return c;
        }

        public ICollection<Card> DrawCards(int n)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
