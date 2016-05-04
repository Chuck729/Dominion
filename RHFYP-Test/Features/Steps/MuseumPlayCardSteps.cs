﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP_Test.Features.Steps;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test
{
    [Binding]
    public class MuseumPlayCardSteps
    {
        private readonly GameSteps _game;
        private readonly SpeedyLoansPlayCardSteps _speadyLoansSteps; // used to see if Exception is thrown
        public MuseumPlayCardSteps(GameSteps game, SpeedyLoansPlayCardSteps speadyLoansSteps)
        {
            _game = game;
            _speadyLoansSteps = speadyLoansSteps;
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
                _speadyLoansSteps.caughtException = e;
            }
        }


    }
}
