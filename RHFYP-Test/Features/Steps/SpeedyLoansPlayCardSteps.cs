using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class SpeedyLoansPlayCardSteps
    {
        [Given(@"player ([0-9]) has a SpeedyLoans in thier hand")]
        public void GivenPlayerHasASpeedyLoansInThierHand(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"player ([0-9]) plays the SpeedyLoans card")]
        public void WhenPlayerPlaysTheSpeedyLoansCard(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the small business card is put in the trash pile")]
        public void ThenTheSmallBusinessCardIsPutInTheTrashPile()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"player ([0-9]) gains ([0-9]) gold")]
        public void ThenPlayerGainsGold(int p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"SpeedyLoans is discarded")]
        public void ThenSpeedyLoansIsDiscarded()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
