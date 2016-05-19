using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MinePlayCardSteps
    {
        private readonly GameSteps _game;

        private Apartment _apartmentCard;

        public Company CompanyCard;

        private Mine _mineCard;

        public SmallBusiness SmallBusinessCard;

        public int X;

        public MinePlayCardSteps(GameSteps game)
        {
            _game = game;
        }

        [Given(@"player ([0-9]) has a Mine card in their hand")]
        public void GivenPlayerHasAMineCardInTheirHand(int player)
        {
            _mineCard = _game.Game.Players[player].Hand.GetFirstCard(card => card.Name == "Mine") as Mine;
            if (_mineCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_mineCard);
                return;
            }
            _mineCard = new Mine();
            _game.Game.Players[player].Hand.AddCard(_mineCard);
        }

        [Given(@"player ([0-9]) doesnt have a (.*) in their hand")]
        public void GivenPlayerDoesntHaveACertainCardInTheirHand(int player, string cardName)
        {
            while (_game.Game.Players[player].Hand.GetFirstCard(card => card.Name == cardName) != null)
            {
                // Do nothing.
            }
        }

        [Given(@"player ([0-9]) is in Action mode")]
        public void GivenPlayerIsInActionMode(int player)
        {
            _game.Game.Players[player].PlayerState = PlayerState.Action;
        }

        [Given(@"player ([0-9]) has an Apartment card in their hand")]
        public void GivenPlayerHasAnApartmentCardInTheirHand(int player)
        {
            _apartmentCard = _game.Game.Players[player].Hand.GetFirstCard(card => card.Name == "Apartment") as Apartment;
            if (_apartmentCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_apartmentCard);
                return;
            }
            _apartmentCard = new Apartment();
            _game.Game.Players[player].Hand.AddCard(_apartmentCard);
        }

        [Then(@"player ([0-9]) has ([0-9]*) cards in their hand")]
        public void ThenPlayerHasCardInTheirHand(int player, int numberOfCards)
        {
            Assert.AreEqual(numberOfCards, _game.Game.Players[player].Hand.CardList.Count);
        }

        [Then(@"player ([0-9]) has a (.*) card in their hand")]
        public void ThenPlayerHasASpecificCardInTheirHand(int player, string cardName)
        {
            var c = _game.Game.Players[player].Hand.GetFirstCard(card => card.Name == cardName);
            Assert.IsNotNull(c);
            _game.Game.Players[player].Hand.AddCard(c);
        }

        [When(@"player ([0-9]) plays the Mine card")]
        public void WhenPlayerPlaysTheMineCard(int player)
        {
            _game.Game.Players[player].PlayCard(_mineCard);
        }

        [Given(@"x is the number of cards player ([0-9]) has")]
        public void GivenXIsTheNumberOfCardsPlayerHas(int player)
        {
            X = _game.Game.Players[player].Hand.CardList.Count;
        }

        [Then(@"x - 1 is the number of cards player ([0-9]) has")]
        public void ThenXIsTheNumberOfCardsPlayerHas(int player)
        {
            Assert.AreEqual(X - 1, _game.Game.Players[player].Hand.CardList.Count);
        }

        [Given(@"player ([0-9]) has a Small Business in their hand")]
        public void GivenPlayerHasASmallBusinessInTheirHand(int player)
        {
            SmallBusinessCard =
                _game.Game.Players[player].Hand.GetFirstCard(card => card.Name == "Small Business") as SmallBusiness;
            if (SmallBusinessCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(SmallBusinessCard);
                return;
            }
            SmallBusinessCard = new SmallBusiness();
            _game.Game.Players[player].Hand.AddCard(SmallBusinessCard);
        }

        [Given(@"player ([0-9]) has a Company in their hand")]
        public void GivenPlayerHasACompanyInTheirHand(int player)
        {
            CompanyCard = _game.Game.Players[player].Hand.GetFirstCard(card => card.Name == "Company") as Company;
            if (CompanyCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(CompanyCard);
                return;
            }
            CompanyCard = new Company();
            _game.Game.Players[player].Hand.AddCard(CompanyCard);
        }


        [Then(@"player ([0-9]) cant play the Mine card")]
        public void ThenPlayerCantPlayTheMineCard(int player)
        {
            Assert.IsFalse(_game.Game.Players[player].PlayCard(_mineCard));
        }
    }
}