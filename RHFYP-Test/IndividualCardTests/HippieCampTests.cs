using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.VictoryCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class HippieCampTests
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHippieCampPlayerNotExist()
        {
            Card c = new HippieCamp();
            c.PlayCard(null, null);

        }

        [TestMethod]
        public void TestPlayCardHippieCamp()
        {
            ICard card = new HippieCamp();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p, null);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }

        [TestMethod]
        public void TestHippieCampFactory()
        {
            ICard card = new HippieCamp();
            var newCard = card.CreateCard() as HippieCamp;
            Assert.IsNotNull(newCard);
        }
    }
}
