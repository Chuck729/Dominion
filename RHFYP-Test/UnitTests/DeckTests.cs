﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;

namespace RHFYP_Test.UnitTests
{
    [TestClass]
    public class DeckTests
    {

        private MockRepository _mocks;

        [TestMethod]
        public void TestConstructor()
        {
            var deck = new Deck();
            Assert.IsNotNull(deck);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorGivenNull()
        {
            var deck = new Deck(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException),
            "Tried to add a null card to the deck.")]
        public void TestAddCard_NullCard_ThrowsNullArgumentException()
        {
            var deck = new Deck();
            deck.AddCard(null);
        }


        [TestMethod]
        public void TestAddCardAndCardCount()
        {
            var deck = new Deck();
            ICard card = _mocks.Stub<Rose>();

            _mocks.ReplayAll();

            Assert.AreEqual(0, deck.CardList.Count);
            deck.AddCard(card);
            Assert.AreEqual(1, deck.CardList.Count);

            _mocks.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TestDeck_NullCards_ThrowsArgumentNullException()
        {
            var iCards = new ICard[1];
            var deck = new Deck(iCards);
            deck.DrawCards(1);
        }

        [TestMethod]
        public void TestDrawCard()
        {
            var deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            ICard purdue = _mocks.Stub<Purdue>();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);

            _mocks.ReplayAll();

            Assert.AreEqual(rose, deck.DrawCard());
            Assert.AreEqual(hippieCamp, deck.DrawCard());
            Assert.AreEqual(purdue, deck.DrawCard());
            Assert.AreEqual(null, deck.DrawCard());

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestDrawCards()
        {
            var deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            ICard purdue = _mocks.Stub<Purdue>();

            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);

            _mocks.ReplayAll();

            var drawTwo = deck.DrawCards(2);
            Assert.AreSame(rose, drawTwo.CardList[0]);
            Assert.AreSame(hippieCamp, drawTwo.CardList[1]);

            var drawOne = deck.DrawCards(1);

            Assert.AreSame(purdue, drawOne.CardList[0]);

            Assert.AreEqual(0, deck.CardList.Count);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestGetFirstCard()
        {
            var deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            ICard purdue = _mocks.Stub<Purdue>();
            ICard company = _mocks.Stub<Company>();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            _mocks.ReplayAll();

            Assert.AreEqual(rose, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardAction));
            Assert.AreEqual(company, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(purdue, deck.GetFirstCard(x => x.Name == "Purdue"));
            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(0, deck.CardList.Count);

            _mocks.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "This could return the first card if it needs to be implemented.")]
        public void TestGetFirstCard_NullArgument()
        {
            var deck = new Deck();
            deck.GetFirstCard(null);
        }

        private static bool IsCardTreasure(ICard card)
        {
            return card.Type == CardType.Treasure;
        }

        private static bool IsCardVictory(ICard card)
        {
            return card.Type == CardType.Victory;
        }

        private static bool IsCardAction(ICard card)
        {
            return card.Type == CardType.Action;
        }

