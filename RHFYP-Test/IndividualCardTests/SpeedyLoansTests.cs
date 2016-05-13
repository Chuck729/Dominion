using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class SpeedyLoansTests
    {
        [TestMethod]
        public void SpeedyLoansFactoryTest()
        {
            ICard card = new SpeedyLoans();
            var newCard = card.CreateCard() as SpeedyLoans;
            Assert.IsNotNull(newCard);
        }
    }
}