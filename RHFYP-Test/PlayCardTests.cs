using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class PlayCardTests
    {
        private MockRepository _mocks;

        [TestMethod]
        public void TestPlayCardSmallBusiness()
        {
            ICard c = new SmallBusiness();

            var p = _mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (_mocks.Ordered())
            {
                p.AddGold(1);
            }

            _mocks.ReplayAll();
            c.PlayCard(p);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardCompany()
        {
            ICard c = new Company();

            var p = _mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (_mocks.Ordered())
            {
                p.AddGold(3);
            }

            _mocks.ReplayAll();
            c.PlayCard(p);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardCorporation()
        {
            ICard c = new Corporation();

            var p = _mocks.DynamicMock<Player>("bob");
            p.Gold = 0;

            using (_mocks.Ordered())
            {
                p.AddGold(6);
            }

            _mocks.ReplayAll();
            c.PlayCard(p);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardRose()
        {
            ICard card = new Rose();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }

        [TestMethod]
        public void TestPlayCardHippieCamp()
        {
            ICard card = new HippieCamp();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }

        [TestMethod]
        public void TestPlayCardMit()
        {
            ICard card = new Mit();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
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
            card.PlayCard(p);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardApartment_WithCardsToDraw()
        {
            ICard card = new Apartment();
            var p = _mocks.DynamicMock<Player>("bob");

            // Add card to players draw pile.
            var drawCard = _mocks.Stub<ICard>();
            drawCard.IsAddable = true;
            p.DrawPile.AddCard(drawCard);

            using (_mocks.Ordered())
            {
                p.Expect(x => x.DrawCard()).Return(true);
            }

            _mocks.ReplayAll();
            card.PlayCard(p);
            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestPlayCardArea51()
        {

        }

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }
    }
}