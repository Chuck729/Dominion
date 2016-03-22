using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;

namespace RHFYP_Test
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void TestGetCost()
        {
            Card card = new Card();
            Assert.AreEqual(0, card.CardCost);
        }
    }
}
