using System;
using RHFYP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RHFYP_Test
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void TestAddCard()
        {
            Deck deck = new Deck();
            Card card = new Rose();
            deck.AddCard(card);

        }
    }
}
