using System;
using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Rhino.Mocks;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class DeckTests
    {

        private MockRepository _mocks;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
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

            using (_mocks.Ordered())
            {

            }

            _mocks.ReplayAll();

            Assert.AreEqual(0, deck.CardCount());
            deck.AddCard(card);
            Assert.AreEqual(1, deck.CardCount());

            _mocks.VerifyAll();
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

            using (_mocks.Ordered())
            {

            }

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

            using (_mocks.Ordered())
            {

            }

            _mocks.ReplayAll();

            IDeck drawTwo = deck.DrawCards(2);
            Assert.AreSame(rose, drawTwo.CardList[0]);
            Assert.AreSame(hippieCamp, drawTwo.CardList[1]);

            IDeck drawOne = deck.DrawCards(1);


            Assert.AreSame(purdue, drawOne.CardList[0]);

            Assert.AreEqual(0, deck.CardCount());

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestGetFirstCard()
        {
            Deck deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            ICard purdue = _mocks.Stub<Purdue>();
            ICard company = _mocks.Stub<Company>();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            Assert.AreEqual(rose, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardAction));
            Assert.AreEqual(company, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(purdue, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardCurse));
            Assert.AreEqual(0, deck.CardCount());

            _mocks.VerifyAll();

        }

        public bool IsCardTreasure(ICard card)
        {
            return card.Type == "treasure";
        }

        public bool IsCardVictory(ICard card)
        {
            return card.Type == "victory";
        }

        public bool IsCardAction(ICard card)
        {
            return card.Type == "action";
        }

        public bool IsCardCurse(ICard card)
        {
            return card.Type == "curse";
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

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            Assert.IsTrue(deck.InDeck(rose));
            Assert.IsTrue(deck.InDeck(company));
            Assert.IsFalse(deck.InDeck(corporation));
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestShuffleOneCardAndInDeck()
        {
            Deck deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            deck.AddCard(rose);

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            deck.Shuffle();
            Assert.IsTrue(deck.InDeck(rose));

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestShuffleTwoCards()
        {
            Deck deck = new Deck();
            ICard rose = _mocks.Stub<Rose>();
            ICard hippieCamp = _mocks.Stub<HippieCamp>();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            deck.Shuffle();

            var firstPossible = new List<ICard> { rose, hippieCamp };
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<ICard> { hippieCamp, rose };
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

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            deck.Shuffle();

            var firstPossible = new List<ICard> { r, h, p };
            var a = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<ICard> { r, p, h };
            var b = CompareLists(secondPossible, deck.CardList);

            var thirdPossible = new List<ICard> { p, r, h };
            var c = CompareLists(thirdPossible, deck.CardList);

            var fourthPossible = new List<ICard> { p, h, r };
            var d = CompareLists(fourthPossible, deck.CardList);

            var fifthPossible = new List<ICard> { h, r, p };
            var e = CompareLists(fifthPossible, deck.CardList);

            var sixthPossible = new List<ICard> { h, p, r };
            var f = CompareLists(sixthPossible, deck.CardList);

            bool g = a || b || c || d || e || f;
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

            Assert.IsTrue(deck2.CardCount() == 0);
            Assert.IsTrue(deck1.CardCount() == 2);

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

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            d3 = (Deck)d1.AppendDeck(d2);

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
        public void TestInsertSameCardToDecks()
        {
            var d1 = new Deck();
            var d2 = new Deck();

            ICard c = _mocks.Stub<Rose>();

            var passes = false;

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            d1.AddCard(c);
            try
            {
                d2.AddCard(c);
            }
            catch (Exception)
            {
                passes = true;
            }

            Assert.IsTrue(passes);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestInsertSameCardAfterDraw()
        {
            var d1 = new Deck();
            var d2 = new Deck();

            ICard c = _mocks.Stub<Rose>();

            using (_mocks.Ordered())
            {

            }
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

            using (_mocks.Ordered())
            {

            }
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

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

    }
}
