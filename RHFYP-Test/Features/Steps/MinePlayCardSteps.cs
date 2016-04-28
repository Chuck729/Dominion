using RHFYP.Cards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class MinePlayCardSteps
    {
        private GameSteps _game;
        public MinePlayCardSteps(GameSteps game)
        {
            _game = game;
        }

        private Mine mineCard;
        [Given(@"player ([0-9]) has a Mine card in thier hand")]
        public void GivenPlayerHasAMineCardInThierHand(int player)
        {
            mineCard = new Mine();
            //_game.Game.Players[player]
        }

        [Given(@"player ([0-9]) doesnt have a Small Business in their hand")]
        public void GivenPlayerDoesntHaveASmallBusinessInTheirHand(int player)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"player ([0-9]) doesnt have a Company in their hand")]
        public void GivenPlayerDoesntHaveACompanyInTheirHand(int player)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"player ([0-9]) cant play the Mine card")]
        public void ThenPlayerCantPlayTheMineCard(int player)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
