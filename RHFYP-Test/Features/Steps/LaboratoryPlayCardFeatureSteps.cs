using RHFYP.Cards.ActionCards;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class LaboratoryPlayCardFeatureSteps
    {
        private readonly GameSteps _game;

        public LaboratoryPlayCardFeatureSteps(GameSteps game)
        {
            _game = game;
        }

        private Laboratory _laboratoryCard;
        [Given(@"player ([0-9]) has a Laboratory in their hand")]
        public void GivenPlayerHasALaboratoryInTheirHand(int player)
        {
            _laboratoryCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Laboratory") as Laboratory;
            if (_laboratoryCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_laboratoryCard);
                return;
            }
            _laboratoryCard = new Laboratory();
            _game.Game.Players[player].Hand.AddCard(_laboratoryCard);
        }
        
        [When(@"player ([0-9]) plays the Laboratory card")]
        public void WhenPlayerPlaysTheLaboratoryCard(int player)
        {
            _game.Game.Players[player].PlayCard(_laboratoryCard);
        }
    }
}
