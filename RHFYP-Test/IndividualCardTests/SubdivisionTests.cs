using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class SubdivisionTests
    {
        [TestMethod]
        public void SubdivisionFactoryTest()
        {
            ICard card = new Subdivision();
            var newCard = card.CreateCard() as Subdivision;
            Assert.IsNotNull(newCard);
        }
    }
}