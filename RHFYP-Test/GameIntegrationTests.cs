using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP_Test
{
    [TestClass]
    class GameIntegrationTests
    {
        [TestMethod]
        public void TestCannotBuyCard()
        {
            List<Player> players = new List<Player>();
            Player p = new Player("bob");
            p.Gold = 5;
            players.Add(p);

            Game g = new Game();

            ICard c = new Corporation();
            IDeck d = new Deck();
            d.AddCard(c);

            Assert.IsFalse(g.BuyCard("Corporation", p));
        }

        [TestMethod]
        public void TestCanBuyCard()
        {
            List<Player> players = new List<Player>();
            Player p = new Player("bob");
            p.Gold = 6;
            players.Add(p);

            Game g = new Game();

            Assert.IsTrue(g.BuyCard("Corporation", p));
        }

        
    }
}
