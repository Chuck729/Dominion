using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RHFYP.Cards;
using System;

namespace RHFYP_Test
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void TestAddCardAndCardCount()
        {
            var deck = new Deck();
            Card card = new Rose();
            Assert.AreEqual(0, deck.CardCount());
            deck.AddCard(card);
            Assert.AreEqual(1, deck.CardCount());
        }

        [TestMethod]
        public void TestDrawCard()
        {
            Deck deck = new Deck();
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
        public void TestDrawCards()
        {
            var deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();

            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);

            IList<Card> drawTwo = deck.DrawCards(2);
            Assert.AreEqual("Rose", drawTwo[0].Name);
            Assert.AreEqual("Hippie Camp", drawTwo[1].Name);

            IList<Card> drawOne = deck.DrawCards(1);


            Assert.AreEqual("Purdue", drawOne[0].Name);

            Assert.AreEqual(0, deck.CardCount());

            Assert.AreEqual(null, deck.DrawCards(1)[0]);
        }

        [TestMethod]
        public void TestGetFirstCard()
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

            Assert.AreEqual(company, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(rose, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardAction));

        }

        public bool IsCardTreasure(Card card)
        {
            return card.Type == "treasure";
        }

        public bool IsCardVictory(Card card)
        {
            return card.Type == "victory";
        }

        public bool IsCardAction(Card card)
        {
            return card.Type == "action";
        }

        [TestMethod]
        public void TestInDeck()
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
        public void TestShuffleOneCardAndInDeck()
        {
            Deck deck = new Deck();
            Card rose = new Rose();
            deck.AddCard(rose);

            deck.Shuffle();
            Assert.IsTrue(deck.InDeck(rose));
        }

        [TestMethod]
        public void TestShuffleTwoCards()
        {
            Deck deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);

            deck.Shuffle();

            var firstPossible = new List<Card> {rose, hippieCamp};
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<Card> {hippieCamp, rose};
            var y = CompareLists(secondPossible, deck.CardList);

            bool z = (x || y);
            Assert.IsTrue(z);
        }

        [TestMethod]
        public void TestShuffleThreeCards()
        {
            var deck = new Deck();
            Card r = new Rose();
            Card h = new HippieCamp();
            Card p = new Purdue();
            deck.AddCard(r);
            deck.AddCard(h);
            deck.AddCard(p);

            deck.Shuffle();

            var firstPossible = new List<Card> {r, h, p};
            var a = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<Card> {r, p, h};
            var b = CompareLists(secondPossible, deck.CardList);

            var thirdPossible = new List<Card> {p, r, h};
            var c = CompareLists(thirdPossible, deck.CardList);

            var fourthPossible = new List<Card> {p, h, r};
            var d = CompareLists(fourthPossible, deck.CardList);

            var fifthPossible = new List<Card> {h, r, p};
            var e = CompareLists(fifthPossible, deck.CardList);

            var sixthPossible = new List<Card> {h, p, r};
            var f = CompareLists(sixthPossible, deck.CardList);

            bool g = a || b || c || d || e || f;
            Assert.IsTrue(g);
        }

        public bool CompareLists(List<Card> possible, List<Card> actual)
        {
            if (possible.Count != actual.Count)
                throw new Exception("List are not same size");
           for(int x = 0; x < possible.Count; x++)
            {
                if (possible[x] != actual[x])
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void TestShuffleIn()
        {
            var deck = new Deck();
        }

        [TestMethod]
        public void TestDrawEmptyDeck()
        {
            Deck d = new Deck();
            try
            {
                d.DrawCard();
                Assert.IsTrue(true);
            } catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestWasDeckChanged()
        {
            Deck deck = new Deck();
            Assert.IsFalse(deck.WasDeckChanged());
            deck.AddCard(new Rose());
            Assert.IsTrue(deck.WasDeckChanged());
            deck.DrawCard();
            Assert.IsTrue(deck.WasDeckChanged());

            //TODO add function that sets deck changed variable to false after it 
            //uses the information that the deck was changed
        }

        [TestMethod]
        public void TestAppendDeck()
        {
            Deck d1 = new Deck();
            Deck d2 = new Deck();
            IDeck d3 = new Deck();

            Card r = new Rose();
            Card p = new Purdue();
            Card h = new HippieCamp();
            Card r2 = new Rose();

            d1.AddCard(r);
            d2.AddCard(p);
            d2.AddCard(h);
            d3.AddCard(r2);

            d3 = d1.AppendDeck(d2);

            Assert.IsTrue(d3.InDeck(r));
            Assert.IsTrue(d3.InDeck(p));
            Assert.IsTrue(d3.InDeck(h));
            Assert.IsFalse(d3.InDeck(r2));
            Assert.IsTrue(d1.InDeck(r));
            Assert.IsFalse(d1.InDeck(p));
            Assert.IsTrue(d2.InDeck(p) && d2.InDeck(h));
            Assert.IsFalse(d2.InDeck(r));
        }

        [TestMethod]
        public void TestInsertSameCardToDecks()
        {
            Deck d1 = new Deck();
            Deck d2 = new Deck();

            Card c = new Rose();

            bool passes = false;

            d1.AddCard(c);
            try
            {
                d2.AddCard(c);
            } catch (Exception e)
            {
                passes = true;
            }

            Assert.IsTrue(passes);
        }
    }
}
