﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class IntegrationTestCardDeck
    {
        [TestMethod]
        public void IntegrationTestGetCardDataFromDeck()
        {
            Deck d = new Deck();
            d.AddCard(new Rose());

            ICard c = d.DrawCard();

            Assert.AreSame("victory", c.Type);
            Assert.AreSame("rosehulman", c.Name);
        }

        [TestMethod]
        public void IntegrationTestDrawAndAdd()
        {
            Deck d1 = new Deck();
            Deck d2 = new Deck();
            Card r = new Rose();

            d1.AddCard(r);
            d2.AddCard(d1.DrawCard());
            Assert.AreSame(r, d2.DrawCard());
            Assert.IsTrue(d1.CardList.Count == 0);
        }

        //TODO test BuyCard (cant do all of it because Game is not complete DONE 
        //          PlayCard 
        //          PlayAll... DONE
                    
        [TestMethod]
        public void IntegrationTestBuyCard()
        {
            Player player = new Player("Foo Bar");
            player.Gold = 4;
            Card card1 = new TestCard();
            player.BuyCard(card1);
            Assert.AreEqual(player.Gold, 4 - card1.CardCost);

            Assert.AreSame(card1, player.DiscardPile.DrawCard());

            Card card2 = new TestCard();
            player.BuyCard(card2);

            Assert.AreEqual(player.Gold, 4 - card1.CardCost);
        }

        [TestMethod]
        public void IntegrationTestPlayAllTreasures()
        {
            Player player = new Player("foo bar");
            ICard t1 = new Corporation();
            ICard t2 = new Corporation();
            ICard a1 = new TestCard();
            ICard a2 = new TestCard();

            player.Hand.AddCard(t1);
            player.Hand.AddCard(a1);
            player.Hand.AddCard(t2);
            player.Hand.AddCard(a2);

            player.PlayAllTreasures();

            IDeck discard = player.DiscardPile;
            Assert.AreEqual(2, discard.CardCount());
            Assert.AreEqual(2, player.Hand.CardCount());
        }

        [TestMethod]
        public void IntegrationTestPlayCard()
        {
            Player p = new Player("Hi Chuck");
            p.Gold = 3;
            p.PlayCard(new TestCard());
            Assert.AreEqual(3+1, p.Gold);
        }

    }
}
