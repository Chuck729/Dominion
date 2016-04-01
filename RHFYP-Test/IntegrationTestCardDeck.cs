using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{
    [TestClass]
    public class IntegrationTestCardDeck
    {
        [TestMethod]
        public void IntegrationTestGetCardDataFromDeck()
        {
            Deck d = new Deck();
            d.AddCard(new Rose());

            ICard c = d.DrawCard();

            Assert.AreSame("victory", c.Type);
            Assert.AreSame("rosehulman", c.Name);
        }

        [TestMethod]
        public void IntegrationTestDrawAndAdd()
        {
            Deck d1 = new Deck();
            Deck d2 = new Deck();
            Card r = new Rose();

            d1.AddCard(r);
            d2.AddCard(d1.DrawCard());
            Assert.AreSame(r, d2.DrawCard());
            Assert.IsTrue(d1.CardList.Count == 0);
        }

        //TODO test BuyCard (cant do all of it because Game is not complete DONE 
        //          PlayCard
        //          PlayAll...
                    
        [TestMethod]
        public void IntegrationTestBuyCard()
        {
            Player player = new Player("Foo Bar");
            player.Gold = 4;
            Card card1 = new TestCard();
            player.BuyCard(card1);
            Assert.AreEqual(player.Gold, 4 - card1.CardCost);

            Assert.AreSame(card1, player.DiscardPile.DrawCard());

            Card card2 = new TestCard();
            player.BuyCard(card2);

            Assert.AreEqual(player.Gold, 4 - card1.CardCost);
        }

    }
}
