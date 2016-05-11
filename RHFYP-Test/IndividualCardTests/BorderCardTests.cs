using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class BorderCardTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestBorderCard_PlayCard_ThrowsInvalidOperationException()
        {
            var card = new BorderCard();
            card.PlayCard(null, null);
        }

        [TestMethod]
        public void TestBorderCard_CreateCard_CreatesABorderCardObject()
        {
            var card = new BorderCard();
            var borderCard = card.CreateCard() as BorderCard;
            if (borderCard == null) Assert.Fail("The border card factory pattern didn't return a border card.");
        }
    }
}
