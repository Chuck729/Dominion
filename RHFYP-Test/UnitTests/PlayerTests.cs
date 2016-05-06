using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.UnitTests
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
        [ExpectedException(typeof(ArgumentNullException),
            "Can't give the player a null card.")]
        public void TestGiveCard_CardNull()
        {
            var player = new Player("");
            player.GiveCard(null);
        }

        [TestMethod]
        public void TestGiveCard_ValidCard_PutsCardInPlayersDiscard()
        {
            var player = new Player("");
            var fakeCard = _mocks.DynamicMock<ICard>();
            var fakeDiscardDeck = _mocks.DynamicMock<IDeck>();

            fakeDiscardDeck.AddCard(fakeCard);

            _mocks.ReplayAll();

            player.DiscardPile = fakeDiscardDeck;

            Assert.IsTrue(player.GiveCard(fakeCard));
            
            _mocks.ReplayAll();
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
        [ExpectedException(typeof(InvalidOperationException),
            "Can't end actions when the player state is not set to action.")]
        public void TestEndActions_PlayerNotInActionState_ThrowsException()
        {
            var p = new Player("Test") { PlayerState = PlayerState.Buy };
            p.EndActions();
        }

        [TestMethod]
        public void TestEndTurn()
        {
            var p = new Player("Test");
            var tc1 = new TestCard();
            var tc2 = new TestCard2();

            p.Hand = new TestDeck();
            p.DiscardPile = new TestDeck();

            p.DiscardPile.AddCard(tc1);
            p.DiscardPile.AddCard(tc2);
            Assert.IsTrue(p.DiscardPile.CardList.Count == 2);
            Assert.IsTrue(p.Hand.CardList.Count == 0);

            p.PlayerState = PlayerState.Buy;
            var stateInitial = p.PlayerState;
            Assert.IsTrue(stateInitial == PlayerState.Buy);

            p.EndTurn();

            var statefinal = p.PlayerState;
            Assert.IsTrue(statefinal == PlayerState.TurnOver);

            // TODO: Adjust this test to work with discard pile transfer
            // The player draws thier cards at the end of thier turn.
            Assert.AreEqual(2, p.Hand.CardList.Count);
            Assert.IsTrue(p.DiscardPile.CardList.Count == 0);
        }

        [TestMethod]
        public void TestEndTurnDrawsCards()
        {
            var p = new Player("");
            var tc1 = new TestCard();
            var tc2 = new TestCard2();
            var tc3 = new TestCard();
            var tc4 = new TestCard();
            var tc5 = new TestCard();
            var tc6 = new TestCard();

            p.Hand = new TestDeck();
            p.DiscardPile = new TestDeck();

            p.Hand.AddCard(tc1);
            p.DiscardPile.AddCard(tc2);
            p.DiscardPile.AddCard(tc3);
            p.DiscardPile.AddCard(tc4);
            p.DiscardPile.AddCard(tc5);
            p.DiscardPile.AddCard(tc6);
            for(var x = 0; x < 5; x++)
                p.DrawPile.AddCard(new TestCard());

            Assert.IsTrue(p.DiscardPile.CardList.Count == 5);
            Assert.IsTrue(p.Hand.CardList.Count == 1);
            Assert.IsTrue(p.DrawPile.CardList.Count == 5);

            p.PlayerState = PlayerState.Buy;
            var stateInitial = p.PlayerState;
            Assert.IsTrue(stateInitial == PlayerState.Buy);

            p.EndTurn();

            var statefinal = p.PlayerState;
            Assert.IsTrue(statefinal == PlayerState.TurnOver);
            
            // The player draws thier cards at the end of thier turn.
            Assert.AreEqual(0, p.DrawPile.CardList.Count);
            Assert.AreEqual(5, p.Hand.CardList.Count);
            Assert.AreEqual(6, p.DiscardPile.CardList.Count);

            p.PlayerState = PlayerState.Buy;

            p.EndTurn();

            Assert.AreEqual(6, p.DrawPile.CardList.Count);
            Assert.AreEqual(5, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DiscardPile.CardList.Count);
        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasures()
        {
            var p = new Player("");
            var treasureCard = new TestCard2();
            var otherTreasureCard = new TestCard3();

            p.Hand = new TestDeck();

            p.Hand.AddCard(treasureCard);
            p.Hand.AddCard(otherTreasureCard);
            Assert.IsTrue(p.Hand.CardList.Count == 2);
            p.PlayerState = PlayerState.Buy;

            p.PlayAllTreasures();

            Assert.AreEqual(p.Hand.CardList.Count, 0);
        }

        [TestMethod]
        public void TestPlayAllTreasuresNoTreasures()
        {
            var p = new Player("");
            var actionCard = new TestCard();

            p.Hand = new TestDeck();

            p.Hand.AddCard(actionCard);
            Assert.IsTrue(p.Hand.CardList.Count == 1);

            p.PlayAllTreasures();

            Assert.IsTrue(p.Hand.CardList.Count == 1);
        }

        [TestMethod]
        public void TestPlayAllTreasuresTwoTreasuresOneNot()
        {
            var p = new Player("");
            var action = new TestCard();
            var treasure1 = new TestCard2();
            var treasure2 = new TestCard3();

            p.Hand = new TestDeck();

            p.Hand.AddCard(action);
            p.Hand.AddCard(treasure1);
            p.Hand.AddCard(treasure2);

            Assert.IsTrue(p.Hand.CardList.Count == 3);
            p.PlayerState = PlayerState.Buy;

            p.PlayAllTreasures();

            Assert.IsTrue(p.Hand.CardList.Count == 1);
            Assert.IsTrue(p.Hand.CardList.Contains(action));
        }

        [TestMethod]
        public void TestPlayCard()
        {
            var p = new Player("") { Managers = 1};

            var c = new TestCard();

            p.Hand = new TestDeck();
            p.DiscardPile = new TestDeck();

            p.Hand.AddCard(c);
            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DiscardPile.CardList.Count);

            p.PlayCard(c);

            Assert.AreEqual(0, p.Hand.CardList.Count);
            Assert.AreEqual(1, p.DiscardPile.CardList.Count);
        }

        [TestMethod]
        public void TestPlayCard_CardNotInPlayerHand()
        {
            var player = new Player("");
            var fakeCard = _mocks.Stub<ICard>();

            Assert.IsFalse(player.PlayCard(fakeCard));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPlayCard_NullCard()
        {
            var player = new Player("");
            player.PlayCard(null);
        }

        [TestMethod]
        public void TestPlayCard_PlayerIsNotInActionState_DoesntPlayCard()
        {
            var player = new Player("") {Managers = 1};
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

            player.PlayerState = PlayerState.Buy;

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

        [TestMethod]
        public void TestDrawCard_NoCardsToDraw_ReturnsFalse()
        {
            var p = new Player("");

            Assert.IsFalse(p.DrawCard());
        }

        [TestMethod]
        public void TestDrawCard_1CardInDrawPile()
        {
            var p = new Player("");
            var c = _mocks.Stub<ICard>();

            c.IsAddable = true;

            p.DrawPile.AddCard(c);
            Assert.AreEqual(0, p.Hand.CardList.Count);
            Assert.AreEqual(1, p.DrawPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DrawPile.CardList.Count);
        }

        [TestMethod]
        public void TestDrawCard_2CardsInDrawPile()
        {
            var p = new Player("");
            var c = _mocks.Stub<ICard>();
            var c2 = _mocks.Stub<ICard>();

            c.IsAddable = true;
            c2.IsAddable = true;

            p.DrawPile.AddCard(c);
            p.DrawPile.AddCard(c2);
            Assert.AreEqual(0, p.Hand.CardList.Count);
            Assert.AreEqual(2, p.DrawPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.AreEqual(1, p.DrawPile.CardList.Count);
        }

        [TestMethod]
        public void TestDrawCard_CardsInDrawPileAndDiscardPile()
        {
            var p = new Player("");
            var c = _mocks.Stub<ICard>();
            var c2 = _mocks.Stub<ICard>();
            var c3 = _mocks.Stub<ICard>();

            c.IsAddable = true;
            c2.IsAddable = true;
            c3.IsAddable = true;

            p.DrawPile.AddCard(c);
            p.DrawPile.AddCard(c2);
            p.DiscardPile.AddCard(c3);

            Assert.AreEqual(0, p.Hand.CardList.Count);
            Assert.AreEqual(2, p.DrawPile.CardList.Count);
            Assert.AreEqual(1, p.DiscardPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.AreEqual(1, p.DrawPile.CardList.Count);
            Assert.AreEqual(1, p.DiscardPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(2, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DrawPile.CardList.Count);
            Assert.AreEqual(1, p.DiscardPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(3, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DrawPile.CardList.Count);
            Assert.AreEqual(0, p.DiscardPile.CardList.Count);
        }
        [TestMethod]
        public void TestDrawCard_DrawPileEmpty()
        {
            var p = new Player("");
            var c = _mocks.Stub<ICard>();

            c.IsAddable = true;

            p.DiscardPile.AddCard(c);
            Assert.AreEqual(0, p.Hand.CardList.Count);
            Assert.AreEqual(1, p.DiscardPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DiscardPile.CardList.Count);
        }

        [TestMethod]
        public void TestDrawCard_TrasnfersAllOfDiscardDeckToDrawDeck()
        {
            var p = new Player("");
            var c = _mocks.Stub<ICard>();
            var c2 = _mocks.Stub<ICard>();

            c.IsAddable = true;
            c2.IsAddable = true;

            p.DiscardPile.AddCard(c);
            p.DiscardPile.AddCard(c2);
            Assert.AreEqual(0, p.Hand.CardList.Count);
            Assert.AreEqual(2, p.DiscardPile.CardList.Count);
            Assert.AreEqual(0, p.DrawPile.CardList.Count);

            Assert.IsTrue(p.DrawCard());

            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.AreEqual(0, p.DiscardPile.CardList.Count);
            Assert.AreEqual(1, p.DrawPile.CardList.Count);
        }

        [TestMethod]
        public void TestTrashCard_NoCardsInDeck_ReturnsFalse()
        {
            var p = new Player("");
            var c = _mocks.Stub<ICard>();
            Assert.IsFalse(p.TrashCard(c));
        }

        [TestMethod]
        public void TestTrashCard_CardInDeck_TrashesCard()
        {
            var p = new Player("") {TrashPile = _mocks.DynamicMock<IDeck>()};
            var c = _mocks.Stub<ICard>();
            c.IsAddable = true;
            p.DrawPile.AddCard(c);

            using (_mocks.Ordered())
            {
                p.TrashPile.AddCard(c);
            }

            _mocks.ReplayAll();

            Assert.AreEqual(1, p.DrawPile.CardList.Count);
            Assert.IsTrue(p.TrashCard(c));
            Assert.AreEqual(0, p.DrawPile.CardList.Count);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestTrashCard_CardInHand_TrashesCard()
        {
            var p = new Player("") {TrashPile = _mocks.DynamicMock<IDeck>()};
            var c = _mocks.Stub<ICard>();
            c.IsAddable = true;
            p.Hand.AddCard(c);

            using (_mocks.Ordered())
            {
                p.TrashPile.AddCard(c);
            }

            _mocks.ReplayAll();

            Assert.AreEqual(1, p.Hand.CardList.Count);
            Assert.IsTrue(p.TrashCard(c));
            Assert.AreEqual(0, p.Hand.CardList.Count);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestTrashCard_CardInDiscard_TrashesCard()
        {
            var p = new Player("") {TrashPile = _mocks.DynamicMock<IDeck>()};
            var c = _mocks.Stub<ICard>();
            c.IsAddable = true;
            p.DiscardPile.AddCard(c);

            using (_mocks.Ordered())
            {
                p.TrashPile.AddCard(c);
            }

            _mocks.ReplayAll();

            Assert.AreEqual(1, p.DiscardPile.CardList.Count);
            Assert.IsTrue(p.TrashCard(c));
            Assert.AreEqual(0, p.DiscardPile.CardList.Count);

            _mocks.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException),
            "Must supply a card to trash.")]
        public void TestTrashCard_CardNull_ThrowsException()
        {
            var p = new Player("");
            p.TrashCard(null);
        }

        [TestMethod]
        public void TestNukeCard_ValidNuke_PlayerTrashesCard()
        {
            var p = new Player("");
            var card = _mocks.Stub<ICard>();
            card.IsAddable = true;
            p.Hand.AddCard(card);
            p.Nukes = 1;
            p.PlayCard(card);

            Assert.IsFalse(p.TrashCard(card));
        }

        [TestMethod]
        public void TestNukeCard_NoCard_PlayCardReturnsFalse()
        {
            var p = new Player("");
            var card = _mocks.Stub<ICard>();
            p.Nukes = 1;

            Assert.IsFalse(p.PlayCard(card));
        }

        [TestMethod]
        public void TestPlayCard_NoCard_PlayCardReturnsFalse()
        {
            var p = new Player("") {Managers = 1};

            Assert.IsFalse(p.PlayCard(_mocks.Stub<ICard>()));
        }

        // Testing DrawCard() decision table
        // DrawCardCase.....................||..1..|..2..|..3..|
        // DrawPile has at least 1 card.....||..F..|..T..|..F..|
        // DiscardPile has at least 1 card..||..F..|..X..|..T..|
        //----------------------------------||-----|-----|-----|
        // Card drawn.......................||..F..|..T..|..T..|

        [TestMethod]
        public void TestDrawCardCase1()
        {
            var p = new Player("test");

            Assert.IsFalse(p.DrawCard());
        }

        [TestMethod]
        public void TestDrawCardCase2()
        {
            var p = new Player("test");
            var c = _mocks.Stub<Rose>();

            p.DrawPile.AddCard(c);

            Assert.IsTrue(p.DrawCard());
        }

        [TestMethod]
        public void TestDrawCardCase3()
        {
            var p = new Player("test");
            var c = _mocks.Stub<Rose>();

            p.DiscardPile.AddCard(c);

            Assert.IsTrue(p.DrawCard());
        }

        [TestMethod]
        public void TestDrawHandToDiscard()
        {
            var p = new Player("bob");
            var hand = _mocks.DynamicMock<Deck>();
            var disc = _mocks.DynamicMock<Deck>();

            var playerType = typeof(Player);
            var handField = playerType.GetProperty("Hand");

            handField.SetValue(p, hand);

            var discField = playerType.GetProperty("DiscardPile");

            discField.SetValue(p, disc);

            using (_mocks.Ordered())
            {
                Expect.Call(hand.DrawCard()).Return(new Company());
                disc.AddCard(Arg<Card>.Is.Anything);
            }

            _mocks.ReplayAll();

            p.DrawHandToDiscard();

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestHandContainsMilitaryBase()
        {
            var p = new Player("bob");
            p.Hand.CardList.Add(new MilitaryBase());

            Assert.IsTrue(p.HandContainsMilitaryBase());
        }

        [TestMethod]
        public void TestHandContainsMilitaryBaseBva()
        {
            var p = new Player("bob");
            p.Hand.CardList.Add(new MilitaryBase());
            for (var x = 0; x < 4; x++)
                p.Hand.CardList.Add(new Company());

            Assert.IsTrue(p.HandContainsMilitaryBase());

            p = new Player("bob");
            for (int x = 0; x < 4; x++)
                p.Hand.CardList.Add(new Company());
            p.Hand.CardList.Add(new MilitaryBase());

            Assert.IsTrue(p.HandContainsMilitaryBase());

            p = new Player("bob");
            p.Hand.AddCard(new Company());
            p.Hand.AddCard(new MilitaryBase());
            p.Hand.AddCard(new MilitaryBase());
            p.Hand.AddCard(new Company());
            p.Hand.AddCard(new Company());

            Assert.IsTrue(p.HandContainsMilitaryBase());

            p = new Player("bob");
            for (int x = 0; x < 5; x++)
                p.Hand.CardList.Add(new Company());

            Assert.IsFalse(p.HandContainsMilitaryBase());

        }
        [TestMethod]
        public void TestSetGame()
        {
            var p = new Player("foo");
            var g = new Game();
            p.Game = g;
            Assert.AreSame(p.Game, g);
        }

        [TestMethod]
        public void TestGetName()
        {
            var p = new Player("bar");
            Assert.AreSame("bar", p.Name);
        }


        #region Test Classes

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
                VictoryPoints = 1;
                IsAddable = true;
            }

            public int CardCost { get; set; }

            public string Name { get; set; }

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
                get { return CardType.Action; }

                set { }
            }

            /// <summary>
            /// Preforms the actions that the card is supposed to.
            /// </summary>
            /// <param name="player">The player whom played the card.</param>
            /// <param name="game">The game that is currently being played.</param>
            public void PlayCard(Player player, Game game)
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
                VictoryPoints = 1;
                IsAddable = true;
            }

            public int CardCost { get; set; }

            public string Name { get; set; }

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
                get { return CardType.Treasure; }

                set { }
            }

            /// <summary>
            /// Preforms the actions that the card is supposed to.
            /// </summary>
            /// <param name="player">The player whom played the card.</param>
            /// <param name="game">The game that is currently being played.</param>
            public void PlayCard(Player player, Game game)
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
                VictoryPoints = 1;
                IsAddable = true;
            }

            public int CardCost { get; set; }

            public string Name { get; set; }

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
                get { return CardType.Treasure; }

                set { }
            }

            /// <summary>
            /// Preforms the actions that the card is supposed to.
            /// </summary>
            /// <param name="player">The player whom played the card.</param>
            /// <param name="game">The game that is currently being played.</param>
            public void PlayCard(Player player, Game game)
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

        // TODO: Take this out and just use mocking
        /// <summary>
        ///     A deck class used for testing purposes
        /// </summary>
        private class TestDeck : IDeck
        {

            public TestDeck()
            {
                CardList = new List<ICard>();
            }

            /// <summary>
            ///     The list of cards that this deck started as.
            /// </summary>
            public List<ICard> DefaultCardList { get; set; }
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
            ///     Removes and returns the first card that meets the given condition
            /// </summary>
            /// <param name="pred">Condition that must be met.</param>
            /// <returns>The first card that meets the
            ///     <param name="pred"></param>
            /// </returns>
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
                foreach (var drawn in otherCards.Cards().Select(c => otherCards.DrawCard()))
                {
                    AddCard(drawn);
                }

                Shuffle(seed);
            }


            public Deck SubDeck(Predicate<ICard> pred)
            {
                var subCards = CardList.Where(pred.Invoke).ToList();
                return new Deck(subCards);
            }

            /// <summary>
            /// Clears the decks card list and set it to the default card list.
            /// </summary>
            public void ResetToDefault()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Sets the current list of cards as the default list of cards.
            /// </summary>
            public void SetDefaultCardList()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Returns the number of types where at least one card of that type existed
            /// in the default card list but no card of that type still remain in the card list.
            /// </summary>
            /// <returns>Number of depleted types.</returns>
            public int NumberOfDepletedNames()
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}