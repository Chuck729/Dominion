using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class PlayerDeckIntegrationFeatureSteps
    {
        private readonly List<Player> _players = new List<Player>();

        [Given(@"I have (.) players")]
        public void GivenIHavePlayer(int p0)
        {
            for (var i = 0; i < p0; i++)
            {
                _players.Add(new Player("" + p0));
            }
        }

        [Given(@"player (.) has a Corporation in their hand")]
        public void GivenPlayerHasACorporationInTheirHand(int p0)
        {
            _players[p0].Hand.AddCard(new Corporation());
        }

        [Given(@"player (.) has a SmallBusiness in their hand")]
        public void GivenPlayerHasASmallBusinessInTheirHand(int p0)
        {
            _players[p0].Hand.AddCard(new SmallBusiness());
        }

        [Given(@"player (.) has a Bank in their hand")]
        public void GivenPlayerHasABankInTheirHand(int p0)
        {
            _players[p0].Hand.AddCard(new Bank());
        }

        [When(@"palyer (.) plays all treaures")]
        public void WhenPalyerPlaysAllTreaures(int p0)
        {
            _players[p0].PlayAllTreasures();
        }

        [Then(@"player (.) has (.) card in their hand")]
        public void ThenPlayerHasCardInTheirHand(int p0, int p1)
        {
            if (p0 < 0) throw new ArgumentOutOfRangeException(nameof(p0));
            if (p1 < 0) throw new ArgumentOutOfRangeException(nameof(p1));
            Assert.IsTrue(_players[p0].Hand.CardList.Count == p1);
        }

        [Then(@"player (.) has a Bank in their hand")]
        public void ThenPlayerHasABankInTheirHand(int p0)
        {
            Assert.IsNotNull(_players[p0].Hand.GetFirstCard(c => c.Name.Equals("Bank")));
        }
    }
}