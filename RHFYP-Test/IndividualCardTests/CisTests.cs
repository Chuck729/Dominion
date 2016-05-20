using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class CisTests
    {
        private MockRepository _mocks;

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCisPlayerNotExist()
        {
            Card c = new Cia();
            c.PlayCard(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCisGameNotExist()
        {
            Card c = new Cia();
            c.PlayCard(_mocks.Stub<Player>(""), null);
        }

        [TestMethod]
        public void TestCisFactory()
        {
            ICard card = new Cia();
            var newCard = card.CreateCard() as Cia;
            Assert.IsNotNull(newCard);
        }
    }
}