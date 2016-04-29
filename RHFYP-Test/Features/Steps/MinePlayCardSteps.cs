using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class MinePlayCardSteps
    {
        private readonly GameSteps _game;
        public MinePlayCardSteps(GameSteps game)
        {
            _game = game;
        }

        private Mine _mineCard;
        [Given(@"player ([0-9]) has a Mine card in their hand")]
        public void GivenPlayerHasAMineCardInThierHand(int player)
        {
            _mineCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Mine") as Mine;
            if (_mineCard != null) return;
            _mineCard = new Mine();
            _game.Game.Players[player].Hand.AddCard(_mineCard);
        }

        [Given(@"player ([0-9]) doesnt have a (.*) in their hand")]
        public void GivenPlayerDoesntHaveACertainCardInTheirHand(int player, string cardName)
        {
            while (_game.Game.Players[player].Hand.GetFirstCard(x => x.Name == cardName) != null)
            {
                // Do nothing.
            }
        }

        [Given(@"player ([0-9]) is in Action mode")]
        public void GivenPlayerIsInActionMode(int player)
        {
            _game.Game.Players[player].PlayerState = PlayerState.Action;
        }

        private Apartment _apartmentCard;
        [Given(@"player ([0-9]) has an Apartment card in their hand")]
        public void GivenPlayerHasAnApartmentCardInTheirHand(int player)
        {
            _apartmentCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Apartment") as Apartment;
            if (_mineCard != null) return;
            _apartmentCard = new Apartment();
            _game.Game.Players[player].Hand.AddCard(_apartmentCard);
        }

        [Then(@"player ([0-9]) has ([0-9]*) cards in thier hand")]
        public void ThenPlayerHasCardInThierHand(int player, int numberOfCards)
        {
            Assert.AreEqual(numberOfCards, _game.Game.Players[player].Hand.CardList.Count);
        }

        [Then(@"player ([0-9]) has an (.*) card in their hand")]
        public void ThenPlayerHasASpecificCardInTheirHand(int player, string cardName)
        {
            Assert.IsNotNull(_game.Game.Players[player].Hand.GetFirstCard(x => x.Name == cardName));
        }

        [When(@"player ([0-9]) plays the Mine card")]
        public void WhenPlayerPlaysTheMineCard(int player)
        {
            _game.Game.Players[player].PlayCard(_mineCard);
        }

        private int x;
        [Given(@"x is the number of cards player ([0-9]) has")]
        public void GivenXIsTheNumberOfCardsPlayerHas(int player)
        {
            x = _game.Game.Players[player].Hand.CardList.Count;
        }

        [Then(@"x - 1 is the number of cards player ([0-9]) has")]
        public void ThenXIsTheNumberOfCardsPlayerHas(int player)
        {
            Assert.AreEqual(x - 1, _game.Game.Players[player].Hand.CardList.Count);
        }

    }
}
