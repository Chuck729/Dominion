using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.UnitTests
{

    [TestClass]
    public class GameTests
    {

        private MockRepository _mocks;

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        public void GenerateCards_CardIsPutIntoBuyDeck_BuyDeckNotEmpty()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.CardList.Count > 0);
        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_60FamilyBusinessesInBuyDeck()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.NumberOfPlayers = 4;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsFamilyBusiness).CardList.Count == 60 - (7*4));
        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_40CompaniesInBuyDeck()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCompany).CardList.Count == 40);
        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_30CorporationsInBuyDeck()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCorporation).CardList.Count == 30);
        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Purdues()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsPurdue).CardList.Count == 8);
        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Mits()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsMit).CardList.Count == 8);
        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Roses()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsRose).CardList.Count == 8);
        }

        [TestMethod]
        public void GenerateCards_HippieCampCardsPresent_CorrectNumberOfCurses()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);

            g.NumberOfPlayers = 2;
            g.GenerateCards();
            Assert.AreEqual((g.NumberOfPlayers - 1)*10, g.BuyDeck.SubDeck(x => x.Name == "Hippie Camp").CardList.Count);

            g.NumberOfPlayers = 4;
            g.GenerateCards();
            Assert.AreEqual((g.NumberOfPlayers - 1)*10, g.BuyDeck.SubDeck(x => x.Name == "Hippie Camp").CardList.Count);
        }

        [TestMethod]
        public void GenerateCards_IsValidDeck_17DifferentlyNamedCards()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();

            var foundNames = new List<string>();
            var count = 0;
            foreach (var card in g.BuyDeck.CardList.Where(card => !foundNames.Contains(card.Name)))
            {
                foundNames.Add(card.Name);
                count++;
            }

            Assert.AreEqual(17, count);
        }

        [TestMethod]
        public void GenerateCards_GivenActionCards_CorrectNumberOfCards()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards(new List<ICard>(new [] { new Storeroom()}));

            Assert.AreEqual(164, g.BuyDeck.CardList.Count);
        }

        [TestMethod]
        public void GenerateCards_IsValidDeck_7DifferentlyNamedCards()
        {
            var g = new Game(0);

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards(new List<ICard>(new[] { new Storeroom() }));

            var foundNames = new List<string>();
            var count = 0;
            foreach (var card in g.BuyDeck.CardList.Where(card => !foundNames.Contains(card.Name)))
            {
                foundNames.Add(card.Name);
                count++;
            }

            Assert.AreEqual(7, count);
        }

        [TestMethod]
        public void SetupPlayers_CorrectNumberOfPlayersCreated()
        {
            var g = new Game(0);

            g.SetupPlayers(new[] {"bob", "larry", "george"});
            Assert.AreEqual(3, g.NumberOfPlayers);
            Assert.AreEqual(3, g.NumberOfPlayers);
        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_Has7SmallBusinesses()
        {
            var g = new Game(0);

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});
            for (var i = 0; i < g.NumberOfPlayers; i++)
            {
                var allCards = g.Players[i].DrawPile.AppendDeck(g.Players[i].Hand.AppendDeck(g.Players[i].DiscardPile));
                Assert.AreEqual(7, allCards.SubDeck(IsFamilyBusiness).CardList.Count);
            }
        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_Has3Purdues()
        {
            var g = new Game(0);

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});
            for (var i = 0; i < g.NumberOfPlayers; i++)
            {
                var allCards = g.Players[i].DrawPile.AppendDeck(g.Players[i].Hand.AppendDeck(g.Players[i].DiscardPile));
                Assert.AreEqual(3, allCards.SubDeck(IsPurdue).CardList.Count);
            }
        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_HasCorrectNumberOfStartingCards()
        {
            var g = new Game(0);

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});
            for (var i = 0; i < g.NumberOfPlayers; i++)
            {
                var allCards = g.Players[i].DrawPile.AppendDeck(g.Players[i].Hand.AppendDeck(g.Players[i].DiscardPile));
                Assert.AreEqual(10, allCards.CardList.Count);
            }
        }

        [TestMethod]
        public void SetupPlayers_PlayersStartInCorrectMode()
        {
            var g = new Game(0);

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.AreEqual(PlayerState.Action, g.Players[0].PlayerState);

            for (var i = 1; i < g.NumberOfPlayers; i++)
            {
                Assert.AreEqual(PlayerState.TurnOver, g.Players[i].PlayerState);
            }
        }

        [TestMethod]
        public void SetupPlayers_CurrentPlayerIs0()
        {
            var g = new Game(0);

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.AreEqual(0, g.CurrentPlayer);
        }

        [TestMethod]
        public void SetupPlayers_CurrentPlayerIsValidPlayer()
        {
            var g = new Game(0);

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.IsTrue(g.Players.Count > 0);
            Assert.IsTrue(g.CurrentPlayer >= 0);
            Assert.IsTrue(g.CurrentPlayer < g.Players.Count);
        }

        [TestMethod]
        public void NextTurn_IncrementsCurrentPlayer()
        {
            var g = new Game(0);
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.AreEqual(0, g.CurrentPlayer);
            g.NextTurn();
            Assert.AreEqual(1, g.CurrentPlayer);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException),
            "There are no players in the game.")]
        public void NextTurn_NumbersOfPlayersIs0()
        {
            var game = new Game(0) {NumberOfPlayers = 0};
            game.NextTurn();
        }

        // Testing BuyCard() decision table
        // BuyCardCase......................||..1..|..2..|..3..|
        // Can afford the card..............||..F..|..T..|..T..|
        // Has at least one investment......||..X..|..F..|..T..|
        //----------------------------------||-----|-----|-----|
        // Card bought......................||..F..|..F..|..T..|

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException),
            "Must spply the player who is buying the card.")]
        public void TestBuyCard_NullPlayer()
        {
            var game = new Game(0);
            game.BuyCard("", null);
        }

        [TestMethod]
        public void TestBuyCard_PlayerHasNoInvestments_BuyCardFails()
        {
            var game = new Game(0);

            var fakePlayer = _mocks.DynamicMock<IPlayer>();

            _mocks.ReplayAll();

            fakePlayer.Investments = 0;
            Assert.IsFalse(game.BuyCard("", fakePlayer));

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestBuyCard_CardNotInBuyDeck_ReturnsFalse()
        {
            var game = new Game(0);

            var fakePlayer = _mocks.DynamicMock<Player>("");
            var fakeBuyDeck = _mocks.DynamicMock<IDeck>();

            Expect.Call(fakeBuyDeck.GetFirstCard(Arg<Predicate<ICard>>.Is.Anything)).Return(null);

            _mocks.ReplayAll();

            fakePlayer.Investments = 1;
            game.BuyDeck = fakeBuyDeck;

            Assert.IsFalse(game.BuyCard("", fakePlayer));

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestBuyCard_NotEnoughGold_ReturnsFalse()
        {
            var game = new Game(0);

            var fakePlayer = _mocks.DynamicMock<Player>("");
            var fakeBuyDeck = _mocks.DynamicMock<IDeck>();
            var cardList = new List<ICard>();
            var fakeCard = _mocks.DynamicMock<Corporation>();

            Expect.Call(fakeBuyDeck.GetFirstCard(Arg<Predicate<ICard>>.Is.Anything)).Return(fakeCard);

            _mocks.ReplayAll();

            game.BuyDeck = fakeBuyDeck;
            fakeBuyDeck.CardList = cardList;
            cardList.Add(fakeCard);

            fakePlayer.Investments = 1;
            fakePlayer.Gold = 5;

            Assert.IsFalse(game.BuyCard(fakeCard.Name, fakePlayer));
            Assert.IsTrue(cardList.Contains(fakeCard));
            Assert.AreEqual(1, fakePlayer.Investments);
            Assert.AreEqual(5, fakePlayer.Gold);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestBuyCard_CanBuyCard_CallsPlayerGiveCard()
        {
            var game = new Game(0);

            var fakePlayer = _mocks.DynamicMock<Player>("");
            var fakeBuyDeck = _mocks.DynamicMock<IDeck>();
            var cardList = new List<ICard>();
            var fakeCard = _mocks.DynamicMock<Corporation>();

            Expect.Call(fakeBuyDeck.GetFirstCard(Arg<Predicate<ICard>>.Is.Anything)).Return(fakeCard);
            Expect.Call(fakePlayer.GiveCard(fakeCard, false)).Return(true);

            _mocks.ReplayAll();

            game.BuyDeck = fakeBuyDeck;
            fakeBuyDeck.CardList = cardList;
            cardList.Add(fakeCard);

            fakePlayer.Investments = 1;
            fakePlayer.Gold = 6;

            Assert.IsTrue(game.BuyCard(fakeCard.Name, fakePlayer));
            Assert.IsTrue(cardList.Contains(fakeCard));
            Assert.AreEqual(0, fakePlayer.Investments);
            Assert.AreEqual(0, fakePlayer.Gold);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestBuyCardNullPlayer()
        {
            var game = new Game(0);
            try
            {
                game.BuyCard(null, _mocks.Stub<IPlayer>());
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestBuyCard_EnoughCoupons_BuysCardAndDoesntUseGold()
        {
            var game = new Game();

            var fakePlayer = _mocks.DynamicMock<Player>("");
            fakePlayer.Gold = 3;
            fakePlayer.Coupons = 3;
            fakePlayer.Investments = 1;

            var fakeCard = _mocks.DynamicMock<Card>();
            fakeCard.CardCost = 3;
            fakeCard.Name = "CardThatCosts3";

            game.BuyDeck.AddCard(fakeCard);

            _mocks.ReplayAll();

            Assert.IsTrue(game.BuyCard("CardThatCosts3", fakePlayer));

            Assert.AreEqual(3, fakePlayer.Gold);
            Assert.AreEqual(0, fakePlayer.Coupons);
            Assert.AreEqual(1, fakePlayer.Investments);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestRandomListOfSequentialNumbersLengthLteZero()
        {
            try
            {
                Game.RandomListOfSequentialNumbers(0);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestSetNumberOfPlayersLtZero()
        {
            var game = new Game(0);
            try
            {
                game.NumberOfPlayers = -1;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }

        }

        #region Helper Predicates

        private static bool IsFamilyBusiness(ICard card)
        {
            return (card.Name.ToLower().Equals("small business"));
        }

        private static bool IsCompany(ICard card)
        {
            return (card.Name.ToLower().Equals("company"));
        }

        private static bool IsCorporation(ICard card)
        {
            return (card.Name.ToLower().Equals("corporation"));
        }

        private bool IsRose(ICard card)
        {
            return (card.Name.ToLower().Equals("rose-hulman"));
        }

        private static bool IsMit(ICard card)
        {
            return (card.Name.ToLower().Equals("mit"));
        }

        private static bool IsPurdue(ICard card)
        {
            return (card.Name.ToLower().Equals("purdue"));
        }

        #endregion
    }
}