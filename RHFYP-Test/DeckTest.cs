﻿using System;
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
    }
}
