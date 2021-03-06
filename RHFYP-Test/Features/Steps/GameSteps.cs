﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using RHFYP.Interfaces;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class GameSteps
    {

        private ConstructionSite _constructionSite;

        private ICollection<ICard> _hand;

        private Plug _plug;

        private StartUp _startUp;

        private Subdivision _subdivision;

        private WallStreet _wallStreet;
        public Game Game { get; private set; }

        [Given(@"I have a game")]
        public void GivenIHaveAGame()
        {
            Game = new Game(new Random().Next());
        }

        [Given(@"the game has a default deck")]
        public void GivenTheGameHasADefaultDeck()
        {
            Game.GenerateCards();
        }

        [Given(@"I have a game with three initial types of cards")]
        public void GivenIHaveAGameWithThreeInitialTypesOfCards()
        {
            Game = new Game(new Random().Next());
            Game.BuyDeck.AddCard(new Corporation());
            Game.BuyDeck.AddCard(new Purdue());
            Game.BuyDeck.AddCard(new Mit());
            Game.BuyDeck.SetDefaultCardList();
        }


        [Given(@"there are no (.*) cards left in the buy deck")]
        public void GivenThereAreNoXCardsLeftInTheBuyDeck(string cardName)
        {
            while (Game.BuyDeck.GetFirstCard(x => x.Name == cardName) != null)
            {
            }
        }

        [Given(@"its the end of someones turn")]
        public void GivenItsTheEndOfSomeonesTurn()
        {
            Game.NextTurn();
        }

        [Given(@"the game has ([0-9]) players")]
        public void GivenTheGameHasTwoPlayers(int numberOfPlayers)
        {
            var players = new string[numberOfPlayers];
            for (var i = 0; i < numberOfPlayers; i++)
            {
                players[i] = "player " + i;
            }
            Game.SetupPlayers(players);
        }

        [Given(@"player (.*) has (.*) Manager")]
        public void GivenPlayerHasManager(int player, int n)
        {
            Game.Players[player].Managers = n;
        }

        [Given(@"player (.*) has a Wall Street card")]
        public void GivenPlayerHasAWallStreetCard(int player)
        {
            _wallStreet = new WallStreet();
            Game.Players[player].Hand.AddCard(_wallStreet);
        }

        [Given(@"player (.*) has x cards in thier hand")]
        public void GivenPlayerHasXCardsInThierHand(int player)
        {
            _hand = new List<ICard>(Game.Players[player].Hand.CardList);
        }

        [When(@"player (.*) plays the Wall Street card")]
        public void WhenPlayerPlaysTheWallStreetCard(int player)
        {
            _wallStreet.PlayCard(Game.Players[player], Game);
        }

        [When(@"player (.*) ends thier turn")]
        public void WhenPlayerEndsThierTurn(int player)
        {
            Game.Players[player].EndTurn();
        }


        [Then(@"x cards are all on the top of player (.*) draw pile")]
        public void ThenXCardsAreAllOnTheTopOfPlayerDrawPile(int player)
        {
            // ReSharper disable once UnusedVariable
            foreach (var card in _hand)
            {
                Assert.IsTrue(_hand.Contains(Game.Players[player].DrawPile.DrawCard()));
            }
        }


        [Given(@"player ([0-9]) has a Purdue card")]
        public void GivenPlayerHasAPurdueCard(int player)
        {
            Game.Players[player].GiveCard(new Purdue());
        }

        [Given(@"a Rose-Hulman card is added to the buy deck")]
        public void GivenThereIsARose_HulmanCardInTheBuyDeck()
        {
            Game.BuyDeck.AddCard(new Rose());
        }

        [Given(@"([0-9]+) cards are drawn from the buy deck")]
        public void GivenXCardsAreDrawnFromTheBuyDeck(int numberOfCards)
        {
            for (var i = 0; i < numberOfCards; i++)
            {
                Game.BuyDeck.DrawCard();
            }
        }

        [Given(@"player ([0-9]) does not have managers")]
        public void GivenPlayerDoesNotHaveManagers(int player)
        {
            Game.Players[player].Managers = 0;
        }

        [Given(@"player (.*) has (.*) cards in hand")]
        public void GivenPlayerHasCardsInHand(int player, int n)
        {
            while (Game.Players[player].Hand.CardList.Count > n) Game.Players[player].Hand.DrawCard();
            while (Game.Players[player].Hand.CardList.Count < n) Game.Players[player].Hand.AddCard(new Apartment());
        }

        [Given(@"player (.*) has (.*) cards in draw pile")]
        public void GivenPlayerHasCardsInDrawPile(int player, int n)
        {
            while (Game.Players[player].DrawPile.CardList.Count > n) Game.Players[player].DrawPile.DrawCard();
            while (Game.Players[player].DrawPile.CardList.Count < n)
                Game.Players[player].DrawPile.AddCard(new Apartment());
        }


        [Given(@"player ([0-9]) has managers")]
        public void GivenPlayerHasManagers(int player)
        {
            Game.Players[player].Managers = 1;
        }

        [Given(@"player ([0-9]) does not have a Military Base")]
        public void GivenPlayerDoesNotHaveAMilitaryBase(int player)
        {
            while (Game.Players[player].Hand.GetFirstCard(card => card is MilitaryBase) != null)
            {
            }
        }

        [Then(@"player ([0-9]) has a Company card on top of their draw pile")]
        public void ThenPlayerHasACompanyCardOnTopOfTheirDrawPile(int player)
        {
            var numCards = Game.Players[player].DrawPile.CardList.Count;
            var index = Game.Players[player].DrawPile.CardList.FindLastIndex(card => card is Company);
            Assert.AreEqual(index, numCards - 1);
        }

        [Then(@"player ([0-9]) has a Purdue card on top of their draw pile")]
        public void ThenPlayerHasAPurdueCardOnTopOfTheirDrawPile(int player)
        {
            var numCards = Game.Players[player].DrawPile.CardList.Count;
            var index = Game.Players[player].DrawPile.CardList.FindLastIndex(card => card is Purdue);
            Assert.AreEqual(index, numCards - 1);
        }

        [Given(@"player ([0-9]) does not have a Victory card")]
        public void GivenPlayerDoesNotHaveAVictoryCard(int player)
        {
            while (Game.Players[player].Hand.GetFirstCard(card => card.Type == CardType.Victory) != null)
            {
            }
        }

        [Given(@"player (.*) has a Plug card")]
        public void GivenPlayerHasAPlugCard(int player)
        {
            _plug = new Plug();
            Game.Players[player].GiveCard(_plug);
        }

        [Given(@"player (.*) has no cards to draw")]
        public void GivenPlayerHasNoCardsToDraw(int player)
        {
            while (Game.Players[player].DrawCard())
            {
            }
        }

        [When(@"player (.*) plays the Plug card")]
        public void WhenPlayerPlaysThePlugCard(int player)
        {
            _plug.PlayCard(Game.Players[player], Game);
        }

        [Then(@"player (.*) has (.*) of (.*) cards")]
        public void ThenPlayerHasHippieCampCards(int playerIndex, int n, string cardName)
        {
            IPlayer player = Game.Players[playerIndex];
            var allCards = player.Hand.AppendDeck(player.DiscardPile.AppendDeck(player.DrawPile));
            Assert.AreEqual(n, allCards.SubDeck(card => card.Name == cardName).CardList.Count);
        }

        [Then(@"player (.*) has (.*) cards in hand")]
        public void ThenPlayerHasCardsInHand(int player, int n)
        {
            Assert.AreEqual(n, Game.Players[player].Hand.CardList.Count);
        }

        [Given(@"player (.*) has more than (.*) coupons")]
        public void GivenPlayerHasMoreThanCoupons(int player, int n)
        {
            Game.Players[player].Coupons = n + 1;
        }

        [Given(@"player (.*) has a StartUp card")]
        public void GivenPlayerHasAStartUpCard(int player)
        {
            _startUp = new StartUp();
            Game.Players[player].Hand.AddCard(_startUp);
        }

        [Then(@"player (.*) cant play the StartUp card")]
        public void ThenPlayerCantPlayTheStartUpCard(int player)
        {
            Assert.IsFalse(Game.Players[player].PlayCard(_startUp));
        }


        [Given(@"player (.*) has (.*) coupons")]
        public void GivenPlayerHasCoupons(int player, int n)
        {
            Game.Players[player].Coupons = n;
        }

        [When(@"player (.*) plays the StartUp card")]
        public void WhenPlayerPlaysTheStartUpCard(int player)
        {
            Game.Players[player].PlayCard(_startUp);
        }

        [Then(@"player (.*) has (.*) coupons")]
        public void ThenPlayerHasCoupons(int player, int n)
        {
            Assert.AreEqual(n, Game.Players[player].Coupons);
        }

        [Then(@"player (.*) has no (.*) cards")]
        public void ThenPlayerHasNoStartUpCards(int player, string cardName)
        {
            Assert.AreEqual(0,
                Game.Players[player].Hand.AppendDeck(
                    Game.Players[player].DrawPile.AppendDeck(Game.Players[player].DiscardPile))
                    .SubDeck(card => card.Name == cardName)
                    .CardList.Count);
        }

        [Given(@"player (.*) has (.*) Managers")]
        public void GivenPlayerHasManagers(int player, int n)
        {
            Game.Players[player].Managers = n;
        }

        [Given(@"player (.*) has a ConstructionSite card")]
        public void GivenPlayerHasAConstructionSiteCard(int player)
        {
            _constructionSite = new ConstructionSite();
            Game.Players[player].Hand.AddCard(_constructionSite);
        }

        [Then(@"player (.*) cant play the ConstructionSite card")]
        public void ThenPlayerCantPlayTheConstructionSiteCard(int player)
        {
            Assert.IsFalse(Game.Players[player].PlayCard(_constructionSite));
        }

        [When(@"player (.*) plays the ConstructionSite card")]
        public void WhenPlayerPlaysTheConstructionSiteCard(int player)
        {
            Game.Players[player].PlayCard(_constructionSite);
        }

        [Given(@"player (.*) has a Subdivision")]
        public void GivenPlayerHasASubdivision(int player)
        {
            _subdivision = new Subdivision();
            Game.Players[player].Hand.AddCard(_subdivision);
        }

        [When(@"player (.*) plays the Subdivision")]
        public void WhenPlayerPlaysTheSubdivision(int player)
        {
            _subdivision.PlayCard(Game.Players[player], Game);
        }
    }
}