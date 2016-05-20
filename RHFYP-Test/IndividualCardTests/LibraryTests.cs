using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class LibraryTests
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLibraryPlayerNotExist()
        {
            Card c = new Library();
            c.PlayCard(null, null);
        }

        [TestMethod]
        public void TestLibraryFactory()
        {
            ICard card = new Library();
            var newCard = card.CreateCard() as Library;
            Assert.IsNotNull(newCard);
        }
    }
}
