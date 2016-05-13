using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class ScholarshipTests
    {
        [TestMethod]
        public void ScholarshipFactoryTest()
        {
            ICard card = new Scholarship();
            var newCard = card.CreateCard() as Scholarship;
            Assert.IsNotNull(newCard);
        }
    }
}