using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class CardDeckIntegrationTests
    {
        [TestMethod]
        public void IntegrationTestGetCardDataFromDeck()
        {
            var d = new Deck();
            d.AddCard(new Rose());

            var c = d.DrawCard();

            Assert.AreEqual(CardType.Victory, c.Type);
            Assert.AreEqual("Rose-Hulman", c.Name);
        }

        [TestMethod]
        public void IntegrationTestDrawAndAdd()
        {
            var d1 = new Deck();
            var d2 = new Deck();
            Card r = new Rose();

            d1.AddCard(r);
            d2.AddCard(d1.DrawCard());
            Assert.AreSame(r, d2.DrawCard());
            Assert.IsTrue(d1.CardList.Count == 0);
        }
                    
        [TestMethod]
        public void IntegrationTestBuyCard()
        {
            var player = new Player("Foo Bar") {Gold = 10, Investments = 2};
            Card card1 = new TestCard();
            player.BuyCard(card1);
            Assert.AreEqual(player.Gold, 10 - card1.CardCost);

            Assert.AreSame(card1, player.DiscardPile.DrawCard());

            Card card2 = new TestCard();
            player.BuyCard(card2);

            Assert.AreEqual(player.Gold, 10 - card1.CardCost - card2.CardCost);
        }

        [TestMethod]
        public void IntegrationTestPlayAllTreasures()
        {
            var player = new Player("foo bar");
            ICard t1 = new Corporation();
            ICard t2 = new Corporation();
            ICard a1 = new TestCard();
            ICard a2 = new TestCard();

            player.Hand.AddCard(t1);
            player.Hand.AddCard(a1);
            player.Hand.AddCard(t2);
            player.Hand.AddCard(a2);

            player.PlayAllTreasures();

            var discard = player.DiscardPile;
            Assert.AreEqual(2, discard.CardCount());
            Assert.AreEqual(2, player.Hand.CardCount());
        }

        [TestMethod]
        public void IntegrationTestPlaySmallBusinessCard()
        {
            var p = new Player("") {Gold = 3};
            var c = new SmallBusiness();
            p.Hand.AddCard(c);
            p.PlayCard(c);
            Assert.AreEqual(3+1, p.Gold);
        }

    }
}
