using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class SimpleCardSteps
    {
        private Player _player;
        [Given(@"I have a player")]
        public void GivenIHaveAPlayer()
        {
            _player = new Player("");
        }

        private Storeroom _storeroom;
        [Given(@"the player has (.*) Storeroom cards")]
        public void GivenThePlayerHasStoreroomCard(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _storeroom = new Storeroom();
                _player.GiveCard(_storeroom);
            }
        }

        private LawFirm _lawFirm;
        [Given(@"the player has (.*) LawFirm cards")]
        public void GivenThePlayerHasLawFirmCard(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _lawFirm = new LawFirm();
                _player.GiveCard(_lawFirm);
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
            _storeroom.PlayCard(_player, new Game());
        }

        [When(@"the player plays the LawFirm card")]
        public void WhenThePlayerPlaysTheLawFirmCard()
        {
            _lawFirm.PlayCard(_player, new Game());
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
