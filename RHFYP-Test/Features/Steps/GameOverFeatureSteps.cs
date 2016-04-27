using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class GameOverFeatureSteps
    {
        private Game _game;

        [Given(@"I have a game")]
        public void GivenIHaveAGame()
        {
            _game = new Game();
        }
        
        [Given(@"there are no (.*) cards left in the buy deck")]
        public void GivenThereAreNoXCardsLeftInTheBuyDeck(string cardName)
        {
            while (_game.BuyDeck.GetFirstCard(x => x.Name == cardName) != null)
            {
            }
        }

        [Given(@"its the end of someones turn")]
        public void GivenItsTheEndOfSomeonesTurn()
        {
            _game.NextTurn();
        }

        [Given(@"the game has ([0-9]) players")]
        public void GivenTheGameHasTwoPlayers(int numberOfPlayers)
        {
            var players = new string[numberOfPlayers];
            for (var i = 0; i < numberOfPlayers; i++)
            {
                players[i] = "player " + i;
            }
            _game.SetupPlayers(players);
        }

        [Given(@"player ([0-9]) has a Purdue card")]
        public void GivenPlayerHasAPurdueCard(int player)
        {
            _game.Players[player].GiveCard(new Purdue());
        }

        [Then(@"the game should be over")]
        public void ThenTheGameShouldBeOver()
        {
            Assert.IsTrue(_game.GameState == GameState.Ended);
        }

        [Then(@"player ([0-9]) should win")]
        public void ThenPlayerXShouldWin(int player)
        {
            Assert.IsTrue(_game.Players[player].Winner);
        }

        [Given(@"a Rose-Hulman card is added to the buy deck")]
        public void GivenThereIsARose_HulmanCardInTheBuyDeck()
        {
            _game.BuyDeck.AddCard(new Rose());
        }

        [Given(@"([0-9]+) cards are drawn from the buy deck")]
        public void GivenXCardsAreDrawnFromTheBuyDeck(int numberOfCards)
        {
            for (var i = 0; i < numberOfCards; i++)
            {
                _game.BuyDeck.DrawCard();
            }
        }

        [Given(@"I have a game with three initial types of cards")]
        public void GivenIHaveAGameWithThreeInitialTypesOfCards()
        {
            _game = new Game();
            _game.BuyDeck.AddCard(new Corporation());
            _game.BuyDeck.AddCard(new Purdue());
            _game.BuyDeck.AddCard(new Mit());
            _game.BuyDeck.SetDefaultCardList();
        }

    }
}
