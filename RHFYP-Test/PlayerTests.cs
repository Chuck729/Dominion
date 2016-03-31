using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using System.Drawing;
using Rhino.Mocks;
using System.Reflection;

namespace RHFYP_Test
{
    [TestClass]
    public class PlayerTests
    {
        private MockRepository mocks;

        [TestInitialize()]
        public void Initialize()
        {
            mocks = new MockRepository();
        }

        [TestMethod]
        public void TestBuyCard()
        {
            var p = new Player("Test");
            Deck discard = mocks.DynamicMock<Deck>();
            var t = new TestCard();
     
            // Lots of mocking stuff
            Type PlayerClass = typeof(Player);
            FieldInfo deckField = PlayerClass.GetField("DiscardPile",
                BindingFlags.NonPublic | BindingFlags.Instance);

            deckField.SetValue(p, discard);

            mocks.Replay();

            // Actual unit testing stuff
            p.Investments = 5;
            p.Gold = 8;

            var discardInitial = discard.CardCount();

            p.BuyCard(t);

            var investmentsFinal = p.Investments;
            var goldFinal = p.Gold;
            var discardFinal = discard.CardCount();
            Assert.IsTrue(4 == investmentsFinal);
            Assert.IsTrue(5 == goldFinal);
            Assert.IsTrue(discardInitial + 1 == discardFinal);

            mocks.VerifyAll();
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
            var actionCard = new TestCard();

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
    }
}
