using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP_Test.Features.Steps;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test
{
    [Binding]
    public class CEOsHouseFeatureSteps
    {

        private readonly GameSteps _game;
        private readonly SpeedyLoansPlayCardSteps _speedyLoansSteps; // used to see if Exception is thrown
        private readonly MinePlayCardSteps _minePlayCardSteps; // used for adding a company
       
        public CEOsHouseFeatureSteps(GameSteps game, SpeedyLoansPlayCardSteps speadyLoansSteps, 
            MinePlayCardSteps minePlayCardSteps)
        {
            _game = game;
            _speedyLoansSteps = speadyLoansSteps;
            _minePlayCardSteps = minePlayCardSteps;
        }

        private ICard _cCard = new CeosHouse();
        [Given(@"player (.) has a CEOsHouse in their hand")]
        public void GivenPlayerHasACEOsHouseInTheirHand(int p0)
        {
            _game.Game.Players[p0].Hand.AddCard(_cCard);
        }
        
        [When(@"player (.) plays the CEOsHouse card")]
        public void WhenPlayerPlaysTheCEOsHouseCard(int p0)
        {
            _game.Game.Players[p0].PlayCard(_cCard);
        }

        [When(@"player (.) plays the Company card")]
        public void WhenPlayerPlaysTheCompanyCard(int p0)
        {
            _game.Game.Players[p0].PlayCard(_minePlayCardSteps._companyCard);
        }

        [When(@"player (.) plays the SmallBusiness card")]
        public void WhenPlayerPlaysTheSmallBusinessCard(int p0)
        {
            _game.Game.Players[p0].PlayCard(_minePlayCardSteps._smallBusinessCard);
        }

        private Card _aCard = new Apartment();
        [Given(@"player (.) has a Apartment in their hand")]
        public void GivenPlayerHasAApartmentInTheirHand(int p0)
        {
            _game.Game.Players[p0].Hand.AddCard(_aCard);
        }

        [Then(@"player (.) has (.) managers left")]
        public void ThenPlayerHasManagersLeft(int p0, int p1)
        {
            Assert.AreEqual(p1, _game.Game.Players[p0].Managers);
        }

        [When(@"player (.) plays the Apartment card")]
        public void WhenPlayerPlaysTheApartmentCard(int p0)
        {
            _game.Game.Players[p0].PlayCard(_aCard);
        }

        [Given(@"there is a CEOsHouse card in the game")]
        public void GivenThereIsACEOsHouseCardInTheGame()
        {
            _cCard = new CeosHouse();
        }

        [Given(@"the CEOsHouse card is played without a player")]
        public void GivenTheCEOsHouseCardIsPlayedWithoutAPlayer()
        {
            try
            {
                _cCard.PlayCard(null, _game.Game);
            }
            catch (Exception e)
            {
                _speedyLoansSteps.caughtException = e;
            }
        }

        [Given(@"the CEOsHouse card is played without a game")]
        public void GivenTheCEOsHouseCardIsPlayedWithoutAGame()
        {
            try
            {
                _cCard.PlayCard(new RHFYP.Player(""), null);
            }
            catch (Exception e)
            {
                _speedyLoansSteps.caughtException = e;
            }
        }

    }
}
