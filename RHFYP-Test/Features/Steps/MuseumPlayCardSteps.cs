using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public MuseumPlayCardSteps(GameSteps game)
        {
            _game = game;
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


    }
}
