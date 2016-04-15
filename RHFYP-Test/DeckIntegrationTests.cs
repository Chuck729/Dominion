using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class DeckIntegrationTests
    {

        [TestMethod]
        public void IntegrationTestAddCardAndCardCount()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Assert.AreEqual(0, deck.CardCount());
            deck.AddCard(rose);
            Assert.AreEqual(1, deck.CardCount());
        }

        [TestMethod]
        public void IntegrationTestDrawCard()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);

            Assert.AreEqual(rose, deck.DrawCard());
            Assert.AreEqual(hippieCamp, deck.DrawCard());
            Assert.AreEqual(purdue, deck.DrawCard());
            Assert.AreEqual(null, deck.DrawCard());

        }

        [TestMethod]
        public void IntegrationTestDrawCards()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();

            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);

            IDeck drawTwo = deck.DrawCards(2);
            Assert.AreSame(rose, drawTwo.CardList[0]);
            Assert.AreSame(hippieCamp, drawTwo.CardList[1]);

            IDeck drawOne = deck.DrawCards(1);


            Assert.AreSame(purdue, drawOne.CardList[0]);

            Assert.AreEqual(0, deck.CardCount());

        }


        [TestMethod]
        public void IntegrationTestGetFirstCard()
        {
            Deck deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();
            Card company = new Company();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            Assert.AreEqual(purdue, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(rose, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(company, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardAction));
            Assert.AreEqual(0, deck.CardCount());
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

        [TestMethod]
        public void IntegrationTestInDeck()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();
            Card company = new Company();
            Card corporation = new Corporation();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            Assert.IsTrue(deck.InDeck(rose));
            Assert.IsTrue(deck.InDeck(company));
            Assert.IsFalse(deck.InDeck(corporation));
        }


        [TestMethod]
        public void IntegrationTestShuffleOneCardAndInDeck()
        {
            Deck deck = new Deck();
            Card rose = new Rose();

            Assert.IsFalse(deck.InDeck(rose));

            deck.AddCard(rose);

            deck.Shuffle();
            Assert.IsTrue(deck.InDeck(rose));
        }

        [TestMethod]
        public void IntegrationTestShuffleTwoCards()
        {
            Deck deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);

            deck.Shuffle();

            var firstPossible = new List<ICard> { rose, hippieCamp };
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<ICard> { hippieCamp, rose };
            var y = CompareLists(secondPossible, deck.CardList);

            var z = (x || y);
            Assert.IsTrue(z);
        }

        [TestMethod]
        public void IntegrationTestShuffleThreeCards()
        {
            var deck = new Deck();
            Card r = new Rose();
            Card h = new HippieCamp();
            Card p = new Purdue();
            deck.AddCard(r);
            deck.AddCard(h);
            deck.AddCard(p);

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
        public void IntegrationTestShuffleIn()
        {
            var deck1 = new Deck();
            var deck2 = new Deck();
            Card r = new Rose();
            Card h = new HippieCamp();

            deck1.AddCard(r);
            deck2.AddCard(h);

            deck1.ShuffleIn(deck2);

            Assert.IsTrue(deck2.CardCount() == 0);
            Assert.IsTrue(deck1.CardCount() == 2);

            if (deck1.CardList[0].Equals(r))
            {
                Assert.AreEqual(deck1.CardList[1], h);
            }
            else if (deck1.CardList[0].Equals(h))
            {
                Assert.AreEqual(deck1.CardList[1], r);
            }
            else
            {
                Assert.IsFalse(true);
            }


        }

        [TestMethod]
        public void IntegrationTestDrawEmptyDeck()
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
        public void IntegrationTestAppendDeck()
        {
            var d1 = new Deck();
            var d2 = new Deck();
            var d3 = new Deck();

            Card r = new Rose();
            Card p = new Purdue();
            Card h = new HippieCamp();
            Card c = new Company();

            d1.AddCard(r);
            d2.AddCard(p);
            d2.AddCard(h);
            d3.AddCard(c);

            d3 = (Deck)d1.AppendDeck(d2);

            Assert.IsTrue(d3.InDeck(r));
            Assert.IsTrue(d3.InDeck(p));
            Assert.IsTrue(d3.InDeck(h));
            Assert.IsFalse(d3.InDeck(c));
            Assert.IsTrue(d1.InDeck(r));
            Assert.IsFalse(d1.InDeck(p));
            Assert.IsTrue(d2.InDeck(p) && d2.InDeck(h));
            Assert.IsFalse(d2.InDeck(r));
        }

        [TestMethod]
        public void IntegrationTestInsertSameCardToDecks()
        {
            var d1 = new Deck();
            var d2 = new Deck();

            Card c = new HomelessGuy();

            var passes = false;

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
        }

        [TestMethod]
        public void IntegrationTestInsertSameCardAfterDraw()
        {
            var d1 = new Deck();
            var d2 = new Deck();

            Card c = new SmallBusiness();

            d1.AddCard(c);
            d2.AddCard(d1.DrawCard());

            Assert.AreSame(c, d2.DrawCard());
        }

        [TestMethod]
        public void IntegrationTestSubDeck()
        {
            var deck = new Deck();
            Card action = new WallStreet();
            Card victory = new Mit();
            Card treasure = new SmallBusiness();

            deck.AddCard(action);
            deck.AddCard(victory);
            deck.AddCard(treasure);

            var actionCard = deck.SubDeck(IsCardAction).CardList[0];
            var victoryCard = deck.SubDeck(IsCardVictory).CardList[0];
            var treasureCard = deck.SubDeck(IsCardTreasure).CardList[0];

            Assert.AreEqual(deck.CardList[0], actionCard);

            Assert.AreEqual(deck.CardList[1], victoryCard);

            Assert.AreEqual(deck.CardList[2], treasureCard);
        }

    }
}
