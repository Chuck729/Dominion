using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class GameOverFeatureSteps
    {
        private Game _game;

        [Given(@"I have a game")]
        public void GivenIHaveAGame()
        {
            _game = new Game();
            _game.SetupPlayers(new []{"bob", "larry"});
        }
        
        [Given(@"there are no (.*) cards left in the buy deck")]
        public void GivenThereAreNoXCardsLeftInTheBuyDeck(string cardName)
        {
            while (_game.BuyDeck.GetFirstCard(x => x.Name == cardName) != null)
            {
            }
        }

        [Given(@"its the end of someones turn")]
        public void GivenItsTheEndOfSomeonesTurn()
        {
            _game.NextTurn();
        }

        [Then(@"the game should be over")]
        public void ThenTheGameShouldBeOver()
        {
            Assert.IsTrue(_game.GameState == GameState.Ended);
        }
        
        [Then(@"The player with the most victory points should win")]
        public void ThenThePlayerWithTheMostVictoryPointsShouldWin()
        {
            var maxVp = 0;
            IPlayer winningPlayer = _game.Players[0];
            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var player in _game.Players)
            {
                if (maxVp <= player.VictoryPoints) continue;
                maxVp = player.VictoryPoints;
                winningPlayer = player;
            }
            Assert.IsTrue(winningPlayer.Winner);
        }
    }
}
