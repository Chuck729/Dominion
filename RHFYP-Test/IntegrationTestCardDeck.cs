using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;

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
            Assert.AreSame("victory", d.DrawCard().Type);
        }
    }
}
