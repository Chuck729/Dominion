using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    internal class GameIntegrationTests
    {
        [TestMethod]
        public void TestCannotBuyCard()
        {
            var p = new Player("bob") {Gold = 5};
            new List<Player>().Add(p);

            var g = new Game();

            ICard c = new Corporation();
            IDeck d = new Deck();
            d.AddCard(c);

            Assert.IsFalse(g.BuyCard("Corporation", p));
        }

        [TestMethod]
        public void TestCanBuyCard()
        {
            var p = new Player("bob") {Gold = 6};
            new List<Player>().Add(p);

            var g = new Game();

            Assert.IsTrue(g.BuyCard("Corporation", p));
        }
    }
}