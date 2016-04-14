using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Reflection;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class PlayCardTests
    {
        private MockRepository mocks;

        [TestMethod]
        public void TestPlayCardSmallBusiness()
        {
            ICard c = new SmallBusiness();

            Player p = mocks.DynamicMock<Player>("bob");
            p.Gold = 0;
            
            using (mocks.Ordered())
            {
                p.AddGold(1);
            }

            mocks.ReplayAll();
            c.PlayCard(p);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardCompany()
        {
            ICard c = new Company();

            Player p = mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (mocks.Ordered())
            {
                p.AddGold(3);
            }

            mocks.ReplayAll();
            c.PlayCard(p);
            mocks.VerifyAll();
        }

        [TestInitialize()]
        public void Initialize()
        {
            mocks = new MockRepository();
        }
    }
}
