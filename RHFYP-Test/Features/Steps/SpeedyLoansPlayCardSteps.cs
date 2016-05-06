using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class SpeedyLoansPlayCardSteps
    {
        private readonly GameSteps _game;
        public SpeedyLoansPlayCardSteps(GameSteps game)
        {
            _game = game;
        }
        private SpeedyLoans _speedyLoansCard;
        [Given(@"player ([0-9]) has a SpeedyLoans in their hand")]
        public void GivenPlayerHasASpeedyLoansInThierHand(int player)
        {
            
            _speedyLoansCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "SpeedyLoans") as SpeedyLoans;
            if (_speedyLoansCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_speedyLoansCard);
                return;
            }
            _speedyLoansCard = new SpeedyLoans();
            _game.Game.Players[player].Hand.AddCard(_speedyLoansCard);
        }
        
        [When(@"player ([0-9]) plays the SpeedyLoans card")]
        public void WhenPlayerPlaysTheSpeedyLoansCard(int player)
        {
            
            _game.Game.Players[player].PlayCard(_speedyLoansCard);
        }
     
        [Then(@"player ([0-9]) Small Business card is put in the trash pile")]
        public void ThenPlayerSmallBusinessCardIsPutInTheTrashPile(int player)
        {
            
            ICard smallBusiness = _game.Game.Players[player].TrashPile.GetFirstCard(card => card is SmallBusiness);
            if (smallBusiness != null)
                _game.Game.Players[player].TrashPile.CardList.Add(smallBusiness);
            Assert.IsTrue(_game.Game.Players[player].TrashPile.CardList.Contains(smallBusiness));
        }

        [Then(@"player ([0-9]) SpeedyLoans is discarded")]
        public void ThenPlayerSpeedyLoansIsDiscarded(int player)
        {
            
            Assert.IsTrue(_game.Game.Players[player].DiscardPile.CardList.Contains(_speedyLoansCard));
        }

        [Given(@"player ([0-9]) does not have a Small Business in their hand")]
        public void GivenPlayerDoesNotHaveASmallBusinessInTheirHand(int player)
        {
            
            while (_game.Game.Players[player].Hand.GetFirstCard(card => card is SmallBusiness) != null) ;
        }

        [Given(@"player (.[0-9]*) has ([0-9]) gold")]
        public void GivenPlayerHasGold(int player, int gold)
        {
            
            _game.Game.Players[player].Gold = gold;
        }

        [Then(@"player ([0-9]) has ([0-9]) gold")]
        public void ThenPlayerHasGold(int player, int gold)
        {
            
            Assert.AreEqual(gold, _game.Game.Players[player].Gold);
        }

        private ICard _sCard;
        [Given(@"there is a SpeedyLoans card in the game")]
        public void GivenThereIsASpeedyLoansCardInTheGame()
        {
            _sCard = new SpeedyLoans();
        }

        [Given(@"the SpeedyLoans card is played without a player")]
        public void GivenTheSpeedyLoansCardIsPlayedWithoutAPlayer()
        {
            try {
                _sCard.PlayCard(null, _game.Game);
            } catch (Exception e)
            {
                caughtException = e;
            }
        }

        public Exception caughtException = null;
        [Then(@"An ArgumentNullException is thrown")]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            Assert.IsTrue(caughtException is ArgumentNullException);
        }

        [Given(@"the SpeedyLoans card is played without a game")]
        public void GivenTheSpeedyLoansCardIsPlayedWithoutAGame()
        {
            try
            {
                _sCard.PlayCard(_game.Game.Players[0], null);
            }
            catch (Exception e)
            {
                caughtException = e;
            }
        }



    }
}
