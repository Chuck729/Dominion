using System;
using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Card hippyCamp = new HippieCamp();
            Card purdue = new Purdue();
            deck.AddCard(rose);
            deck.AddCard(hippyCamp);
            deck.AddCard(purdue);

            Assert.AreEqual(rose, deck.DrawCard());
            Assert.AreEqual(hippyCamp, deck.DrawCard());
            Assert.AreEqual(purdue, deck.DrawCard());
            Assert.AreEqual("Out of cards, need to reshuffle", deck.DrawCard());

        }
    }
}
