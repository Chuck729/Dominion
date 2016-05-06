using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class Area51Test
    {
        private MockRepository _mocks;

        [TestInitialize]
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
            c.PlayCard(null, null);
        }

        [TestMethod]
        public void TestArea51PlayCard_PlayerGets4Nukes()
        {
            var area51 = new Area51();
            var p = _mocks.DynamicMock<Player>("");

            _mocks.ReplayAll();

            Assert.AreEqual(0, p.Nukes);
            area51.PlayCard(p, null);
            Assert.AreEqual(4, p.Nukes);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestArea51Factory()
        {
            ICard card = new Area51();
            var newCard = card.CreateCard() as Area51;
            Assert.IsNotNull(newCard);
        }
    }
}