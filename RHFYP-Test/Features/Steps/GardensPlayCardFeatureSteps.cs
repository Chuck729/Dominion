using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class GardensPlayCardFeatureSteps
    {
        private readonly GameSteps _g;
        private readonly SpeedyLoansPlayCardSteps _s;

        private ICard _gCard;

        public GardensPlayCardFeatureSteps(GameSteps g, SpeedyLoansPlayCardSteps s)
        {
            _s = s;
            _g = g;
        }

        [Given(@"player (.*) has (.*) Corporation cards")]
        public void GivenPlayerxHasyCorporationCards(int x, int y)
        {
            for (var i = 0; i < y; i++)
            {
                _g.Game.Players[x].Hand.CardList.Add(new Corporation());
            }
        }

        [Given(@"player (.*) has (.*) Company cards")]
        public void GivenPlayerxHasyCompanyCards(int x, int y)
        {
            for (var i = 0; i < y; i++)
            {
                _g.Game.Players[x].Hand.CardList.Add(new Company());
            }
        }

        [Given(@"player (.*) has (.*) Gardens cards")]
        public void GivenPlayerXHasYGardensCards(int x, int y)
        {
            for (var i = 0; i < y; i++)
            {
                _g.Game.Players[x].Hand.CardList.Add(new Gardens());
            }
        }

        [Given(@"player (.*) has a trash deck")]
        public void GivenPlayerXHasATrashDeck(int x)
        {
            _g.Game.Players[x].TrashPile = new Deck();
        }


        [Then(@"player (.*) should have (.*) victory cards")]
        public void ThenPlayerXShouldHaveYVictoryCard(int x, int y)
        {
            Assert.AreEqual(y,
                _g.Game.Players[x].Hand.AppendDeck(_g.Game.Players[x].DiscardPile.AppendDeck(_g.Game.Players[x].DrawPile))
                    .SubDeck(card => card.Type == CardType.Victory)
                    .CardList.Count);
        }

        [Then(@"player (.*) should have (.*) victory points")]
        public void ThenPlayerXShouldHaveYVictoryPoints(int x, int y)
        {
            Assert.AreEqual(y, _g.Game.Players[x].VictoryPoints);
        }

        [When(@"player (.*) turn ends")]
        public void WhenPlayerXTurnEnds(int x)
        {
            _g.Game.Players[x].EndTurn();
        }

        [Given(@"there is a Gardens card in the game")]
        public void GivenThereIsAGardensCardInTheGame()
        {
            _gCard = new Gardens();
        }

        [When(@"the Gardens card is played without a player")]
        public void WhenTheGardensCardIsPlayedWithoutAPlayer()
        {
            try
            {
                _gCard.PlayCard(null, _g.Game);
            }
            catch (Exception e)
            {
                _s.caughtException = e;
            }
        }

        [When(@"the Gardens card is played without a game")]
        public void WhenTheGardensCardIsPlayedWithoutAGame()
        {
            try
            {
                _gCard.PlayCard(new Player(""), null);
            }
            catch (Exception e)
            {
                _s.caughtException = e;
            }
        }
    }
}