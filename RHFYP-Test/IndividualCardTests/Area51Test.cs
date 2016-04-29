using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class Area51Test
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), 
            "Player doesn't exist.")]
        public void TestArea51PlayerNotExist()
        {
            Card c = new Area51();
            c.PlayCard(null);
        }

        [TestMethod]
        public void TestArea51PlayCard1Card()
        {
            var area51 = new Area51();
            var p = _mocks.DynamicMock<Player>("foo");
            var c1 = _mocks.Stub<Corporation>();

            using (_mocks.Ordered())
            {
                Expect.Call(p.TrashCard(c1)).Return(true);
            }

            _mocks.ReplayAll();

            area51.PlayCard(p, new List<ICard> { c1 });

            _mocks.VerifyAll();
        }

    }
}
