using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.VictoryCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class RoseTest
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRosePlayerNotExist()
        {
            Card c = new Rose();
            c.PlayCard(null, null);

        }

        [TestMethod]
        public void TestPlayCardRose()
        {
            ICard card = new Rose();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p, null);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }

        [TestMethod]
        public void TestRoseFactory()
        {
            ICard card = new Rose();
            var newCard = card.CreateCard() as Rose;
            Assert.IsNotNull(newCard);
        }
    }
}
