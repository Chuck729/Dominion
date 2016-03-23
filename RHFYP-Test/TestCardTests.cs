using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;

namespace RHFYP_Test
{
    [TestClass]
    public class TestCardTests
    {
        [TestMethod]
        public void TestTestCard()
        {
            Card c = new TestCard();
            Assert.AreEqual(3, c.CardCost);
            Assert.AreEqual("action", c.Type);
            Assert.AreEqual("This card is used for testing purposes", c.Description);
            Assert.AreEqual("TestCard", c.Name);
            Assert.AreEqual(1, c.VictoryPoints);
        }

        [TestMethod]
        public void TestChangeCardCost()
        {
            Card c = new TestCard();
            try
            {
                c.CardCost = 4;
                Assert.IsTrue(false);
            } catch (Exception e)
            {
                Assert.AreEqual(3, c.CardCost);
            }
  
        }
    }
}
