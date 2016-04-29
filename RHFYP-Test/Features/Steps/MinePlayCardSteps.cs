using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class MinePlayCardSteps
    {
        private readonly GameSteps _game;
        public MinePlayCardSteps(GameSteps game)
        {
            _game = game;
        }

        private Mine _mineCard;
        [Given(@"player ([0-9]) has a Mine card in their hand")]
        public void GivenPlayerHasAMineCardInThierHand(int player)
        {
            _mineCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Mine") as Mine;
            if (_mineCard != null) return;
            _mineCard = new Mine();
            _game.Game.Players[player].Hand.AddCard(_mineCard);
        }

        [Given(@"player ([0-9]) doesnt have a (.*) in their hand")]
        public void GivenPlayerDoesntHaveACertainCardInTheirHand(int player, string cardName)
        {
            while (_game.Game.Players[player].Hand.GetFirstCard(x => x.Name == cardName) != null)
            {
                // Do nothing.
            }
        }

        [Then(@"player ([0-9]) cant play the Mine card")]
        public void ThenPlayerCantPlayTheMineCard(int player)
        {
            Assert.IsFalse(_game.Game.Players[player].PlayCard(_mineCard));
        }

        [Given(@"player ([0-9]) is in Action mode")]
        public void GivenPlayerIsInActionMode(int player)
        {
            _game.Game.Players[player].PlayerState = PlayerState.Action;
        }

    }
}
