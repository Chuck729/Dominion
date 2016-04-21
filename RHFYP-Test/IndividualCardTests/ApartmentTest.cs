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
    public class ApartmentTest
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestApartmentPlayerNotExist()
        {
            Card c = new Apartment();
            c.PlayCard(null);
            
        }

        [TestMethod]
        public void TestApartmentPlayCard()
        {
            Card c = new Apartment();
            Player p = _mocks.StrictMock<Player>("test");
            Card f1 = _mocks.Stub<Rose>();
            Card f2 = _mocks.Stub<HippieCamp>();
            Card f3 = _mocks.Stub<Purdue>();

            p.Hand.AddCard(f1);
            p.Hand.AddCard(f2);
            p.DrawPile.AddCard(f3);

            Assert.IsTrue(p.DrawPile.CardCount() == 1);
            Assert.IsTrue(p.Hand.CardCount() == 2);

            p.Gold = 5;
            p.Investments = 2;
            p.Managers = 3;

            using (_mocks.Ordered())
            {
                Expect.Call(p.DrawCard()).Return(true);
            }
            _mocks.ReplayAll();

            c.PlayCard(p);

            Assert.AreEqual(5, p.Managers);
            Assert.AreEqual(3, p.Hand.CardCount());

        }

    }
}
