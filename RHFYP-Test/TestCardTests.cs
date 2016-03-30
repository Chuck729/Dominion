using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;

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
            } catch (Exception)
            {
                Assert.AreEqual(3, c.CardCost);
            }
  
        }

        [TestMethod]
        public void TestChangeType()
        {
            Card c = new TestCard();
            try
            {
                c.Type = "victory";
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.AreEqual("action", c.Type);
            }

        }

        [TestMethod]
        public void TestChangeDescription()
        {
            Card c = new TestCard();
            try
            {
                c.Description = "foo";
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.AreEqual("This card is used for testing purposes", c.Description);
            }

        }

        [TestMethod]
        public void TestChangName()
        {
            Card c = new TestCard();
            try
            {
                c.Name = "bar";
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.AreEqual("TestCard", c.Name);
            }
            
        }

        [TestMethod]
        public void TestChangeVp()
        {
            Card c = new TestCard();
            try
            {
                c.VictoryPoints = 100;
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.AreEqual(1, c.VictoryPoints);
            }

        }
    }
}
