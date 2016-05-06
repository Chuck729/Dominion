using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class CisTest
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
            Card c = new Cis();
            c.PlayCard(null, null);
        }

        [TestMethod]
        public void TestCisFactory()
        {
            ICard card = new Cis();
            var newCard = card.CreateCard() as Cis;
            Assert.IsNotNull(newCard);
        }
    }
}