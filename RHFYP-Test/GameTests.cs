using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;

namespace RHFYP_Test
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GenerateCards_CardIsPutIntoBuyDeck_BuyDeckNotEmpty()
        {
            var g  = new Game();
            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.generateCards();
            Assert.IsTrue(g.BuyDeck.CardList.Count > 0);
        }
    }
}
