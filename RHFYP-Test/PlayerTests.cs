using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using System.Drawing;
using System.Linq;

namespace RHFYP_Test
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestBuyCard()
        {
            var p = new Player("Test");
            var t = new TestCard();

            p.DiscardPile = new TestDeck();

            // Actual unit testing stuff
            p.Investments = 5;
            p.Gold = 8;

            var discardInitial = p.DiscardPile.CardCount();

            Assert.IsFalse(p.DiscardPile.InDeck(t));

            p.BuyCard(t);

            var investmentsFinal = p.Investments;
            var goldFinal = p.Gold;
            var discardFinal = p.DiscardPile.CardCount();
            Assert.IsTrue(4 == investmentsFinal);
            Assert.IsTrue(5 == goldFinal);
            Assert.AreEqual(discardInitial + 1, discardFinal);

            Assert.IsTrue(p.DiscardPile.InDeck(t));

            //mocks.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Must provide a valid player to sell the card to.")]
        public void TestBuyCard_NullPlayerArgument()
        {
            var game = new Game();
            game.BuyCard("", null);
        }

        [TestMethod]
        public void TestEndActions()
        {
            var p = new Player("Test") {PlayerState = PlayerState.Action};

            var stateInitial = p.PlayerState;

            p.EndActions();

            var stateFinal = p.PlayerState;

            Assert.IsTrue(stateInitial == PlayerState.Action);
            Assert.IsTrue(stateFinal == PlayerState.Buy);
        }

        [TestMethod]
        public void TestEndTurn()
        {
            var p = new Player("Test");
            var tc1 = new TestCard();
            var tc2 = new TestCard2();

            p.Hand = new TestDeck();
            p.DiscardPile = new TestDeck();

            p.Hand.AddCard(tc1);
            p.Hand.AddCard(tc2);
            Assert.IsTrue(p.DiscardPile.CardCount() == 0);
            Assert.IsTrue(p.Hand.CardCount() == 2);

            p.PlayerState = PlayerState.Buy;
            var stateInitial = p.PlayerState;
            Assert.IsTrue(stateInitial == PlayerState.Buy);

            p.EndTurn();

            var statefinal = p.PlayerState;
            Assert.IsTrue(statefinal == PlayerState.TurnOver);

            // TODO: Adjust this test to work with discard pile transfer
            // The player draws thier cards at the end of thier turn.
            Assert.AreEqual(0, p.Hand.CardCount());
            Assert.IsTrue(p.DiscardPile.CardCount() == 2);

        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasures()
        {

            var p = new Player("Test");
            var treasureCard = new TestCard2();
            var otherTreasureCard = new TestCard3();

            p.Hand = new TestDeck();

            p.Hand.AddCard(treasureCard);
            p.Hand.AddCard(otherTreasureCard);
            Assert.IsTrue(p.Hand.CardCount() == 2);

            p.PlayAllTreasures();

            Assert.AreEqual(p.Hand.CardCount(), 0);
        }

        [TestMethod]
        public void TestPlayAllTreasuresNoTreasures()
        {
            var p = new Player("Test");
            var actionCard = new TestCard();

            p.Hand = new TestDeck();

            p.Hand.AddCard(actionCard);
            Assert.IsTrue(p.Hand.CardCount() == 1);

            p.PlayAllTreasures();

            Assert.IsTrue(p.Hand.CardCount() == 1);
        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasuresOneNot()
        {
            var p = new Player("Test");
            var action = new TestCard();
            var treasure1 = new TestCard2();
            var treasure2 = new TestCard3();

            p.Hand = new TestDeck();

            p.Hand.AddCard(action);
            p.Hand.AddCard(treasure1);
            p.Hand.AddCard(treasure2);

            Assert.IsTrue(p.Hand.CardCount() == 3);

            p.PlayAllTreasures();

            Assert.IsTrue(p.Hand.CardCount() == 1);
            Assert.IsTrue(p.Hand.CardList.Contains(action));
        }

        [TestMethod]
        public void TestPlayCard()
        {
            var p = new Player("Test");
            var c = new TestCard();

            p.Hand = new TestDeck();
            p.DiscardPile = new TestDeck();

            p.Hand.AddCard(c);
            Assert.IsTrue(p.Hand.CardCount() == 1);
            Assert.IsTrue(p.DiscardPile.CardCount() == 0);

            p.PlayCard(c);

            Assert.IsTrue(p.Hand.CardCount() == 0);
            Assert.IsTrue(p.DiscardPile.CardCount() == 1);
        }

        [TestMethod]
        public void TestStartTurn()
        {
            var p = new Player("Test");

            p.StartTurn();

            Assert.IsTrue(p.PlayerState == PlayerState.Action);
            Assert.IsTrue(p.Investments == 1);
            Assert.IsTrue(p.Gold == 0);
            Assert.IsTrue(p.Managers == 1);

        }

        [TestMethod]
        public void TestCanAfford()
        {
            TestCard card = new TestCard();
            Player p = new Player("foo bar");
            p.Gold = 3;
            Assert.IsTrue(p.CanAfford(card));

            p.Gold = 2;
            Assert.IsFalse(p.CanAfford(card));
        }

        [TestMethod]
        public void TestAddGold()
        {
            Player p = new Player("bob");
            p.AddGold(3);
            Assert.AreEqual(0 + 3, p.Gold);
        }




        /// <summary>
        /// A card class used for testing purposes
        /// </summary>
        private class TestCard : ICard
        {
            public int CardCost { get; }

            public string Name { get; }

            public string Type { get; }

            /// <summary>
            /// The name of the image resource that represents this card.
            /// </summary>
            public string ResourceName { get; }

            public string Description { get; }

            public int VictoryPoints { get; }

            public bool IsAddable { get; set; }

            public Point Location { get; set; }
            public TestCard()
            {
                CardCost = 3;
                Name = "TestCard";
                Description = "This card is used for testing purposes";
                Type = "action";
                VictoryPoints = 1;
                IsAddable = true;
            }

            public void PlayCard(Player player)
            {
               
            }

            /// <summary>
            ///     Factory pattern for card objects.
            /// </summary>
            /// <returns>A new card object.</returns>
            public ICard CreateCard()
            {
                return new TestCard();
            }

            public bool CanAfford(Player player)
            {
                if (player.Gold >= CardCost) return true;
                else return false;
            }
        }

        /// <summary>
        /// A card class used for testing purposes
        /// </summary>
        private class TestCard2 : ICard
        {
            public int CardCost { get; }

            public string Name { get; }

            public string Type { get; }

            /// <summary>
            /// The name of the image resource that represents this card.
            /// </summary>
            public string ResourceName { get; }

            public string Description { get; }

            public int VictoryPoints { get; }

            public bool IsAddable { get; set; }

            public Point Location { get; set; }
            public TestCard2()
            {
                CardCost = 3;
                Name = "TestCard2";
                Description = "This card is used for testing purposes";
                Type = "treasure";
                VictoryPoints = 1;
                IsAddable = true;
            }

            public void PlayCard(Player player)
            {

            }

            /// <summary>
            ///     Factory pattern for card objects.
            /// </summary>
            /// <returns>A new card object.</returns>
            public ICard CreateCard()
            {
                return new TestCard2();
            }

            public bool CanAfford(Player player)
            {
                if (player.Gold >= CardCost) return true;
                else return false;
            }
        }

        /// <summary>
        /// A card class used for testing purposes
        /// </summary>
        private class TestCard3 : ICard
        {
            public int CardCost { get; }

            public string Name { get; }

            public string Type { get; }

            /// <summary>
            /// The name of the image resource that represents this card.
            /// </summary>
            public string ResourceName { get; }

            public string Description { get; }

            public int VictoryPoints { get; }

            public bool IsAddable { get; set; }

            public Point Location { get; set; }
            public TestCard3()
            {
                CardCost = 5;
                Name = "TestCard3";
                Description = "This card is used for testing purposes";
                Type = "treasure";
                VictoryPoints = 1;
                IsAddable = true;
            }

            public void PlayCard(Player player)
            {

            }

            /// <summary>
            ///     Factory pattern for card objects.
            /// </summary>
            /// <returns>A new card object.</returns>
            public ICard CreateCard()
            {
                return new TestCard3();
            }

            public bool CanAfford(IPlayer player)
            {
                return player.Gold >= CardCost;
            }
        }

        /// <summary>
        /// A deck class used for testing purposes
        /// </summary>
        private class TestDeck : IDeck
        {
            // TODO: Need a WasDeckChanged() method
            // TODO: Need a List<ICard> LookAtDeck() method

            public List<ICard> CardList { get; set; }
            private bool WasChanged { get; set; }

            public TestDeck()
            {

                this.CardList = new List<ICard>();
            }

            public TestDeck(IEnumerable<ICard> cards)
            {
                CardList = new List<ICard>();
                if (cards != null)
                    CardList.AddRange(cards);
            }

            public void AddCard(ICard card)
            {
                if (card == null) return;
                if (card.IsAddable)
                {
                    CardList.Add(card);
                    card.IsAddable = false;
                    WasChanged = true;
                }
                else
                {
                    throw new Exception("Card is not addable");
                }
            }

            public IDeck AppendDeck(IDeck deck)
            {
                IDeck newDeck = new Deck(Cards().Concat(deck.Cards()));
                return newDeck;
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
                if (CardList.Count == 0)
                {
                    return null; //TODO needs to shuffle in discard deck but this handles the error for now
                }


                ICard c = CardList[0];
                c.IsAddable = true;
                CardList.RemoveAt(0);
                WasChanged = true;
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

                foreach (ICard c in CardList)
                {
                    if (pred.Invoke(c))
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

                foreach (ICard c in otherCards.Cards())
                {

                    ICard drawn = otherCards.DrawCard();
                    this.AddCard(drawn);
                }

                Shuffle();
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
                var value = WasChanged;
                WasChanged = false;
                return value;
            }
        }

    }
}
