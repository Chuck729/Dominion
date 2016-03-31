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
            var p = new Player();
            var t = new TestCard();
            var discard = new TestDeck();

            p.Investments = 5;
            p.Gold = 8;

            var investmentsInitial = p.Investments;
            var goldInitial = p.Gold;
            var discardInitial = discard.CardCount();

            p.BuyCard(t);

            var investmentsFinal = p.Investments;
            var goldFinal = p.Gold;
            var discardFinal = discard.CardCount();

            Assert.IsTrue(4 == investmentsFinal);
            Assert.IsTrue(5 == goldInitial);
            Assert.IsTrue(discardInitial + 1 == discardFinal);
        }

        [TestMethod]
        public void TestEndActions()
        {
            var p = new Player();

            var stateInitial = p.PlayerState;

            p.EndActions();

            var stateFinal = p.PlayerState;

            Assert.IsTrue(stateInitial == PlayerState.Action);
            Assert.IsTrue(stateFinal == PlayerState.Buy);
        }

        [TestMethod]
        public void TestEndTurn()
        {
            var p = new Player();
            var hand = new TestDeck();
            var discard = new TestDeck();
            var tc1 = new TestCard();
            var tc2 = new TestCard2();

            hand.AddCard(tc1);
            hand.AddCard(tc2);
            Assert.IsTrue(discard.CardCount() == 0);
            Assert.IsTrue(hand.CardCount() == 2);

            var stateInitial = p.PlayerState;
            Assert.IsTrue(stateInitial == PlayerState.Buy);

            p.EndTurn();

            var statefinal = p.PlayerState;
            Assert.IsTrue(statefinal == PlayerState.Action);

            Assert.IsTrue(discard.CardCount() == 2);
            Assert.IsTrue(hand.CardCount() == 0);

        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasures()
        {
            var p = new Player();
            var handWithTreasure = new TestDeck();
            var treasureCard = new TestCard2();
            var otherTreasureCard = new TestCard3();

            handWithTreasure.AddCard(treasureCard);
            handWithTreasure.AddCard(otherTreasureCard);
            Assert.IsTrue(handWithTreasure.CardCount() == 2);

            p.PlayAllTreasures();

            Assert.IsTrue(handWithTreasure.CardCount() == 0);
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
