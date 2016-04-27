using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test
{
    [Binding]
    public class GameOverFeatureSteps
    {
        [Given(@"I have a game")]
        public void GivenIHaveAGame()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"there are no Rose-Hulman cards left in the buy deck")]
        public void GivenThereAreNoRose_HulmanCardsLeftInTheBuyDeck()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The game should end")]
        public void ThenTheGameShouldEnd()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The player with the most victory points should win")]
        public void ThenThePlayerWithTheMostVictoryPointsShouldWin()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
