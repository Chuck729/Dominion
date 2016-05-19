using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class CIAPlayCardSteps
    {
        [Given(@"player (.*) has a CIA card in their hand")]
        public void GivenPlayerHasACIACardInTheirHand(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"there is a CIA card in the game")]
        public void GivenThereIsACIACardInTheGame()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"player (.*) plays the CIA card and chooses to discard it")]
        public void WhenPlayerPlaysTheCIACardAndChoosesToDiscardIt(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the CIA card is played without a player")]
        public void WhenTheCIACardIsPlayedWithoutAPlayer()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the CIA card is played without a game")]
        public void WhenTheCIACardIsPlayedWithoutAGame()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"player (.*) has (.*) non-action cards in their discard pile")]
        public void ThenPlayerHasNon_ActionCardsInTheirDiscardPile(int p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"player (.*) does not have a CIA card in their hand")]
        public void ThenPlayerDoesNotHaveACIACardInTheirHand(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"player (.*) has a CIA card in their discard pile")]
        public void ThenPlayerHasACIACardInTheirDiscardPile(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
