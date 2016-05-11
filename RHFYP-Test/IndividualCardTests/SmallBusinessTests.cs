using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.TreasureCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class SmallBusinessTests
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSmallBusinessPlayerNotExist()
        {
            Card c = new SmallBusiness();
            c.PlayCard(null, null);

        }

        [TestMethod]
        public void TestSmallBusinessGoldIncrease()
        {
            Card c = new SmallBusiness();
            var p = _mocks.DynamicMock<Player>("test");

            p.Gold = 4;

            using (_mocks.Ordered())
            {
                p.AddGold(1);
            }
            _mocks.ReplayAll();

            c.PlayCard(p, null);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardSmallBusiness()
        {
            ICard c = new SmallBusiness();

            var p = _mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (_mocks.Ordered())
            {
                p.AddGold(1);
            }

            _mocks.ReplayAll();
            c.PlayCard(p, null);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestSmallBusinessFactory()
        {
            ICard card = new SmallBusiness();
            var newCard = card.CreateCard() as SmallBusiness;
            Assert.IsNotNull(newCard);
        }
    }
}
