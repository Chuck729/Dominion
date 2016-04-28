using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class SmallBusinessTest
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
            c.PlayCard(null);

        }

        [TestMethod]
        public void TestSmallBusinessGoldIncrease()
        {
            Card c = new SmallBusiness();
            Player p = _mocks.DynamicMock<Player>("test");

            p.Gold = 4;

            using (_mocks.Ordered())
            {
                p.AddGold(1);
            }
            _mocks.ReplayAll();

            c.PlayCard(p);

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
            c.PlayCard(p);
            _mocks.VerifyAll();
        }

    }
}
