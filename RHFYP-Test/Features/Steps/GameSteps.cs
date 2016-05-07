﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using RHFYP.Interfaces;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class GameSteps
    {
        public Game Game { get; private set; }
        
        [Given(@"I have a game")]
        public void GivenIHaveAGame()
        {
            Game = new Game();
        }

        [Given(@"I have a game with three initial types of cards")]
        public void GivenIHaveAGameWithThreeInitialTypesOfCards()
        {
            Game = new Game();
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
            while (Game.Players[player].DrawPile.CardList.Count < n) Game.Players[player].DrawPile.AddCard(new Apartment());
        }


        [Given(@"player ([0-9]) has managers")]
        public void GivenPlayerHasManagers(int player)
        {
            
            Game.Players[player].Managers = 1;
        }

        [Given(@"player ([0-9]) does not have a Military Base")]
        public void GivenPlayerDoesNotHaveAMilitaryBase(int player)
        {
            while (Game.Players[player].Hand.GetFirstCard(card => card is MilitaryBase) != null) ;
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
            while(Game.Players[player].Hand.GetFirstCard(card => card.Type == CardType.Victory) != null);
        }

        private Plug _plug;
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
    }
}
