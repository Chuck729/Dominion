using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class PlayCardTests
    {
        private MockRepository _mocks;

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
            c.PlayCard(p);
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
                p.AddGold(3);
            }

            _mocks.ReplayAll();
            c.PlayCard(p);
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
                p.AddGold(6);
            }

            _mocks.ReplayAll();
            c.PlayCard(p);
            _mocks.VerifyAll();
        }

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }
    }
}