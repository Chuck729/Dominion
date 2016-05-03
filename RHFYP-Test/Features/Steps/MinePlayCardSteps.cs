using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;
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
            if (_apartmentCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_apartmentCard);
                return;
            }
            _apartmentCard = new Apartment();
            _game.Game.Players[player].Hand.AddCard(_apartmentCard);
        }

        [Then(@"player ([0-9]) has ([0-9]*) cards in thier hand")]
        public void ThenPlayerHasCardInThierHand(int player, int numberOfCards)
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
            player--;
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

        private SmallBusiness _smallBusinessCard;
        [Given(@"player ([0-9]) has a Small Business in their hand")]
        public void GivenPlayerHasASmallBusinessInTheirHand(int player)
        {
            player--;
            _smallBusinessCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Small Business") as SmallBusiness;
            if (_smallBusinessCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_smallBusinessCard);
                return;
            }
            _smallBusinessCard = new SmallBusiness();
            _game.Game.Players[player].Hand.AddCard(_smallBusinessCard);
        }

        private Company _companyCard;
        [Given(@"player ([0-9]) has a Company in their hand")]
        public void GivenPlayerHasACompanyInTheirHand(int player)
        {
            _companyCard = _game.Game.Players[player].Hand.GetFirstCard(x => x.Name == "Company") as Company;
            if (_companyCard != null)
            {
                _game.Game.Players[player].Hand.AddCard(_companyCard);
                return;
            }
            _companyCard = new Company();
            _game.Game.Players[player].Hand.AddCard(_companyCard);
        }


        [Then(@"player ([0-9]) cant play the Mine card")]
        public void ThenPlayerCantPlayTheMineCard(int player)
        {
            Assert.IsFalse(_game.Game.Players[player].PlayCard(_mineCard));
        }

    }
}
