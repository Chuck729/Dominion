using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class HomelessGuyFeatureSteps
    {

        private readonly ICard _bank = new Bank();

        private readonly ICard _cis = new Cia();

        private readonly ICard _company = new Company();

        private readonly ICard _corperation = new Corporation();
        private readonly GameSteps _g;

        private ICard _homelessGuy = new HomelessGuy();

        private readonly ICard _smallBusiness = new SmallBusiness();
        private readonly SpeedyLoansPlayCardSteps _sl;

        public HomelessGuyFeatureSteps(SimpleCardSteps simpleTest, GameSteps g, SpeedyLoansPlayCardSteps sl)
        {
            _g = g;
            this._sl = sl;
            _g.Game.SetupPlayers(new[] {"test", "Test2"});
        }

        [Given(@"the player has a Small Business in their hand")]
        public void GivenThePlayerHasASmallBusinessInTheirHand()
        {
            _g.Game.Players[0].Hand.AddCard(_smallBusiness);
        }

        [Given(@"the player has a company in their hand")]
        public void GivenThePlayerHasACompanyInTheirHand()
        {
            _g.Game.Players[0].Hand.AddCard(_company);
        }

        [Given(@"the player has a corporation in their hand")]
        public void GivenThePlayerHasACorporationInTheirHand()
        {
            _g.Game.Players[0].Hand.AddCard(_corperation);
        }

        [Given(@"the player has a homelessguy in their hand")]
        public void GivenThePlayerHasAHomelessguyInTheirHand()
        {
            _g.Game.Players[0].Hand.AddCard(_homelessGuy);
        }

        [Given(@"the player has a bank in their draw deck")]
        public void GivenThePlayerHasABankInTheirDrawDeck()
        {
            _g.Game.Players[0].DrawPile.AddCard(_bank);
        }

        [Given(@"the player has a Cia in thier draw deck")]
        public void GivenThePlayerHasACisInThierDrawDeck()
        {
            _g.Game.Players[0].DrawPile.AddCard(_cis);
        }

        [When(@"the player plays the homeless guy")]
        public void WhenThePlayerPlaysTheHomelessGuy()
        {
            _g.Game.Players[0].PlayCard(_homelessGuy);
        }

        [When(@"the player chooses to discard the small business")]
        public void WhenThePlayerChoosesToDiscardTheSmallBusiness()
        {
            _g.Game.Players[0].PlayCard(_smallBusiness);
        }

        [When(@"the player chooses to discard the company")]
        public void WhenThePlayerChoosesToDiscardTheCompany()
        {
            _g.Game.Players[0].PlayCard(_company);
        }

        [Then(@"the small business is not in their hand")]
        public void ThenTheSmallBusinessIsNotInTheirHand()
        {
            Assert.IsFalse(_g.Game.Players[0].Hand.CardList.Contains(_smallBusiness));
        }

        [Then(@"the company is not in their hand")]
        public void ThenTheCompanyIsNotInTheirHand()
        {
            Assert.IsFalse(_g.Game.Players[0].Hand.CardList.Contains(_company));
        }

        [Then(@"the bank is in their hand")]
        public void ThenTheBankIsInTheirHand()
        {
            Assert.IsTrue(_g.Game.Players[0].Hand.CardList.Contains(_bank));
        }

        [Then(@"the Cia is in their hand")]
        public void ThenTheCisIsInTheirHand()
        {
            Assert.IsTrue(_g.Game.Players[0].Hand.CardList.Contains(_cis));
        }

        [When(@"HomelessGuyMode is set to false")]
        public void WhenHomelessGuyModeIsSetToFalse()
        {
            _g.Game.Players[0].DrawAfterHomelessGuyMode();
        }

        [Given(@"the player has (.) manager")]
        public void GivenThePlayerHasManager(int p0)
        {
            _g.Game.Players[0].Managers = p0;
        }

        [Given(@"there is a Homeless Guy card in the game")]
        public void GivenThereIsAHomelessGuyCardInTheGame()
        {
            _homelessGuy = new HomelessGuy();
        }

        [When(@"the Homeless Guy card is played without a player")]
        public void WhenTheHomelessGuyCardIsPlayedWithoutAPlayer()
        {
            try
            {
                _homelessGuy.PlayCard(null, _g.Game);
            }
            catch (Exception e)
            {
                _sl.caughtException = e;
            }
        }

        [When(@"the Homeless Guy card is played without a game")]
        public void WhenTheHomelessGuyCardIsPlayedWithoutAGame()
        {
            try
            {
                _homelessGuy.PlayCard(new Player(""), null);
            }
            catch (Exception e)
            {
                _sl.caughtException = e;
            }
        }

        [Given(@"the player has no cards")]
        public void GivenThePlayerHasNoCards()
        {
            _g.Game.Players[0].Hand = new Deck();
            _g.Game.Players[0].DrawPile = new Deck();
            _g.Game.Players[0].DiscardPile = new Deck();
        }
    }
}