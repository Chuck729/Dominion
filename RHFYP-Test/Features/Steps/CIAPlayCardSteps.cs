using System;
using TechTalk.SpecFlow;
using RHFYP;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.VictoryCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class CIAPlayCardSteps
    {
        GameSteps g;
        SpeedyLoansPlayCardSteps s;
        SimpleCardSteps ss;
        LibraryPlayCardSteps l;

        public CIAPlayCardSteps(GameSteps g, SpeedyLoansPlayCardSteps s, SimpleCardSteps ss, LibraryPlayCardSteps l)
        {
            this.g = g;
            this.s = s;
            this.ss = ss;
            this.l = l;
        }

        private Cia cia;
        [Given(@"player (.*) has a CIA card in their hand")]
        public void GivenPlayerHasACIACardInTheirHand(int p0)
        {
            cia = new Cia();
            g.Game.Players[p0].Hand.AddCard(cia);
        }

        private Cia ciaError;
        [Given(@"there is a CIA card in the game")]
        public void GivenThereIsACIACardInTheGame()
        {
            ciaError = new Cia();
        }
        
        [When(@"player (.*) plays the CIA card and chooses to discard it")]
        public void WhenPlayerPlaysTheCIACardAndChoosesToDiscardIt(int p0)
        {
            g.Game.Players[p0].PlayCard(cia);
        }
        
        [When(@"the CIA card is played without a player")]
        public void WhenTheCIACardIsPlayedWithoutAPlayer()
        {
            try
            {
                ciaError.PlayCard(null, g.Game);
            }
            catch (Exception e)
            {
                s.caughtException = e;
            }
        }
        
        [When(@"the CIA card is played without a game")]
        public void WhenTheCIACardIsPlayedWithoutAGame()
        {
            try
            {
                ciaError.PlayCard(new RHFYP.Player(""), null);
            }
            catch (Exception e)
            {
                s.caughtException = e;
            }
        }
        
        [Then(@"player (.*) has (.*) non-action cards in their discard pile")]
        public void ThenPlayerHasNon_ActionCardsInTheirDiscardPile(int p0, int p1)
        {
            Assert.AreEqual(p1, g.Game.Players[p0].DiscardPile.CardList.Count);
        }
        
        [Then(@"player (.*) does not have a CIA card in their hand")]
        public void ThenPlayerDoesNotHaveACIACardInTheirHand(int p0)
        {
            Assert.IsFalse(g.Game.Players[p0].Hand.CardList.Contains(cia));
        }
        
        [Then(@"player (.*) has a CIA card in their discard pile")]
        public void ThenPlayerHasACIACardInTheirDiscardPile(int p0)
        {
            Assert.IsTrue(g.Game.Players[p0].DiscardPile.CardList.Contains(cia));
        }
    }
}
