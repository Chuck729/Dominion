using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class ScholarshipPlayCardSteps
    {
        private readonly GameSteps _game;
        private readonly SpeedyLoansPlayCardSteps _speedyLoansSteps; // used to see if Exception is thrown

        public ScholarshipPlayCardSteps(GameSteps game, SpeedyLoansPlayCardSteps speadyLoansSteps)
        {
            _game = game;
            _speedyLoansSteps = speadyLoansSteps;
        }

        private Scholarship _scholarshipCard;
        [Given(@"player ([0-9]) has a Scholarship in their hand")]
        public void GivenPlayerHasAScholarshipInTheirHand(int player)
        {
            _scholarshipCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Scholarship") as Scholarship;
            if (_scholarshipCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_scholarshipCard);
                return;
            }
            _scholarshipCard = new Scholarship();
            _game.Game.Players[player].Hand.AddCard(_scholarshipCard);
        }
        
        [Given(@"player ([0-9]) has ([0-9]) managers")]
        public void GivenPlayerHasManagers(int player, int manager)
        {
            _game.Game.Players[player].Managers = manager;
        }
        
        [Given(@"player ([0-9]) has ([0-9]) investments")]
        public void GivenPlayerDoesNotHaveInvestments(int player, int investments)
        {
            _game.Game.Players[player].Investments = investments;
        }
        
        [When(@"player ([0-9]) plays the Scholarship card")]
        public void WhenPlayerPlaysTheScholarshipCard(int player)
        {
            _game.Game.Players[player].PlayCard(_scholarshipCard);
        }
        
        [Then(@"player ([0-9]) draws a card")]
        public void ThenPlayerDrawsACard(int player)
        {
            Assert.IsTrue(_game.Game.Players[player].DrawCard());
        }
        
        [Then(@"player ([0-9]) has ([0-9]) managers")]
        public void ThenPlayerHasManagers(int player, int managers)
        {
            Assert.AreEqual(managers, _game.Game.Players[player].Managers);
        }
        
        [Then(@"player ([0-9]) has ([0-9]) investments")]
        public void ThenPlayerHasInvestments(int player, int investments)
        {
            Assert.AreEqual(investments, _game.Game.Players[player].Investments);
        }

        private Card _sCard;
        [Given(@"there is a Scholarship card in the game")]
        public void GivenThereIsAScholarshipCardInTheGame()
        {
            _sCard = new Scholarship();
        }

        [Given(@"the Museum Scholarship is played without a player")]
        public void GivenTheMuseumScholarshipIsPlayedWithoutAPlayer()
        {
            try
            {
                _sCard.PlayCard(null, _game.Game);
            }
            catch (Exception e)
            {
                _speedyLoansSteps.caughtException = e;
            }
        }

        [Given(@"the Museum Scholarship is played without a game")]
        public void GivenTheMuseumScholarshipIsPlayedWithoutAGame()
        {
            try
            {
                _sCard.PlayCard(new RHFYP.Player(""), null);
            }
            catch (Exception e)
            {
                _speedyLoansSteps.caughtException = e;
            }
        }


    }
}
