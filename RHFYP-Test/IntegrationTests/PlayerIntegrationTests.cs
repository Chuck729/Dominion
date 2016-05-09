using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.IntegrationTests
{
    [TestClass]
    public class PlayerIntegrationTests
    {
        [TestMethod]
        public void TestTreasureCardsInHand()
        {
            var player = new Player("");

            Assert.IsFalse(player.TreasureCardsInHand);
            player.Hand.AddCard(new Corporation());
            Assert.IsTrue(player.TreasureCardsInHand);
            player.Hand.CardList.Clear();
            Assert.IsFalse(player.TreasureCardsInHand);
        }

        [TestMethod]
        public void TestActionCardsInHand()
        {
            var player = new Player("");

            Assert.IsFalse(player.ActionCardsInHand);
            player.Hand.AddCard(new Area51());
            Assert.IsTrue(player.ActionCardsInHand);
            player.Hand.CardList.Clear();
            Assert.IsFalse(player.ActionCardsInHand);
        }

        [TestMethod]
        public void TestEndActions_RemovesAllActionsFromHand()
        {
            var player = new Player("") {PlayerState = PlayerState.Action};

            var card1 = new Area51();
            var card2 = new Area51();
            var card3 = new Corporation();

            player.Hand.AddCard(card1);
            player.Hand.AddCard(card2);
            player.Hand.AddCard(card3);

            Assert.IsTrue(player.ActionCardsInHand);
            player.EndActions();
            Assert.IsFalse(player.ActionCardsInHand);
            Assert.IsTrue(player.TreasureCardsInHand);
        }
    }
}