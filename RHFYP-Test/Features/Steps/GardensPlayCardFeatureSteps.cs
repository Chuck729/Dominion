﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using RHFYP.Interfaces;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class GardensPlayCardFeatureSteps
    {
        public Player Player;
        private readonly GameSteps _g;
        private readonly SpeedyLoansPlayCardSteps _s;

        public GardensPlayCardFeatureSteps(GameSteps g, SpeedyLoansPlayCardSteps s)
        {
            _s = s;
            _g = g;
        }

        [Given(@"I have player")]
        public void GivenIHaveAPlayer()
        {
            Player = new Player("");
        }

        [Given(@"the player is in buy mode")]
        public void GivenThePlayerIsInBuyMode()
        {
            Player.PlayerState = PlayerState.Buy;
        }

        [Given(@"the player has (.*) Corperation cards")]
        public void GivenThePlayerHasCorperationCards(int n)
        {
            for (var i = 0; i < n; i++)
            {
                Player.Hand.CardList.Add(new Corporation());
            }
        }

        [Given(@"the player has (.*) Gardens cards")]
        public void GivenThePlayerHasGardensCards(int n)
        {
            for (var i = 0; i < n; i++)
            {
                Player.Hand.CardList.Add(new Gardens());
            }
        }

        [Given(@"the player has a trash deck")]
        public void GivenThePlayerHasATrashDeck()
        {
            Player.TrashPile = new Deck();
        }


        [When(@"the player has (.*) victory cards")]
        public void WhenThePlayerHasVictoryCard(int n)
        {
            Assert.AreEqual(n, Player.Hand.AppendDeck(Player.DiscardPile.AppendDeck(Player.DrawPile)).SubDeck(card => card.Type == CardType.Victory).CardList.Count);
        }

        [Then(@"the player should have (.*) victory points")]
        public void ThenThePlayerShouldHaveVictoryPoints(int n)
        {
            Assert.AreEqual(n, Player.VictoryPoints);
        }

        [When(@"the players turn ends")]
        public void WhenThePlayersTurnEnds()
        {
            Player.EndTurn();
        }

        private ICard _gCard;
        [Given(@"there is a Gardens card in the game")]
        public void GivenThereIsAGardensCardInTheGame()
        {
            _gCard = new Gardens();
        }

        [Given(@"the Gardens card is played without a player")]
        public void GivenTheGardensCardIsPlayedWithoutAPlayer()
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

        [Given(@"the Gardens card is played without a game")]
        public void GivenTheGardensCardIsPlayedWithoutAGame()
        {
            try
            {
                _gCard.PlayCard(new RHFYP.Player(""), null);
            }
            catch (Exception e)
            {
                _s.caughtException = e;
            }
        }

    }
}
