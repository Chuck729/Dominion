using System;
using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RHFYP_Test
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void TestAddCardAndCardCount()
        {
            Deck deck = new Deck();
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
            Assert.AreEqual("Out of cards, need to reshuffle", deck.DrawCard());

        }

        [TestMethod]
        public void TestDrawCards()
        {
            Deck deck = new Deck();
            Card rose = new Rose();
            Card hippieCamp = new HippieCamp();
            Card purdue = new Purdue();

            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);

            List<Card> drawTwo = new List<Card>();
            drawTwo.Add(rose);
            drawTwo.Add(hippieCamp);

            Assert.AreEqual(drawTwo, deck.DrawCards(2));

            List<Card> drawOne = new List<Card>();
            drawOne.Add(purdue);

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
            Deck deck = new Deck();
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

            List<Card> firstPossible = new List<Card>();
            firstPossible.Add(rose);
            firstPossible.Add(hippieCamp);
            bool x;
            if (firstPossible[0] == deck.CardList[0] && firstPossible[1] == deck.CardList[1])
            {
                x = true;
            } else
            {
                x = false;
            }

            List<Card> secondPossible = new List<Card>();
            secondPossible.Add(hippieCamp);
            secondPossible.Add(rose);
            bool y;
            if (secondPossible[0] == deck.CardList[0] && secondPossible[1] == deck.CardList[1])
            {
                y = true;
            }
            else
            {
                y = false;
            }
            bool z = (x && y);
            Assert.IsTrue(z);
        }

        [TestMethod]
        public void TestShuffleThreeCards()
        {
            Deck deck = new Deck();
            Card r = new Rose();
            Card h = new HippieCamp();
            Card p = new Purdue();
            deck.AddCard(r);
            deck.AddCard(h);
            deck.AddCard(p);

            deck.Shuffle();

            List<Card> firstPossible = new List<Card>();
            firstPossible.Add(r);
            firstPossible.Add(h);
            firstPossible.Add(p);
            bool a;
            if (firstPossible[0] == deck.CardList[0] && firstPossible[1] == deck.CardList[1] && firstPossible[2] == deck.CardList[2])
            {
                a = true;
            }
            else
            {
                a = false;
            }

            List<Card> secondPossible = new List<Card>();
            secondPossible.Add(r);
            secondPossible.Add(p);
            secondPossible.Add(h);
            bool b;
            if (secondPossible[0] == deck.CardList[0] && secondPossible[1] == deck.CardList[1] && secondPossible[2] == deck.CardList[2])
            {
                b = true;
            }
            else
            {
                b = false;
            }

            List<Card> thirdPossible = new List<Card>();
            thirdPossible.Add(p);
            thirdPossible.Add(r);
            thirdPossible.Add(h);
            bool c;
            if (thirdPossible[0] == deck.CardList[0] && thirdPossible[1] == deck.CardList[1] && thirdPossible[2] == deck.CardList[2])
            {
                c = true;
            }
            else
            {
                c = false;
            }

            List<Card> fourthPossible = new List<Card>();
            fourthPossible.Add(p);
            fourthPossible.Add(h);
            fourthPossible.Add(r);
            bool d;
            if (fourthPossible[0] == deck.CardList[0] && fourthPossible[1] == deck.CardList[1] && fourthPossible[2] == deck.CardList[2])
            {
                d = true;
            }
            else
            {
                d = false;
            }

            List<Card> fifthPossible = new List<Card>();
            fifthPossible.Add(h);
            fifthPossible.Add(r);
            fifthPossible.Add(p);
            bool e;
            if (fifthPossible[0] == deck.CardList[0] && fifthPossible[1] == deck.CardList[1] && fifthPossible[2] == deck.CardList[2])
            {
                e = true;
            }
            else
            {
                e = false;
            }

            List<Card> sixthPossible = new List<Card>();
            sixthPossible.Add(h);
            sixthPossible.Add(r);
            sixthPossible.Add(p);
            bool f;
            if (sixthPossible[0] == deck.CardList[0] && sixthPossible[1] == deck.CardList[1] && sixthPossible[2] == deck.CardList[2])
            {
                f = true;
            }
            else
            {
                f = false;
            }

            bool g = a && b && c && d && e && f;
            Assert.IsTrue(g);
        }
    }
}
