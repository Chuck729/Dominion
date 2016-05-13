using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RHFYP_Test.BottomUpIntegration.Steps
{
    [Binding]
    public class LaboratoryBottomUpSteps
    {
        private List<ICard> cards;
        private Laboratory lCard;
        private Deck deck;
        private Player p;

        [Given(@"there are no cards")]
        public void GivenThereAreNoCards()
        {
            cards = new List<ICard>();
        }

        [Given(@"there is a deck with (.*) Laboratory cards")]
        public void GivenThereIsADeckWithLaboratoryCards(int p0)
        {
            deck = new Deck();
            for (int i = 0; i < p0; i++)
            {
                deck.AddCard(new Laboratory());
            }
        }

        [Given(@"there is a Laboratory card")]
        public void GivenThereIsALaboratoryCard()
        {
            lCard = new Laboratory();
        }

        [Given(@"there is a player")]
        public void GivenThereIsAPlayer()
        {
            p = new Player("test");
        }

        [Given(@"the player has a Laboratory card in their hand")]
        public void GivenThePlayerHasALaboratoryCardInTheirHand()
        {
            lCard = new Laboratory();
            p.Hand.AddCard(lCard);
        }

        [Given(@"the player has (.*) managers")]
        public void GivenThePlayerHasManagers(int x)
        {
            p.Managers = x;
        }

        [Given(@"the player is in the Action state")]
        public void GivenThePlayerIsInTheActionState()
        {
            p.PlayerState = RHFYP.Interfaces.PlayerState.Action;
        }

        [Given(@"the player has (.*) Laboratory cards in their hand")]
        public void GivenThePlayerHasLaboratoryCardsInTheirHand(int x)
        {
            while (p.Hand.CardList.Count > x) p.Hand.DrawCard();
            while (p.Hand.CardList.Count < x) p.Hand.AddCard(new Laboratory());
        }

        [Given(@"the player has (.*) Laboratory cards in their draw pile")]
        public void GivenThePlayerHasLaboratoryCardsInTheirDrawPile(int x)
        {
            while (p.DrawPile.CardList.Count < x) p.DrawPile.AddCard(new Laboratory());
        }

        [Given(@"the player is in the Buy state")]
        public void GivenThePlayerIsInTheBuyState()
        {
            p.PlayerState = RHFYP.Interfaces.PlayerState.Buy;
        }

        private Game game;
        [Given(@"there is a game")]
        public void GivenThereIsAGame()
        {
            game = new Game(1234);
        }

        [Given(@"the game has (.*) players altogether")]
        public void GivenTheGameHasPlayersAltogether(int x)
        {
            var players = new string[x];
            for (var i = 0; i < x; i++)
            {
                players[i] = "player " + i;
            }
            game.SetupPlayers(players);
        }

        [Given(@"player (.*) has (.*) golds")]
        public void GivenPlayerHasGold(int p, int g)
        {
            game.Players[p].Gold = g;
        }

        [Given(@"player (.*) has (.*) investments total")]
        public void GivenPlayerHasInvestmentsTotal(int p, int i)
        {
            game.Players[p].Investments = i;
        }

        [Given(@"player (.*) is in the Buy state")]
        public void GivenPlayerIsInTheBuyState(int x)
        {
            game.Players[x].PlayerState = RHFYP.Interfaces.PlayerState.Buy;
        }

        [Given(@"the game has (.*) Laboratory cards for sale")]
        public void GivenTheGameHasLaboratoryCardsForSale(int x)
        {
            game.BuyDeck = new Deck();
            for (int i = 0; i < x; i++)
            {
                game.BuyDeck.AddCard(new Laboratory());
            }
        }

        [When(@"I create a Laboratory card")]
        public void WhenICreateALaboratoryCard()
        {
            lCard = new Laboratory();
            cards.Add(lCard);
        }

        [When(@"I add the Laboratory card to the deck")]
        public void WhenIAddTheLaboratoryCardToTheDeck()
        {
            deck.AddCard(lCard);
        }

        private ICard drawnCard;
        [When(@"I draw a card from the deck")]
        public void WhenIDrawACardFromTheDeck()
        {
            drawnCard = deck.DrawCard();
        }

        private bool wasPlayed;
        [When(@"the player plays the Laboratory card")]
        public void WhenThePlayerPlaysTheLaboratoryCard()
        {
            p.Game = new Game(1234);
            wasPlayed = p.PlayCard(lCard);
        }

        private Exception caughtException = null;
        [When(@"I make the number of players in the game (.*)")]
        public void WhenIMakeTheNumberOfPlayersInTheGame(int x)
        {
            try
            {
                game.NumberOfPlayers = x;
            } catch (Exception e)
            {
                caughtException = e;
            }
        }

        private bool lCardBought;
        [When(@"player (.*) buys a Laboratory card")]
        public void WhenPlayerBuysALaboratoryCard(int x)
        {
            lCardBought = game.BuyCard("Laboratory", game.Players[x]);
        }

        [Then(@"a Laboratory card is created")]
        public void ThenALaboratoryCardIsCreated()
        {
            Assert.IsTrue(cards.Contains(lCard));
        }

        [Then(@"the deck contains (.*) cards")]
        public void ThenTheDeckContainsCards(int p0)
        {
            Assert.AreEqual(p0, deck.CardList.Count);
        }

        [Then(@"the drawn card is a Laboratory card")]
        public void ThenTheDrawnCardIsALaboratoryCard()
        {
            var lab = new Laboratory();
            Assert.AreEqual(lab.Name, drawnCard.Name);
        }

        [Then(@"the player has (.*) managers")]
        public void ThenThePlayerHasManagers(int x)
        {
            Assert.AreEqual(x, p.Managers);
        }

        [Then(@"the player has (.*) cards in their hand")]
        public void ThenThePlayerHasCardsInTheirHand(int x)
        {
            Assert.AreEqual(x, p.Hand.CardList.Count);
        }

        [Then(@"the Laboratory card was played")]
        public void ThenTheLaboratoryCardWasPlayed()
        {
            Assert.IsTrue(wasPlayed);
        }

        [Then(@"the Laboratory card was not played")]
        public void ThenTheLaboratoryCardWasNotPlayed()
        {
            Assert.IsFalse(wasPlayed);
        }

        [Then(@"an ArgumentOutOfRangeException is thrown")]
        public void ThenAnArgumentOutOfRangeExceptionIsThrown()
        {
            Assert.IsTrue(caughtException is ArgumentOutOfRangeException);
        }

        [Then(@"a Laboratory card is bought")]
        public void ThenALaboratoryCardIsBought()
        {
            Assert.IsTrue(lCardBought);
        }

        [Then(@"player (.*) has (.*) gold total")]
        public void ThenPlayerHasGoldTotal(int p, int g)
        {
            Assert.AreEqual(g, game.Players[p].Gold);
        }

        [Then(@"player (.*) has (.*) investments total")]
        public void ThenPlayerHasInvestmentsTotal(int p, int i)
        {
            Assert.AreEqual(i, game.Players[p].Investments);
        }

        [Then(@"the game has (.*) Laboratory cards for sale")]
        public void ThenTheGameHasLaboratoryCardsForSale(int x)
        {
            Assert.AreEqual(x, game.BuyDeck.CardList.Count);
        }
    }
}
