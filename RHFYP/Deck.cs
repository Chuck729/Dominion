﻿using System;
using System.Collections.Generic;
using System.Linq;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace RHFYP
{
    public class Deck : IDeck
    {

        public Deck()
        {
            CardList = new List<ICard>();
            SetDefaultCardList();
        }

        public Deck(IEnumerable<ICard> defaultCards)
        {
            CardList = new List<ICard>();

            if (defaultCards == null)
            {
                throw new ArgumentException("List of cards was null");
            }
            var cardsArray = defaultCards as ICard[] ?? defaultCards.ToArray();
           

            if (cardsArray.Any(card => card == null))
            {
                throw new ArgumentNullException(nameof(defaultCards));
            }
            CardList.AddRange(cardsArray);
            SetDefaultCardList();
        }

        /// <summary>
        ///     The list of cards that this deck started as.
        /// </summary>
        public List<ICard> DefaultCardList { get; set; }
        public List<ICard> CardList { get; set; }

        public virtual void AddCard(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            CardList.Add(card);
        }

        public IDeck AppendDeck(IDeck deck)
        {
            return deck == null ? new Deck(Cards()) : new Deck(Cards().Concat(deck.Cards()));
        }

        public ICollection<ICard> Cards()
        {
            return CardList;
        }

        public virtual ICard DrawCard()
        {
            if (CardList.Count == 0)
            {
                return null;
            }

            var c = CardList[0];
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

        public virtual ICard GetFirstCard(Predicate<ICard> pred)
        {
            if (pred == null) throw new ArgumentNullException(nameof(pred));

            var c = CardList.Find(pred);
            if (c == null)
                return null;
            CardList.RemoveAt(CardList.IndexOf(c));
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

        public void ResetToDefault()
        {
            foreach (var card in DefaultCardList.Concat(CardList))
            {
            }
            CardList.Clear();
            CardList.AddRange(DefaultCardList);
        }

        /// <summary>
        /// Sets the current list of cards as the default list of cards.
        /// </summary>
        public void SetDefaultCardList()
        {
            DefaultCardList = new List<ICard>();
            DefaultCardList.AddRange(CardList);
        }

        /// <summary>
        /// Returns the number of types where at least one card of that type existed
        /// in the default card list but no card of that type still remain in the card list.
        /// </summary>
        /// <returns>Number of depleted types.</returns>
        public int NumberOfDepletedNames()
        {
            var uniqueNames = new List<string>();
            var count = 0;
            foreach (var card in DefaultCardList.Where(card => !uniqueNames.Exists(x => x == card.Name)))
            {
                uniqueNames.Add(card.Name);
                if (!CardList.Exists(x => x.Name == card.Name))
                    count++;
            }
            return count;
        }
    }
}