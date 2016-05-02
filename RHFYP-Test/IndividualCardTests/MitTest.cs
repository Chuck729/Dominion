using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.VictoryCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class MitTest
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMitPlayerNotExist()
        {
            Card c = new Mit();
            c.PlayCard(null, null);

        }

        [TestMethod]
        public void TestPlayCardMit()
        {
            ICard card = new Mit();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p, null);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }

        [TestMethod]
        public void TestMitFactory()
        {
            ICard card = new Mit();
            var newCard = card.CreateCard() as Mit;
            Assert.IsNotNull(newCard);
        }
    }
}
