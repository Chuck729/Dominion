using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
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

        [Given(@"player ([0-9]) has managers")]
        public void GivenPlayerHasManagers(int player)
        {
            
            Game.Players[player].Managers = 1;
        }
    }
}
