using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.TreasureCards;

namespace RHFYP_Test.IntegrationTests
{
    [TestClass]
    public class GameIntegrationTests
    {

        [TestMethod]
        public void TestCannotBuyCard()
        {
            var p = new Player("") {Gold = 5, Investments = 1};
            new List<Player>().Add(p);

            var g = new Game(0);

            ICard c = new Corporation();
            g.BuyDeck.AddCard(c);

            Assert.IsFalse(g.BuyCard("Corporation", p));
        }

        [TestMethod]
        public void TestCanBuyCard()
        {
            var p = new Player("") {Gold = 6, Investments = 1};
            new List<Player>().Add(p);

            var g = new Game(0);
            ICard c = new Corporation();
            g.BuyDeck.AddCard(c);

            Assert.IsTrue(g.BuyCard("Corporation", p));
        }
    }
}