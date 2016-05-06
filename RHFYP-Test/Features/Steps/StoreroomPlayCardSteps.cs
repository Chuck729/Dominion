using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class StoreroomPlayCardSteps
    {
        private readonly Player _player;
        public StoreroomPlayCardSteps(GardensPlayCardFeatureSteps gpc)
        {
            _player = gpc.Player;
        }

        private Storeroom storeroom = null;
        [Given(@"the player has (.*) Storeroom card")]
        public void GivenThePlayerHasStoreroomCard(int n)
        {
            for (var i = 0; i < n; i++)
            {
                storeroom = new Storeroom();
                _player.GiveCard(storeroom);
            }
        }
        
        [Given(@"the player has (.*) Gold")]
        public void GivenThePlayerHasGold(int n)
        {
            _player.Gold = n;
        }
        
        [Given(@"the player has (.*) Investments")]
        public void GivenThePlayerHasInvestments(int n)
        {
            _player.Investments = n;
        }
        
        [Given(@"the player has (.*) Managers")]
        public void GivenThePlayerHasManagers(int n)
        {
            _player.Managers = n;
        }
        
        [When(@"the player plays the Storeroom card")]
        public void WhenThePlayerPlaysTheStoreroomCard()
        {
            storeroom.PlayCard(_player, new Game());
        }

        [Then(@"the player has (.*) Gold")]
        public void ThenThePlayerHasGold(int n)
        {
            Assert.AreEqual(n, _player.Gold);
        }

        [Then(@"the player has (.*) Investments")]
        public void ThenThePlayerHasInvestments(int n)
        {
            Assert.AreEqual(n, _player.Investments);
        }

        [Then(@"the player has (.*) Managers")]
        public void ThenThePlayerHasManagers(int n)
        {
            Assert.AreEqual(n, _player.Managers);
        }

    }
}
