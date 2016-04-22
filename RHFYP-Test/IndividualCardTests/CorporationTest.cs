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
    public class CorporationTest
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
            c.PlayCard(null);

        }

        [TestMethod]
        public void TestCorporationGoldIncrease()
        {
            Card c = new Corporation();
            Player p = _mocks.DynamicMock<Player>("test");

            p.Gold = 4;

            using (_mocks.Ordered())
            {

            }
            _mocks.ReplayAll();

            c.PlayCard(p);

            Assert.AreEqual(7, p.Gold);

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
            c.PlayCard(p);
            _mocks.VerifyAll();
        }
    }
}
