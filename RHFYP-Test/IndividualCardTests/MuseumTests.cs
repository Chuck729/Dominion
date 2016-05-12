using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class MuseumTests
    {
        [TestMethod]
        public void MuseumFactoryTest()
        {
            ICard card = new Museum();
            var newCard = card.CreateCard() as Museum;
            Assert.IsNotNull(newCard);
        }
    }
}