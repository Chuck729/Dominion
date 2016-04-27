using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual(0, deck.CardList.Count);
            deck.AddCard(rose);
            Assert.AreEqual(1, deck.CardList.Count);
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

            var drawTwo = deck.DrawCards(2);
            Assert.AreSame(rose, drawTwo.CardList[0]);
            Assert.AreSame(hippieCamp, drawTwo.CardList[1]);

            var drawOne = deck.DrawCards(1);


            Assert.AreSame(purdue, drawOne.CardList[0]);

            Assert.AreEqual(0, deck.CardList.Count);
        }


        [TestMethod]
        public void IntegrationTestGetFirstCard()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();
            Card company = new Company();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            Assert.AreEqual(rose, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(company, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardAction));
            Assert.AreEqual(purdue, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(0, deck.CardList.Count);
        }

        public bool IsCardTreasure(ICard card)
        {
            return card.Type == CardType.Treasure;
        }

        public bool IsCardVictory(ICard card)
        {
            return card.Type == CardType.Victory;
        }

        public bool IsCardAction(ICard card)
        {
            return card.Type == CardType.Action;
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
            var deck = new Deck();
            Card rose = new Rose();

            Assert.IsFalse(deck.InDeck(rose));

            deck.AddCard(rose);

            deck.Shuffle();
            Assert.IsTrue(deck.InDeck(rose));
        }

        [TestMethod]
        public void IntegrationTestShuffleTwoCards()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);

            deck.Shuffle();

            var firstPossible = new List<ICard> {rose, hippieCamp};
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<ICard> {hippieCamp, rose};
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

            Assert.IsTrue(deck2.CardList.Count == 0);
            Assert.IsTrue(deck1.CardList.Count == 2);

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

            d3 = (Deck) d1.AppendDeck(d2);

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