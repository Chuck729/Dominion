using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.TreasureCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class CorporationTests
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCorporationPlayerNotExist()
        {
            Card c = new Corporation();
            c.PlayCard(null, null);

        }

        [TestMethod]
        public void TestCorporationGoldIncrease()
        {
            Card c = new Corporation();
            var p = _mocks.DynamicMock<Player>("test");

            p.Gold = 4;

            using (_mocks.Ordered())
            {
                p.AddGold(3);
            }
            _mocks.ReplayAll();

            c.PlayCard(p, null);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardCorporation()
        {
            ICard c = new Corporation();

            var p = _mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (_mocks.Ordered())
            {
                p.AddGold(3);
            }

            _mocks.ReplayAll();
            c.PlayCard(p, null);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestCorporationFactory()
        {
            ICard card = new Corporation();
            var newCard = card.CreateCard() as Corporation;
            Assert.IsNotNull(newCard);
        }
    }
}
