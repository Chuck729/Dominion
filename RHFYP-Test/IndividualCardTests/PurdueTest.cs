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
    public class PurdueTest
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPurduePlayerNotExist()
        {
            Card c = new Purdue();
            c.PlayCard(null);

        }


        [TestMethod]
        public void TestPlayCardPurdue()
        {
            ICard card = new Purdue();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }


    }
}
