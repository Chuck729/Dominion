using System;
using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using System.Drawing;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void TestAddCardAndCardCount()
        {
            var deck = new Deck();
            TestCard card = new TestCard();
            Assert.AreEqual(0, deck.CardCount());
            deck.AddCard(card);
            Assert.AreEqual(1, deck.CardCount());
        }

        [TestMethod]
        public void TestDrawCard()
        {
            Deck deck = new Deck();
            TestCard rose = new TestCard();
            TestCard hippieCamp = new TestCard();
            TestCard purdue = new TestCard();
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
            TestCard rose = new TestCard();
            TestCard hippieCamp = new TestCard();
            TestCard purdue = new TestCard();

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
        public void TestGetFirstCard()
        {
            Deck deck = new Deck();
            TestCard rose = new TestCard();
            TestCard hippieCamp = new TestCard();
            TestCard2 purdue = new TestCard2();
            TestCard2 company = new TestCard2();
            deck.AddCard(rose);
            deck.AddCard(hippieCamp);
            deck.AddCard(purdue);
            deck.AddCard(company);

            Assert.AreEqual(purdue, deck.GetFirstCard(IsCardTreasure));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardVictory));
            Assert.AreEqual(rose, deck.GetFirstCard(IsCardAction));
            Assert.AreEqual(hippieCamp, deck.GetFirstCard(IsCardAction));
            Assert.AreEqual(null, deck.GetFirstCard(IsCardAction));

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
        public void TestInDeck()
        {
            var deck = new Deck();
            TestCard rose = new TestCard();
            TestCard hippieCamp = new TestCard();
            TestCard purdue = new TestCard();
            TestCard company = new TestCard();
            TestCard corporation = new TestCard();
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
            TestCard rose = new TestCard();
            deck.AddCard(rose);

            deck.Shuffle();
            Assert.IsTrue(deck.InDeck(rose));
        }

        [TestMethod]
        public void TestShuffleTwoCards()
        {
            Deck deck = new Deck();
            TestCard rose = new TestCard();
            TestCard hippieCamp = new TestCard();
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
        public void TestShuffleThreeCards()
        {
            var deck = new Deck();
            TestCard r = new TestCard();
            TestCard h = new TestCard();
            TestCard p = new TestCard();
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

            bool g = a || b || c || d || e || f;
            Assert.IsTrue(g);
        }

        public bool CompareLists(List<ICard> possible, List<ICard> actual)
        {
            if (possible.Count != actual.Count)
                throw new Exception("List are not same size");
            // ReSharper disable once LoopCanBeConvertedToQuery
            for(var x = 0; x < possible.Count; x++)
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
            TestCard r1 = new TestCard();
            TestCard r2 = new TestCard();

            deck1.AddCard(r1);
            deck2.AddCard(r2);

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
            

        }

        [TestMethod]
        public void TestDrawEmptyDeck()
        {
            var d = new Deck();
            try
            {
                d.DrawCard();
                Assert.IsTrue(true);
            } catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestWasDeckChanged()
        {
            var deck = new Deck();
            Assert.IsFalse(deck.WasDeckChanged());
            deck.AddCard(new TestCard());
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

            TestCard r = new TestCard();
            TestCard p = new TestCard();
            TestCard h = new TestCard();
            TestCard r2 = new TestCard();

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
            var d1 = new Deck();
            var d2 = new Deck();

            TestCard c = new TestCard();

            var passes = false;

            d1.AddCard(c);
            try
            {
                d2.AddCard(c);
            } catch (Exception)
            {
                passes = true;
            }

            Assert.IsTrue(passes);
        }

        [TestMethod]
        public void TestInsertSameCardAfterDraw()
        {
            var d1 = new Deck();
            var d2 = new Deck();

            TestCard c = new TestCard();

            d1.AddCard(c);
            d2.AddCard(d1.DrawCard());

            Assert.AreSame(c, d2.DrawCard());
        }

        [TestMethod]
        public void TestSubDeck()
        {
            var deck = new Deck();
            TestCard action = new TestCard();
            TestCard victory = new TestCard();
            TestCard2 treasure = new TestCard2();

            deck.AddCard(action);
            deck.AddCard(victory);
            deck.AddCard(treasure);

            var actionCard = deck.SubDeck(IsCardAction).CardList[0];
            var victoryCard = deck.SubDeck(IsCardAction).CardList[1];
            var treasureCard = deck.SubDeck(IsCardTreasure).CardList[0];

            Assert.AreEqual(deck.CardList[0], actionCard);

            Assert.AreEqual(deck.CardList[1], victoryCard);

            Assert.AreEqual(deck.CardList[2], treasureCard);
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

            public void PlayCard(Player player)
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

            public void PlayCard(Player player)
            {
                throw new NotImplementedException();
            }

            public bool CanAfford(Player player)
            {
                throw new NotImplementedException();
            }
        }
    }
}
