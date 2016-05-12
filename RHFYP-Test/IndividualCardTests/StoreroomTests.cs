using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class StoreroomTests
    {
        [TestMethod]
        public void StoreroomFactoryTest()
        {
            ICard card = new Storeroom();
            var newCard = card.CreateCard() as Storeroom;
            Assert.IsNotNull(newCard);
        }
    }
}