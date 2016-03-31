using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using System.Drawing;

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

            p.Investments = 5;
            p.Gold = 8;

            var discardInitial = p.DiscardPile.CardCount();

            p.BuyCard(t);

            var investmentsFinal = p.Investments;
            var goldFinal = p.Gold;
            var discardFinal = p.DiscardPile.CardCount();
            Assert.IsTrue(4 == investmentsFinal);
            Assert.IsTrue(5 == goldFinal);
            Assert.IsTrue(discardInitial + 1 == discardFinal);
        }

        [TestMethod]
        public void TestEndActions()
        {
            var p = new Player("Test");

            p.PlayerState = PlayerState.Action;
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

            Assert.IsTrue(p.DiscardPile.CardCount() == 2);
            Assert.IsTrue(p.Hand.CardCount() == 0);

        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasures()
        {
            var p = new Player("Test");
            var treasureCard = new TestCard2();
            var otherTreasureCard = new TestCard3();

            p.Hand.AddCard(treasureCard);
            p.Hand.AddCard(otherTreasureCard);
            Assert.IsTrue(p.Hand.CardCount() == 2);

            p.PlayAllTreasures();

            Assert.IsTrue(p.Hand.CardCount() == 0);
        }

        [TestMethod]
        public void TestPlayAllTreasuresNoTreasures()
        {
            var p = new Player("Test");
            var handWithoutTreasure = new TestDeck();
            var actionCard = new TestCard();

            handWithoutTreasure.AddCard(actionCard);
            Assert.IsTrue(handWithoutTreasure.CardCount() == 1);

            p.PlayAllTreasures();

            Assert.IsTrue(handWithoutTreasure.CardCount() == 1);
        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasuresOneNot()
        {
            var p = new Player("Test");
            var hand = new TestDeck();
            var action = new TestCard();
            var treasure1 = new TestCard2();
            var treasure2 = new TestCard3();

            hand.AddCard(action);
            hand.AddCard(treasure1);
            hand.AddCard(treasure2);

            Assert.IsTrue(hand.CardCount() == 3);

            p.PlayAllTreasures();

            Assert.IsTrue(hand.CardCount() == 1);
        }

        [TestMethod]
        public void TestPlayCard()
        {
            var p = new Player("Test");
            var hand = new TestDeck();
            var discard = new TestDeck();
            var c = new TestCard();

            hand.AddCard(c);
            Assert.IsTrue(hand.CardCount() == 1);
            Assert.IsTrue(discard.CardCount() == 0);

            p.PlayCard(c);

            Assert.IsTrue(hand.CardCount() == 0);
            Assert.IsTrue(discard.CardCount() == 1);
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


        /// <summary>
        /// A card class used for testing purposes
        /// </summary>
        private class TestCard : ICard
        {
            public int CardCost { get; }

            public string Name { get; }

            public string Type { get; }

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

            public void PlayCard()
            {
                throw new NotImplementedException();
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

            public void PlayCard()
            {
                throw new NotImplementedException();
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

            public void PlayCard()
            {
                throw new NotImplementedException();
            }

            public bool CanAfford(Player player)
            {
                if (player.Gold >= CardCost) return true;
                else return false;
            }
        }

        /// <summary>
        /// A deck class used for testing purposes
        /// </summary>
        private class TestDeck : IDeck
        {
            public TestDeck()
            {
                this.CardList = new List<ICard>();
            }
            public List<ICard> CardList { get; set; }

            public bool WasChanged { get; set; }

            public void AddCard(ICard card)
            {
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
                throw new NotImplementedException();
            }

            public int CardCount()
            {
                return CardList.Count;
            }

            public ICollection<ICard> Cards()
            {
                throw new NotImplementedException();
            }

            public ICard DrawCard()
            {
                throw new NotImplementedException();
            }

            public IList<ICard> DrawCards(int n)
            {
                throw new NotImplementedException();
            }

            public ICard GetFirstCard(Predicate<ICard> pred)
            {
                throw new NotImplementedException();
            }

            public bool InDeck(ICard card)
            {
                throw new NotImplementedException();
            }

            public void Shuffle()
            {
                throw new NotImplementedException();
            }

            public void ShuffleIn(IDeck otherCards)
            {
                throw new NotImplementedException();
            }

            public Deck SubDeck(Predicate<ICard> pred)
            {
                throw new NotImplementedException();
            }

            public bool WasDeckChanged()
            {
                throw new NotImplementedException();
            }
        }
    }
}