        [TestMethod]
        public void TestInDeck()
        {
            var deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            ICard purdue = _mocks.Stub<Purdue>();
            ICard company = _mocks.Stub<Company>();
            ICard corporation = _mocks.Stub<Corporation>();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            _mocks.ReplayAll();

            Assert.IsTrue(deck.InDeck(rose));
            Assert.IsTrue(deck.InDeck(company));
            Assert.IsFalse(deck.InDeck(corporation));
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestShuffleOneCardAndInDeck()
        {
            var deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            deck.AddCard(rose);

            _mocks.ReplayAll();

            deck.Shuffle();
            Assert.IsTrue(deck.InDeck(rose));

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestShuffleTwoCards()
        {
            var deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);

            _mocks.ReplayAll();

            deck.Shuffle();

            var firstPossible = new List<ICard> {rose, hippieCamp};
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<ICard> {hippieCamp, rose};
            var y = CompareLists(secondPossible, deck.CardList);

            var z = (x || y);
            Assert.IsTrue(z);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestShuffleThreeCards()
        {
            var deck = new Deck();
            ICard r = _mocks.Stub<Rose>();
            ICard h = _mocks.Stub<HippieCamp>();
            ICard p = _mocks.Stub<Purdue>();
            deck.AddCard(r);
            deck.AddCard(h);
            deck.AddCard(p);

            _mocks.ReplayAll();

            deck.Shuffle();

            var firstPossible = new List<ICard> {r, h, p};
            var a = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<ICard> {r, p, h};
            var b = CompareLists(secondPossible, deck.CardList);

            var thirdPossible = new List<ICard> {p, r, h};
            var c = CompareLists(thirdPossible, deck.CardList);

            var fourthPossible = new List<ICard> {p, h, r};
            var d = CompareLists(fourthPossible, deck.CardList);

            var fifthPossible = new List<ICard> {h, r, p};
            var e = CompareLists(fifthPossible, deck.CardList);

            var sixthPossible = new List<ICard> {h, p, r};
            var f = CompareLists(sixthPossible, deck.CardList);

            var g = a || b || c || d || e || f;
            Assert.IsTrue(g);

            _mocks.VerifyAll();
        }

        public bool CompareLists(List<ICard> possible, List<ICard> actual)
        {
            if (possible.Count != actual.Count)
                throw new Exception("List are not same size");
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var x = 0; x < possible.Count; x++)
            {
                if (possible[x] != actual[x])
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void TestShuffleIn()
        {
            var deck1 = new Deck();
            var deck2 = new Deck();
            ICard r1 = _mocks.Stub<Rose>();
            ICard r2 = _mocks.Stub<Rose>();

            deck1.AddCard(r1);
            deck2.AddCard(r2);

            using (_mocks.Ordered())
            {
            }
            _mocks.ReplayAll();

            deck1.ShuffleIn(deck2);

            Assert.IsTrue(deck2.CardList.Count == 0);
            Assert.IsTrue(deck1.CardList.Count == 2);

            if (deck1.CardList[0].Equals(r1))
            {
                Assert.AreEqual(deck1.CardList[1], r2);
            }
            else if (deck1.CardList[0].Equals(r2))
            {
                Assert.AreEqual(deck1.CardList[1], r1);
            }
            else
            {
                Assert.IsFalse(true);
            }
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestDrawEmptyDeck()
        {
            var d = new Deck();
            try
            {
                d.DrawCard();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestAppendDeck()
        {
            var d1 = new Deck();
            var d2 = new Deck();
            var d3 = new Deck();

            ICard r = _mocks.Stub<Rose>();
            ICard p = _mocks.Stub<Purdue>();
            ICard h = _mocks.Stub<HippieCamp>();
            ICard r2 = _mocks.Stub<Rose>();

            d1.AddCard(r);
            d2.AddCard(p);
            d2.AddCard(h);
            d3.AddCard(r2);

            _mocks.ReplayAll();

            d3 = (Deck) d1.AppendDeck(d2);

            Assert.IsTrue(d3.InDeck(r));
            Assert.IsTrue(d3.InDeck(p));
            Assert.IsTrue(d3.InDeck(h));
            Assert.IsFalse(d3.InDeck(r2));
            Assert.IsTrue(d1.InDeck(r));
            Assert.IsFalse(d1.InDeck(p));
            Assert.IsTrue(d2.InDeck(p) && d2.InDeck(h));
            Assert.IsFalse(d2.InDeck(r));

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestAppendDeck_OtherDeckNull_TreatsAsEmptyDeck()
        {
            var deck = new Deck();
            var c1 = _mocks.Stub<ICard>();
            deck.AddCard(c1);
            var newDeck = deck.AppendDeck(null);
            Assert.IsTrue(newDeck.Cards().Contains(c1));
        }

        [TestMethod]
        public void TestInsertSameCardAfterDraw()
        {
            var d1 = new Deck();
            var d2 = new Deck();

            ICard c = _mocks.Stub<Rose>();

            _mocks.ReplayAll();

            d1.AddCard(c);
            d2.AddCard(d1.DrawCard());

            Assert.AreSame(c, d2.DrawCard());
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestSubDeck()
        {
            var deck = new Deck();
            ICard action = _mocks.Stub<WallStreet>();
            ICard victory = _mocks.Stub<Rose>();
            ICard treasure = _mocks.Stub<Company>();

            _mocks.ReplayAll();

            deck.AddCard(action);
            deck.AddCard(victory);
            deck.AddCard(treasure);

            var actionCard = deck.SubDeck(IsCardAction).CardList[0];
            var victoryCard = deck.SubDeck(IsCardVictory).CardList[0];
            var treasureCard = deck.SubDeck(IsCardTreasure).CardList[0];

            Assert.AreEqual(deck.CardList[0], actionCard);

            Assert.AreEqual(deck.CardList[1], victoryCard);

            Assert.AreEqual(deck.CardList[2], treasureCard);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestDeck_DefaultCardListSet()
        {
            var defaultCardList = new List<ICard> {_mocks.Stub<ICard>()};

            var deck = new Deck(defaultCardList);
            CollectionAssert.AreEqual(deck.CardList, deck.DefaultCardList);
        }

        [TestMethod]
        public void TestDeck_CardListsNotNull()
        {
            var deck = new Deck();
            Assert.IsNotNull(deck.DefaultCardList);
            Assert.IsNotNull(deck.CardList);
        }

        [TestMethod]
        public void TestResetToDefault()
        {
            var defaultCardList = new List<ICard> { _mocks.Stub<ICard>() };

            var deck = new Deck(defaultCardList);
            var fakeCard = _mocks.Stub<ICard>();
            deck.AddCard(fakeCard);

            CollectionAssert.AreNotEqual(deck.CardList, deck.DefaultCardList);
            deck.ResetToDefault();
            CollectionAssert.AreEqual(deck.CardList, deck.DefaultCardList);
        }

        [TestMethod]
        public void TestNumberOfDelepletedNames_NoDepletedTypes()
        {
            var defaultCardList = new List<ICard> { _mocks.Stub<ICard>() };

            var deck = new Deck(defaultCardList);
            Assert.AreEqual(0, deck.NumberOfDepletedNames());
        }

        [TestMethod]
        public void TestNumberOfDelepletedTypes_OneDepletedType()
        {
            var fakeCard = _mocks.DynamicMock<Corporation>();
            fakeCard.Name = "Corperation";
            var defaultCardList = new List<ICard> { fakeCard };

            var deck = new Deck(defaultCardList);
            Assert.IsNotNull(deck.DrawCard());
            Assert.AreEqual(1, deck.NumberOfDepletedNames());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInitializeWithNullCards()
        {
            var deck = new Deck(null);
            Assert.AreEqual(0, deck.CardList.Count);
        }

        [TestMethod]
        public void TestGetFirstCardNullPred()
        {
            var deck = new Deck();
            try
            {
                deck.GetFirstCard(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestInDeckNullCard()
        {
            var deck = new Deck();
            try
            {
                deck.InDeck(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestShuffleInNullCards()
        {
            var deck = new Deck();
            try
            {
                deck.ShuffleIn(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestSubDeckNullPred()
        {
            var deck = new Deck();
            try
            {
                deck.SubDeck(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

        }

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }
    }
}