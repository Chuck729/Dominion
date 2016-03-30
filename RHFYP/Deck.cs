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
        // TODO: Need a List<ICard> LookAtDeck() method

        public List<ICard> CardList { get; set; }
        public bool WasChanged { get; set; }

        public Deck()
        {
            this.CardList = new List<ICard>();
        }

        public Deck(IEnumerable<ICard> cards)
        {
            CardList = new List<ICard>();
            if(cards != null)
                CardList.AddRange(cards);
        }

        public void AddCard(ICard card)
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

            ICard c = CardList[0];
            c.IsAddable = true;
            CardList.RemoveAt(0);
            WasChanged = true;
            return c;
        }

        public IList<ICard> DrawCards(int n)
        {
            List<ICard> nextCards = new List<ICard>();

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
        public ICard GetFirstCard(Predicate<ICard> pred)
        {
            foreach (ICard c in CardList)
            {
                if(pred.Invoke(c))
                {
                    CardList.RemoveAt(CardList.IndexOf(c));
                    return c;
                }
            }
            return null;
        }

        public bool InDeck(ICard card)
        {
           return CardList.Contains(card);
        }

        public void Shuffle()
        {
            List<ICard> shuffledCards = new List<ICard>();
            Random rnd = new Random();
            while (CardList.Count > 1)
            {
                int index = rnd.Next(0, CardList.Count); //pick a random item from the master list
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
            foreach (ICard c in otherCards.Cards())
            {
                ICard drawn = otherCards.DrawCard();
                this.AddCard(drawn);
            }
            this.Shuffle();
        }

        public Deck SubDeck(Predicate<ICard> pred)
        {
            List<ICard> subCards = new List<ICard>();
            foreach (ICard c in CardList)
            {
                if (pred.Invoke(c))
                {
                    subCards.Add(c);
                }
            }
            return new Deck(subCards);
        }

        public bool WasDeckChanged()
        {
            bool value = WasChanged;
            WasChanged = false;
            return value;
        }
    }
}
