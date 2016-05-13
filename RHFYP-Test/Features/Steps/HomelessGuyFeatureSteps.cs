using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class HomelessGuyFeatureSteps
    {

        SimpleCardSteps s;
        GameSteps g;
        SpeedyLoansPlayCardSteps sl;

        public HomelessGuyFeatureSteps(SimpleCardSteps simpleTest, GameSteps g, SpeedyLoansPlayCardSteps sl)
        {
            s = simpleTest;
            this.g = g;
            this.sl = sl;
            this.g.Game.SetupPlayers(new string[] { "test", "Test2" });
        }

        ICard sb = new SmallBusiness();
        [Given(@"the player has a Small Business in their hand")]
        public void GivenThePlayerHasASmallBusinessInTheirHand()
        {
            g.Game.Players[0].Hand.AddCard(sb);
        }

        ICard comp = new Company();
        [Given(@"the player has a company in their hand")]
        public void GivenThePlayerHasACompanyInTheirHand()
        {
            g.Game.Players[0].Hand.AddCard(comp);
        }

        ICard cor = new Corporation();
        [Given(@"the player has a corporation in their hand")]
        public void GivenThePlayerHasACorporationInTheirHand()
        {
            g.Game.Players[0].Hand.AddCard(cor);
        }

        ICard hg = new HomelessGuy();
        [Given(@"the player has a homelessguy in their hand")]
        public void GivenThePlayerHasAHomelessguyInTheirHand()
        {
            g.Game.Players[0].Hand.AddCard(hg);
        }

        ICard bank = new Bank();
        [Given(@"the player has a bank in their draw deck")]
        public void GivenThePlayerHasABankInTheirDrawDeck()
        {
            g.Game.Players[0].DrawPile.AddCard(bank);
        }

        ICard cis = new Cis();
        [Given(@"the player has a Cis in thier draw deck")]
        public void GivenThePlayerHasACisInThierDrawDeck()
        {
            g.Game.Players[0].DrawPile.AddCard(cis);
        }
        
        [When(@"the player plays the homeless guy")]
        public void WhenThePlayerPlaysTheHomelessGuy()
        {
            g.Game.Players[0].PlayCard(hg);
        }
        
        [When(@"the player chooses to discard the small business")]
        public void WhenThePlayerChoosesToDiscardTheSmallBusiness()
        {
            g.Game.Players[0].PlayCard(sb);
        }
        
        [When(@"the player chooses to discard the company")]
        public void WhenThePlayerChoosesToDiscardTheCompany()
        {
            g.Game.Players[0].PlayCard(comp);
        }
        
        [Then(@"the small business is not in their hand")]
        public void ThenTheSmallBusinessIsNotInTheirHand()
        {
            Assert.IsFalse(g.Game.Players[0].Hand.CardList.Contains(sb));
        }
        
        [Then(@"the company is not in their hand")]
        public void ThenTheCompanyIsNotInTheirHand()
        {
            Assert.IsFalse(g.Game.Players[0].Hand.CardList.Contains(comp));
        }
        
        [Then(@"the bank is in their hand")]
        public void ThenTheBankIsInTheirHand()
        {
            Assert.IsTrue(g.Game.Players[0].Hand.CardList.Contains(bank));
        }
        
        [Then(@"the Cis is in their hand")]
        public void ThenTheCisIsInTheirHand()
        {
            Assert.IsTrue(g.Game.Players[0].Hand.CardList.Contains(cis));
        }

        [When(@"HomelessGuyMode is set to false")]
        public void WhenHomelessGuyModeIsSetToFalse()
        {
            g.Game.Players[0].DrawAfterHomelessGuyMode();
        }

        [Given(@"the player has (.) manager")]
        public void GivenThePlayerHasManager(int p0)
        {
            g.Game.Players[0].Managers = p0;
        }

        [Given(@"there is a Homeless Guy card in the game")]
        public void GivenThereIsAHomelessGuyCardInTheGame()
        {
            hg = new HomelessGuy();
        }

        [When(@"the Homeless Guy card is played without a player")]
        public void WhenTheHomelessGuyCardIsPlayedWithoutAPlayer()
        {
            try
            {
                hg.PlayCard(null, g.Game);
            }
            catch (Exception e)
            {
                sl.caughtException = e;
            }
        }

        [When(@"the Homeless Guy card is played without a game")]
        public void WhenTheHomelessGuyCardIsPlayedWithoutAGame()
        {
            try
            {
                hg.PlayCard(new RHFYP.Player(""), null);
            }
            catch (Exception e)
            {
                sl.caughtException = e;
            }
        }

        [Given(@"the player has no cards")]
        public void GivenThePlayerHasNoCards()
        {
            g.Game.Players[0].Hand = new Deck();
            g.Game.Players[0].DrawPile = new Deck();
            g.Game.Players[0].DiscardPile = new Deck();
        }

    }
}
