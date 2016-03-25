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
        public void TestGetCardDataFromDeck()
        {
            Deck d = new Deck();
            d.AddCard(new Rose());

            Card c = d.DrawCard();

            Assert.AreSame("victory", c.Type);
            Assert.AreSame("Rose", c.Name);
        }

        [TestMethod]
        public void TestDrawAndAdd()
        {
            Deck d1 = new Deck();
            Deck d2 = new Deck();
            Card r = new Rose();

            d1.AddCard(r);
            d2.AddCard(d1.DrawCard());
            Assert.AreSame(r, d2.DrawCard());
            Assert.IsTrue(d1.CardList.Count == 0);
        }

    }
}