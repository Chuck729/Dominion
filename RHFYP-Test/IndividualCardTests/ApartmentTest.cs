using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using RHFYP.Cards.ActionCards;

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
            c.PlayCard(null, null);
            
        }

        [TestMethod]
        public void TestApartmentInvestmentIncrease()
        {
            Card c = new Apartment();
            var p = _mocks.DynamicMock<Player>("test");

            p.Managers = 4;

            using (_mocks.Ordered())
            {
                p.Expect(x => x.DrawCard()).Return(false);
            }
            _mocks.ReplayAll();

            c.PlayCard(p, null);

            Assert.AreEqual(6, p.Managers);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardApartment_WithoutCardsToDraw()
        {
            ICard card = new Apartment();
            var p = _mocks.DynamicMock<Player>("bob");

            using (_mocks.Ordered())
            {
                p.Expect(x => x.DrawCard()).Return(false);
            }

            _mocks.ReplayAll();
            card.PlayCard(p, null);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardApartment_WithCardsToDraw()
        {
            ICard card = new Apartment();
            var p = _mocks.DynamicMock<Player>("bob");

            // Add card to players draw pile.
            var drawCard = _mocks.Stub<ICard>();
            p.DrawPile.AddCard(drawCard);

            using (_mocks.Ordered())
            {
                p.Expect(x => x.DrawCard()).Return(true);
            }

            _mocks.ReplayAll();
            card.PlayCard(p, null);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestApartmentFactory()
        {
            ICard card = new Apartment();
            var newCard = card.CreateCard() as Apartment;
            Assert.IsNotNull(newCard);
        }
    }
}
