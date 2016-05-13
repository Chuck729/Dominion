using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class PrisonTests
    {
        [TestMethod]
        public void PrisonFactoryTest()
        {
            ICard card = new Prison();
            var newCard = card.CreateCard() as Prison;
            Assert.IsNotNull(newCard);
        }
    }
}