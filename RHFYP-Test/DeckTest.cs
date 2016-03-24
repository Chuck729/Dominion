﻿using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RHFYP.Cards;

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
            Card emptyDraw = deck.DrawCard();
            bool wasShuffled = emptyDraw == rose || emptyDraw == hippieCamp || emptyDraw == purdue; 
            Assert.IsTrue(wasShuffled);

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

            var drawTwo = new List<Card> {rose, hippieCamp};

            Assert.AreEqual(drawTwo, deck.DrawCards(2));

            var drawOne = new List<Card> {purdue};

            Assert.AreEqual(drawOne, deck.DrawCards(1));

            Assert.AreEqual("Out of cards, need to reshuffle", deck.DrawCards(4));
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

            Assert.AreEqual(rose, deck.GetFirstCard(IsCardVictory));

            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardVictory));

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

<<<<<<< HEAD
<<<<<<< HEAD
            List<Card> firstPossible = new List<Card>();
            firstPossible.Add(rose);
            firstPossible.Add(hippieCamp);
            bool x = CompareLists2(firstPossible, deck.CardList);

            List<Card> secondPossible = new List<Card>();
            secondPossible.Add(hippieCamp);
            secondPossible.Add(rose);
            bool y = CompareLists2(secondPossible, deck.CardList);

            bool z = (x || y);
=======
            var firstPossible = new List<Card> {rose, hippieCamp};
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<Card> {hippieCamp, rose};
            var y = CompareLists(secondPossible, deck.CardList);

            var z = (x && y);
>>>>>>> refs/remotes/origin/master
=======
            var firstPossible = new List<Card> {rose, hippieCamp};
            var x = CompareLists(firstPossible, deck.CardList);

            var secondPossible = new List<Card> {hippieCamp, rose};
            var y = CompareLists(secondPossible, deck.CardList);

            var z = (x && y);
>>>>>>> refs/remotes/origin/master
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

<<<<<<< HEAD
<<<<<<< HEAD
            List<Card> firstPossible = new List<Card>();
            firstPossible.Add(r);
            firstPossible.Add(h);
            firstPossible.Add(p);
            bool a = CompareLists(firstPossible, deck.CardList);

            List<Card> secondPossible = new List<Card>();
            secondPossible.Add(r);
            secondPossible.Add(p);
            secondPossible.Add(h);
            bool b = CompareLists(secondPossible, deck.CardList);

            List<Card> thirdPossible = new List<Card>();
            thirdPossible.Add(p);
            thirdPossible.Add(r);
            thirdPossible.Add(h);
            bool c = CompareLists(thirdPossible, deck.CardList);

            List<Card> fourthPossible = new List<Card>();
            fourthPossible.Add(p);
            fourthPossible.Add(h);
            fourthPossible.Add(r);
            bool d = CompareLists(fourthPossible, deck.CardList);

            List<Card> fifthPossible = new List<Card>();
            fifthPossible.Add(h);
            fifthPossible.Add(r);
            fifthPossible.Add(p);
            bool e = CompareLists(fifthPossible, deck.CardList);

            List<Card> sixthPossible = new List<Card>();
            sixthPossible.Add(h);
            sixthPossible.Add(p);
            sixthPossible.Add(r);
            bool f = CompareLists(sixthPossible, deck.CardList);

            bool g = a || b || c || d || e || f;
=======
=======
>>>>>>> refs/remotes/origin/master
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

            var g = a && b && c && d && e && f;
<<<<<<< HEAD
>>>>>>> refs/remotes/origin/master
=======
>>>>>>> refs/remotes/origin/master
            Assert.IsTrue(g);
        }

        public bool CompareLists(List<Card> possible, List<Card> actual)
        {
            bool result;
            if (possible[0] == actual[0] && possible[1] == actual[1] && possible[2] == actual[2])
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool CompareLists2(List<Card> possible, List<Card> actual)
        {
            bool result;
            if (possible[0] == actual[0] && possible[1] == actual[1])
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
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
    }
}
