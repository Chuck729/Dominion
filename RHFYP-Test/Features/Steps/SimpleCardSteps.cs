using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class SimpleCardSteps
    {
        public Player _player;

        [Given(@"I have a player")]
        public void GivenIHaveAPlayer()
        {
            _player = new Player("");
        }

        [Given(@"the player has 0 cards in hand")]
        public void GivenThePlayerHasCardsInHand()
        {
            _player.Hand.CardList.Clear();
        }

        [Then(@"the player has (.*) cards in hand")]
        public void ThenThePlayerHasCardsInHand(int n)
        {
            Assert.AreEqual(n, _player.Hand.CardList.Count);
        }

        [Given(@"the player has (.*) cards in draw pile")]
        public void GivenThePlayerHasCardsInDrawPile(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _player.DrawPile.AddCard(new Corporation());
            }
        }

        [Given(@"the player has (.*) Apartment cards in hand")]
        public void GivenThePlayerHasApartmentCardInThierHand(int n)
        {
            while (_player.Hand.CardList.Count > n) _player.Hand.DrawCard();
            while (_player.Hand.CardList.Count < n) _player.Hand.AddCard(new Apartment());
        }

        [Given(@"the player has (.*) Small Business cards in hand")]
        public void GivenThePlayerHasSmallBusinessCardInThierHand(int n)
        {
            while (_player.Hand.CardList.Count > n) _player.Hand.DrawCard();
            while (_player.Hand.CardList.Count < n) _player.Hand.AddCard(new SmallBusiness());
        }

        [Then(@"draw the card, the card is a treasure")]
        public void ThenDrawTheCardTheCardIsATreasure()
        {
            Assert.AreEqual(CardType.Treasure, _player.Hand.DrawCard().Type);
        }

        #region Giving the player cards

        private SmallBusiness _smallBusiness;

        [Given(@"the player has (.*) Small Business cards")]
        public void GivenThePlayerHasSmallBusinessCards(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _smallBusiness = new SmallBusiness();
                _player.GiveCard(_smallBusiness);
            }
        }

        private Apartment _apartment;

        [Given(@"the player has (.*) Apartment cards")]
        public void GivenThePlayerHasApartmentCards(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _apartment = new Apartment();
                _player.GiveCard(_apartment);
            }
        }


        private Storeroom _storeroom;

        [Given(@"the player has (.*) Storeroom cards")]
        public void GivenThePlayerHasStoreroomCard(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _storeroom = new Storeroom();
                _player.GiveCard(_storeroom);
            }
        }

        private LawFirm _lawFirm;

        [Given(@"the player has (.*) LawFirm cards")]
        public void GivenThePlayerHasLawFirmCard(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _lawFirm = new LawFirm();
                _player.GiveCard(_lawFirm);
            }
        }

        private MilitaryBase _militaryBase;

        [Given(@"the player has (.*) MilitaryBase")]
        public void GivenThePlayerHasMilitaryBase(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _militaryBase = new MilitaryBase();
                _player.GiveCard(_militaryBase);
            }
        }

        private Bank _bank;

        [Given(@"the player has (.*) Bank cards")]
        public void GivenThePlayerHasBank(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _bank = new Bank();
                _player.GiveCard(_bank);
            }
        }

        #endregion

        #region Setting player stats

        [Given(@"the player has (.*) Gold")]
        public void GivenThePlayerHasGold(int n)
        {
            _player.Gold = n;
        }

        [Given(@"the player has (.*) Investments")]
        public void GivenThePlayerHasInvestments(int n)
        {
            _player.Investments = n;
        }

        [Given(@"the player has (.*) Managers")]
        public void GivenThePlayerHasManagers(int n)
        {
            _player.Managers = n;
        }

        #endregion

        #region Playing cards

        [When(@"the player plays the Storeroom card")]
        public void WhenThePlayerPlaysTheStoreroomCard()
        {
            _storeroom.PlayCard(_player, new Game(0));
        }

        [When(@"the player plays the LawFirm card")]
        public void WhenThePlayerPlaysTheLawFirmCard()
        {
            _lawFirm.PlayCard(_player, new Game(0));
        }

        [When(@"the player plays the MilitaryBase")]
        public void WhenThePlayerPlaysTheMilitaryBase()
        {
            _militaryBase.PlayCard(_player, new Game(0));
        }

        [When(@"the player plays the Bank card")]
        public void WhenThePlayerPlaysTheBankCard()
        {
            _bank.PlayCard(_player, new Game(0));
        }

        #endregion

        #region Player stat assertions

        [Then(@"the player has (.*) Gold")]
        public void ThenThePlayerHasGold(int n)
        {
            Assert.AreEqual(n, _player.Gold);
        }

        [Then(@"the player has (.*) Investments")]
        public void ThenThePlayerHasInvestments(int n)
        {
            Assert.AreEqual(n, _player.Investments);
        }

        [Then(@"the player has (.*) Managers")]
        public void ThenThePlayerHasManagers(int n)
        {
            Assert.AreEqual(n, _player.Managers);
        }

        #endregion
    }
}