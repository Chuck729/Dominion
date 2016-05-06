using System;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class GardensPlayCardFeatureSteps
    {
        private Player _player = null;
        [Given(@"I have a player")]
        public void GivenIHaveAPlayer()
        {
            _player = new Player("");
        }

        [Given(@"the player has (.*) Corperation cards")]
        public void GivenThePlayerHasCorperationCards(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _player.Hand.CardList.Add(new Corporation());
            }
        }

        [Given(@"the player has (.*) Gardens cards")]
        public void GivenThePlayerHasGardensCards(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _player.Hand.CardList.Add(new Gardens());
            }
        }

        [When(@"the player has (.*) victory cards")]
        public void WhenThePlayerHasVictoryCard(int n)
        {
            Assert.AreEqual(n, _player.Hand.AppendDeck(_player.DiscardPile.AppendDeck(_player.DrawPile)).SubDeck(card => card.Type == CardType.Victory));
        }

        [Then(@"the player should have (.*) victory points")]
        public void ThenThePlayerShouldHaveVictoryPoints(int n)
        {
            Assert.AreEqual(n, _player.VictoryPoints);
        }

    }
}
