using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Deck : IDeck
    {
        // TODO: Need a WasDeckChanged() method
        // TODO: Need a List<Card> LookAtDeck() method

        public List<Card> cards { get; set; }
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public IDeck AppendDeck(IDeck deck)
        {
            throw new NotImplementedException();
        }

        public int CardCount()
        {
           return cards.Capacity;
        }

        public ICollection<Card> Cards()
        {
            throw new NotImplementedException();
        }

        public int CountCardType(string type)
        {
            throw new NotImplementedException();
        }

        public Card DrawCard()
        {
            if(cards.Count == 0)
            {
                //do something
            }

            int index = cards.Count - 1;
            Card c = cards[index];
            cards.RemoveAt(index);
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

        public Card GetFirstCard(Predicate<Card> pred)
        {
            throw new NotImplementedException();
        }

        public bool InDeck(Card card)
        {
           return cards.Contains(card);
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
