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

    }
}
