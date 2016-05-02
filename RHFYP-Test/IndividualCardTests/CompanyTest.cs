using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.TreasureCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class CompanyTest
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompanyPlayerNotExist()
        {
            Card c = new Company();
            c.PlayCard(null, null);

        }

        [TestMethod]
        public void TestCompanyGoldIncrease()
        {
            Card c = new Company();
            var p = _mocks.DynamicMock<Player>("test");

            p.Gold = 4;

            using (_mocks.Ordered())
            {
                p.AddGold(2);
            }
            _mocks.ReplayAll();

            c.PlayCard(p, null);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardCompany()
        {
            ICard c = new Company();

            var p = _mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (_mocks.Ordered())
            {
                p.AddGold(2);
            }

            _mocks.ReplayAll();
            c.PlayCard(p, null);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestCompanyFactory()
        {
            ICard card = new Company();
            var newCard = card.CreateCard() as Company;
            Assert.IsNotNull(newCard);
        }
    }
}
