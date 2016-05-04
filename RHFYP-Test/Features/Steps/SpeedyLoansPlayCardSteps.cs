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
        [Given(@"player ([0-9]) has a SpeedyLoans in thier hand")]
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
     
        [Then(@"player ([0-9]) small business card is put in the trash pile")]
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

        [Given(@"player ([0-9]) does not have a small business in their hand")]
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

    }
}
