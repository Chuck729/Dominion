using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class WallStreetTests
    {
        [TestMethod]
        public void WallStreetFactoryTest()
        {
            ICard card = new WallStreet();
            var newCard = card.CreateCard() as WallStreet;
            Assert.IsNotNull(newCard);
        }
    }
}