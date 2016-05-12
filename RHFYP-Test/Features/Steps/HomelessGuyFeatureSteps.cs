using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class HomelessGuyFeatureSteps
    {

        SimpleCardSteps s;

        public HomelessGuyFeatureSteps(SimpleCardSteps simpleTest)
        {
            s = simpleTest;
        }

        ICard sb = new SmallBusiness();
        [Given(@"the player has a small business in their hand")]
        public void GivenThePlayerHasASmallBusinessInTheirHand()
        {
            s._player.Hand.AddCard(sb);
        }

        ICard comp = new Company();
        [Given(@"the player has a company in their hand")]
        public void GivenThePlayerHasACompanyInTheirHand()
        {
            s._player.Hand.AddCard(comp);
        }

        ICard cor = new Corporation();
        [Given(@"the player has a corporation in their hand")]
        public void GivenThePlayerHasACorporationInTheirHand()
        {
            s._player.Hand.AddCard(cor);
        }

        ICard hg = new HomelessGuy();
        [Given(@"the player has a homelessguy in their hand")]
        public void GivenThePlayerHasAHomelessguyInTheirHand()
        {
            s._player.Hand.AddCard(hg);
        }

        ICard bank = new Bank();
        [Given(@"the player has a bank in their draw deck")]
        public void GivenThePlayerHasABankInTheirDrawDeck()
        {
            s._player.DrawPile.AddCard(bank);
        }

        ICard cis = new Cis();
        [Given(@"the player has a Cis in thier draw deck")]
        public void GivenThePlayerHasACisInThierDrawDeck()
        {
            s._player.DrawPile.AddCard(cis);
        }
        
        [When(@"the player plays the homeless guy")]
        public void WhenThePlayerPlaysTheHomelessGuy()
        {
            s._player.PlayCard(hg);
        }
        
        [When(@"the player chooses to discard the small business")]
        public void WhenThePlayerChoosesToDiscardTheSmallBusiness()
        {
            s._player.PlayCard(sb);
        }
        
        [When(@"the player chooses to discard the company")]
        public void WhenThePlayerChoosesToDiscardTheCompany()
        {
            s._player.PlayCard(comp);
        }
        
        [Then(@"the small business is not in their hand")]
        public void ThenTheSmallBusinessIsNotInTheirHand()
        {
            Assert.IsFalse(s._player.Hand.CardList.Contains(sb));
        }
        
        [Then(@"the company is not in their hand")]
        public void ThenTheCompanyIsNotInTheirHand()
        {
            Assert.IsFalse(s._player.Hand.CardList.Contains(comp));
        }
        
        [Then(@"the bank is in their hand")]
        public void ThenTheBankIsInTheirHand()
        {
            Assert.IsTrue(s._player.Hand.CardList.Contains(bank));
        }
        
        [Then(@"the Cis is in their hand")]
        public void ThenTheCisIsInTheirHand()
        {
            Assert.IsTrue(s._player.Hand.CardList.Contains(bank));
        }

        [When(@"HomelessGuyMode is set to false")]
        public void WhenHomelessGuyModeIsSetToFalse()
        {
            s._player.DrawAfterHomelessGuyMode();
        }

        [Given(@"the player has (.) manager")]
        public void GivenThePlayerHasManager(int p0)
        {
            s._player.Managers = p0;
        }


    }
}
