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

      






        /// <summary>
        /// A card class used for testing purposes
        /// </summary>
        private class TestCard : Card
        {
            //Create test card with certain values
            //this is just a test with random assigned values
            //each other card implemented will have meaningful values
            public TestCard() : base(3, "TestCard", "This card is used for testing purposes", "action", 1)
            {

            }

            public override void PlayCard()
            {
                throw new NotImplementedException();
            }
        }
    }

    
}
