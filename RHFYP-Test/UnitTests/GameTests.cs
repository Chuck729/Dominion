using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.UnitTests
{

    [TestClass]
    public class GameTests
    {

        private MockRepository _mocks;

        [TestMethod]
        public void GenerateCards_CardIsPutIntoBuyDeck_BuyDeckNotEmpty()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.CardList.Count > 0);
        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_60FamilyBusinessesInBuyDeck()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.NumberOfPlayers = 4;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsFamilyBusiness).CardList.Count == 60 - (7*4));
        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_40CompaniesInBuyDeck()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCompany).CardList.Count == 40);
        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_30CorporationsInBuyDeck()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCorporation).CardList.Count == 30);
        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Purdues()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsPurdue).CardList.Count == 8);
        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Mits()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsMit).CardList.Count == 8);
        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Roses()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsRose).CardList.Count == 8);
        }

        [TestMethod]
        public void GenerateCards_HippieCampCardsPresent_CorrectNumberOfCurses()
        {
            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);

            g.NumberOfPlayers = 2;
            g.GenerateCards();
            Assert.AreEqual((g.NumberOfPlayers - 1)*10, g.BuyDeck.SubDeck(x => x.Name == "Hippie Camp").CardList.Count);

            g.NumberOfPlayers = 6;
            g.GenerateCards();
            Assert.AreEqual((g.NumberOfPlayers - 1)*10, g.BuyDeck.SubDeck(x => x.Name == "Hippie Camp").CardList.Count);

            g.NumberOfPlayers = 5;
            g.GenerateCards();
            Assert.AreEqual(40, g.BuyDeck.SubDeck(x => x.Name == "Hippie Camp").CardList.Count);
        }

        [TestMethod]
        public void GenerateCards_IsValidDeck_17DifferentlyNamedCards()
        {
            var g = new Game();

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
        public void SetupPlayers_CorrectNumberOfPlayersCreated()
        {
            var g = new Game();

            g.SetupPlayers(new[] {"bob", "larry", "george", "jacob", "marge"});
            Assert.AreEqual(5, g.Players.Count);
            Assert.AreEqual(5, g.NumberOfPlayers);

            g.SetupPlayers(new[] {"bob", "larry", "george"});
            Assert.AreEqual(3, g.NumberOfPlayers);
            Assert.AreEqual(3, g.NumberOfPlayers);
        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_Has7SmallBusinesses()
        {
            var g = new Game();

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
            var g = new Game();

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
            var g = new Game();

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
            var g = new Game();

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
            var g = new Game();

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.AreEqual(0, g.CurrentPlayer);
        }

        [TestMethod]
        public void SetupPlayers_CurrentPlayerIsValidPlayer()
        {
            var g = new Game();

            g.GenerateCards();
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.IsTrue(g.Players.Count > 0);
            Assert.IsTrue(g.CurrentPlayer >= 0);
            Assert.IsTrue(g.CurrentPlayer < g.Players.Count);
        }

        [TestMethod]
        public void NextTurn_IncrementsCurrentPlayer()
        {
            var g = new Game();
            g.SetupPlayers(new[] {"bob", "larry", "george"});

            Assert.AreEqual(0, g.CurrentPlayer);
            g.NextTurn();
            Assert.AreEqual(1, g.CurrentPlayer);
        }

        [TestMethod]
        [ExpectedException(typeof (DivideByZeroException),
            "There are no players in the game.")]
        public void NextTurn_NumbersOfPlayersIs0()
        {
            var game = new Game {NumberOfPlayers = 0};
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
            var game = new Game();
            game.BuyCard("", null);
        }

        [TestMethod]
        public void TestBuyCard_PlayerHasNoInvestments_BuyCardFails()
        {
            var game = new Game();

            var fakePlayer = _mocks.DynamicMock<IPlayer>();

            _mocks.ReplayAll();

            fakePlayer.Investments = 0;
            Assert.IsFalse(game.BuyCard("", fakePlayer));

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestBuyCard_CardNotInBuyDeck_ReturnsFalse()
        {
            var game = new Game();

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
            var game = new Game();

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
            var game = new Game();

            var fakePlayer = _mocks.DynamicMock<Player>("");
            var fakeBuyDeck = _mocks.DynamicMock<IDeck>();
            var cardList = new List<ICard>();
            var fakeCard = _mocks.DynamicMock<Corporation>();

            Expect.Call(fakeBuyDeck.GetFirstCard(Arg<Predicate<ICard>>.Is.Anything)).Return(fakeCard);
            Expect.Call(fakePlayer.GiveCard(fakeCard)).Return(true);

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
            var game = new Game();
            try
            {
                game.BuyCard(null, new FakePlayer());
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
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
            var game = new Game();
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

        [TestInitialize]
        public void Initialize()
        {
            _mocks = new MockRepository();
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

        private class FakePlayer : IPlayer
        {

            public IDeck DiscardPile
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public IDeck DrawPile
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public Game Game
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public int Gold
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public IDeck Hand
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public int Investments
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public int Managers
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public string Name
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public PlayerState PlayerState
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public int VictoryPoints
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool Winner
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public bool TreasureCardsInHand { get; }

            public bool CanAfford(ICard card)
            {
                throw new NotImplementedException();
            }

            public bool DrawCard()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Returns true if the player has at least one card of <see cref="CardType"/> action.
            /// </summary>
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public bool ActionCardsInHand { get; }

            public void EndActions()
            {
                throw new NotImplementedException();
            }

            public void EndTurn()
            {
                throw new NotImplementedException();
            }

            public bool GiveCard(ICard card)
            {
                throw new NotImplementedException();
            }

            public void PlayAllTreasures()
            {
                throw new NotImplementedException();
            }

            public bool PlayCard(ICard card)
            {
                throw new NotImplementedException();
            }

            public void StartTurn()
            {
                throw new NotImplementedException();
            }

            public bool TrashCard(ICard card)
            {
                throw new NotImplementedException();
            }

        }
    }
}