using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class HomelessGuyTests
    {
        [TestMethod]
        public void HomelessGuyFactoryTest()
        {
            ICard card = new HomelessGuy();
            var newCard = card.CreateCard() as HomelessGuy;
            Assert.IsNotNull(newCard);
        }
    }
}