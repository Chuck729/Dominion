using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class PlayerTests
    {
        private MockRepository _mocks;

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

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
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException),
            "Must provide a valid player to sell the card to.")]
        public void TestBuyCard_NullPlayerArgument()
        {
            var game = new Game();
            game.BuyCard("", null);
        }

        [TestMethod]
        public void TestBuyCard_NullInvalidCardName_ReturnsFalse()
        {
            var game = new Game();
            Assert.IsFalse(game.BuyCard("", _mocks.Stub<IPlayer>()));
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
        public void TestPlayCard_PlayerIsNotInActionState_DoesntPlayCard()
        {
            var player = new Player("");
            var card = _mocks.Stub<ICard>();
            card.IsAddable = true;
            player.Hand.AddCard(card);

            player.PlayerState = PlayerState.Buy;
            Assert.IsFalse(player.PlayCard(card));

            player.PlayerState = PlayerState.TurnOver;
            Assert.IsFalse(player.PlayCard(card));

            player.PlayerState = PlayerState.Action;
            Assert.IsTrue(player.PlayCard(card));
        }

        [TestMethod]
        public void TestPlayCard_PlayingActionCard_CantPlayAfterTreasure()
        {
            var player = new Player("");
            var treasureCard = _mocks.Stub<ICard>();
            var actionCard = _mocks.Stub<ICard>();

            treasureCard.IsAddable = true;
            treasureCard.Type = CardType.Treasure;

            actionCard.IsAddable = true;
            actionCard.Type = CardType.Action;

            player.Hand.AddCard(treasureCard);
            player.Hand.AddCard(actionCard);

            Assert.IsTrue(player.PlayCard(treasureCard));
            Assert.IsFalse(player.PlayCard(actionCard));
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
            var card = new TestCard();
            var p = new Player("foo bar") {Gold = 3};
            Assert.IsTrue(p.CanAfford(card));

            p.Gold = 2;
            Assert.IsFalse(p.CanAfford(card));
        }

        [TestMethod]
        public void TestAddGold()
        {
            var p = new Player("bob");
            p.AddGold(3);
            Assert.AreEqual(0 + 3, p.Gold);
        }


        /// <summary>
        ///     A card class used for testing purposes
        /// </summary>
        private class TestCard : ICard
        {
            public TestCard()
            {
                CardCost = 3;
                Name = "TestCard";
                Description = "This card is used for testing purposes";
                Type = CardType.Action;
                VictoryPoints = 1;
                IsAddable = true;
            }

            public int CardCost { get; }

            public string Name { get; }

            public CardType Type { get; }

            /// <summary>
            ///     The name of the image resource that represents this card.
            /// </summary>
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public string ResourceName { get; }

            public string Description { get; }

            public int VictoryPoints { get; }

            public bool IsAddable { get; set; }

            public Point Location { get; set; }

            CardType ICard.Type
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
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

        }

        /// <summary>
        ///     A card class used for testing purposes
        /// </summary>
        private class TestCard2 : ICard
        {
            public TestCard2()
            {
                CardCost = 3;
                Name = "TestCard2";
                Description = "This card is used for testing purposes";
                Type = CardType.Treasure;
                VictoryPoints = 1;
                IsAddable = true;
            }

            public int CardCost { get; }

            public string Name { get; }

            public CardType Type { get; }

            /// <summary>
            ///     The name of the image resource that represents this card.
            /// </summary>
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public string ResourceName { get; }

            public string Description { get; }

            public int VictoryPoints { get; }

            public bool IsAddable { get; set; }

            public Point Location { get; set; }

            CardType ICard.Type
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
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

        }

        /// <summary>
        ///     A card class used for testing purposes
        /// </summary>
        private class TestCard3 : ICard
        {
            public TestCard3()
            {
                CardCost = 5;
                Name = "TestCard3";
                Description = "This card is used for testing purposes";
                Type = CardType.Treasure;
                VictoryPoints = 1;
                IsAddable = true;
            }

            public int CardCost { get; }

            public string Name { get; }

            public CardType Type { get; }

            /// <summary>
            ///     The name of the image resource that represents this card.
            /// </summary>
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public string ResourceName { get; }

            public string Description { get; }

            public int VictoryPoints { get; }

            public bool IsAddable { get; set; }

            public Point Location { get; set; }

            CardType ICard.Type
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
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

        }

        /// <summary>
        ///     A deck class used for testing purposes
        /// </summary>
        private class TestDeck : IDeck
        {

            public TestDeck()
            {
                CardList = new List<ICard>();
            }

            public List<ICard> CardList { get; set; }

            public void AddCard(ICard card)
            {
                if (card == null) return;
                if (card.IsAddable)
                {
                    CardList.Add(card);
                    card.IsAddable = false;
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


                var c = CardList[0];
                c.IsAddable = true;
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

            /// <summary>
            ///     Removes the first card that meets the given condition
            /// </summary>
            /// <param name="pred"></param>
            /// Condition that must be met
            /// <returns></returns>
            public ICard GetFirstCard(Predicate<ICard> pred)
            {
                foreach (var c in CardList.Where(pred.Invoke))
                {
                    CardList.RemoveAt(CardList.IndexOf(c));
                    return c;
                }
                return null;
            }


            public bool InDeck(ICard card)
            {
                return CardList.Contains(card);
            }

            public void Shuffle()
            {
                var shuffledCards = new List<ICard>();
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


            public Deck SubDeck(Predicate<ICard> pred)
            {
                var subCards = CardList.Where(pred.Invoke).ToList();
                return new Deck(subCards);
            }

        }
    }
}