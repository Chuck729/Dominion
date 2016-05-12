using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class ConstructionSiteTests
    {
        [TestMethod]
        public void SubdivisionFactoryTest()
        {
            ICard card = new ConstructionSite();
            var newCard = card.CreateCard() as ConstructionSite;
            Assert.IsNotNull(newCard);
        }
    }
}