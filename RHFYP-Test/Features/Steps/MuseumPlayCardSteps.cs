using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class MuseumPlayCardSteps
    {
        private readonly GameSteps _game;
        private readonly SpeedyLoansPlayCardSteps _speedyLoansSteps; // used to see if Exception is thrown
        public MuseumPlayCardSteps(GameSteps game, SpeedyLoansPlayCardSteps speadyLoansSteps)
        {
            _game = game;
            _speedyLoansSteps = speadyLoansSteps;
        }

        private Museum  _museumCard;
        [Given(@"player ([0-9]) has a Museum card")]
        public void GivenPlayerHasAMuseumCard(int player)
        {
            _museumCard = _game.Game.Players[player].Hand.GetFirstCard(x => x is Museum) as Museum;
            if (_museumCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_museumCard);
                return;
            }
            _museumCard = new Museum();
            _game.Game.Players[player].Hand.AddCard(_museumCard);
        }
        
        [When(@"player ([0-9]) plays the Museum card")]
        public void WhenPlayerPlaysTheMuseumCard(int player)
        {
            _game.Game.Players[player].PlayCard(_museumCard);
        }
        
        
        
        [Then(@"player ([0-9]) Museum is discarded")]
        public void ThenPlayerMuseumIsDiscarded(int player)
        {
            if (player < 0) throw new ArgumentOutOfRangeException(nameof(player));
            Assert.IsTrue(_game.Game.Players[player].DiscardPile.CardList.Contains(_museumCard));
        }


        [Then(@"player ([0-9]) can not play the Museum card")]
        public void ThenPlayerCanNotPlayTheMuseumCard(int player)
        {
            Assert.IsFalse(_game.Game.Players[player].PlayCard(_museumCard));
        }

        private ICard _mCard;
        [Given(@"there is a Museum card in the game")]
        public void GivenThereIsAMuseumCardInTheGame()
        {
            _mCard = new Museum();
        }

        [Given(@"the Museum card is played without a player")]
        public void GivenTheMuseumCardIsPlayedWithoutAPlayer()
        {
            try
            {
                _mCard.PlayCard(null, _game.Game);
            }
            catch (Exception e)
            {
                _speedyLoansSteps.caughtException = e;
            }
        }

        [Given(@"the Museum card is played without a game")]
        public void GivenTheMuseumCardIsPlayedWithoutAGame()
        {
            try
            {
                _mCard.PlayCard(_game.Game.Players[0], null);
            }
            catch (Exception e)
            {
                _speedyLoansSteps.caughtException = e;
            }
        }


    }
}
