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
        public void TestArea51OneCardTrashed()
        {
            // not yet implemented
        }

    }
}
