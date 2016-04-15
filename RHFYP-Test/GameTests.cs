﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System.Reflection;

namespace RHFYP_Test
{

    [TestClass]
    public class GameTests
    {

        private MockRepository mocks;

        [TestMethod]
        public void GenerateCards_CardIsPutIntoBuyDeck_BuyDeckNotEmpty()
        {

            var g  = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.CardList.Count > 0);

        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_60FamilyBusinessesInBuyDeck()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.NumberOfPlayers = 4;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsFamilyBusiness).CardList.Count == 60 - (7 * 4));

        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_40CompaniesInBuyDeck()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCompany).CardList.Count == 40);

        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_30CorporationsInBuyDeck()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCorporation).CardList.Count == 30);

        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Purdues()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsPurdue).CardList.Count == 8);

        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Mits()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsMit).CardList.Count == 8);

        }

        [TestMethod]
        public void GenerateCards_VictoryCardsPresent_8Roses()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsRose).CardList.Count == 8);

        }

        [TestMethod]
        public void GenerateCards_HippieCampCardsPresent_CorrectNumberOfCurses()
        {

            var g = new game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);

            g.NumberOfPlayers = 2;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsHippieCamp).CardList.Count == (g.NumberOfPlayers - 1) * 10);

            g.NumberOfPlayers = 6;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsHippieCamp).CardList.Count == (g.NumberOfPlayers - 1) * 10);

            g.NumberOfPlayers = 5;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsHippieCamp).CardList.Count == 40);

        }

        [TestMethod]
        public void GenerateCards_IsValidDeck_17DifferentlyNamedCards()
        {

            var g = new game();

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

            var g = new game();

            g.SetupPlayers(new []{"bob", "larry", "george", "jacob", "marge"});
            Assert.AreEqual(5, g.Players.Count);
            Assert.AreEqual(5, g.NumberOfPlayers);

            g.SetupPlayers(new []{ "bob", "larry", "george" });
            Assert.AreEqual(3, g.NumberOfPlayers);
            Assert.AreEqual(3, g.NumberOfPlayers);

        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_Has7SmallBusinesses()
        {

            var g = new game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });
            for (var i = 0; i < g.NumberOfPlayers; i++)
            {
                var allCards = g.Players[i].DrawPile.AppendDeck(g.Players[i].Hand.AppendDeck(g.Players[i].DiscardPile));
                Assert.AreEqual(7, allCards.SubDeck(IsFamilyBusiness).CardCount());
            }

        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_Has3Purdues()
        {

            var g = new game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });
            for (var i = 0; i < g.NumberOfPlayers; i++)
            {
                var allCards = g.Players[i].DrawPile.AppendDeck(g.Players[i].Hand.AppendDeck(g.Players[i].DiscardPile));
                Assert.AreEqual(3, allCards.SubDeck(IsPurdue).CardCount());
            }
        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_HasCorrectNumberOfStartingCards()
        {

            var g = new game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });
            for (var i = 0; i < g.NumberOfPlayers; i++)
            {
                var allCards = g.Players[i].DrawPile.AppendDeck(g.Players[i].Hand.AppendDeck(g.Players[i].DiscardPile));
                Assert.AreEqual(10, allCards.CardCount());
            }

        }

        [TestMethod]
        public void SetupPlayers_PlayersStartInCorrectMode()
        {

            var g = new game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });

            Assert.AreEqual(PlayerState.Action, g.Players[0].PlayerState);
            
            for (var i = 1; i < g.NumberOfPlayers; i++)
            {
                Assert.AreEqual(PlayerState.TurnOver, g.Players[i].PlayerState);
            }

        }

        [TestMethod]
        public void SetupPlayers_CurrentPlayerIs0()
        {

            var g = new game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });

            Assert.AreEqual(0, g.CurrentPlayer);

        }

        [TestMethod]
        public void SetupPlayers_CurrentPlayerIsValidPlayer()
        {

            var g = new game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });

            Assert.IsTrue(g.Players.Count > 0);
            Assert.IsTrue(g.CurrentPlayer >= 0);
            Assert.IsTrue(g.CurrentPlayer < g.Players.Count);

        }

        [TestMethod]
        public void NextTurn_NoPlayers_ThrowsException()
        {

            var g = new game();
            try
            {
                g.NextTurn();
            }
            catch (Exception)
            {
                return;
            }
            Assert.Fail();

        }

        [TestMethod]
        public void NextTurn_IncrementsCurrentPlayer()
        {

            var g = new game();
            g.SetupPlayers(new[] { "bob", "larry", "george" });

            Assert.AreEqual(0, g.CurrentPlayer);
            g.NextTurn();
            Assert.AreEqual(1, g.CurrentPlayer);

        }

        [TestMethod]
        public void TestCannotBuyCard()
        {
            List<Player> fakePlayers = mocks.DynamicMock<List<Player>>();
            Player fakePlayer = mocks.DynamicMock<Player>("bob");
            fakePlayer.Gold = 5;
            fakePlayers.Add(fakePlayer);

            game game = new game();

            ICard fakeCard = mocks.DynamicMock<Corporation>();
            IDeck fakeBuyDeck = mocks.DynamicMock<Deck>();
            fakeBuyDeck.AddCard(fakeCard);

            Type gameType = typeof(game);
            PropertyInfo playersProperty = gameType.GetProperty("Players");
            PropertyInfo buyDeckProperty = gameType.GetProperty("BuyDeck");

            playersProperty.SetValue(game, fakePlayers);
            buyDeckProperty.SetValue(game, fakeBuyDeck);

            using (mocks.Ordered())
            {
                
            }

            mocks.ReplayAll();

            Assert.IsFalse(game.BuyCard("Corporation", fakePlayer));

            mocks.VerifyAll();

        }

        [TestMethod]
        public void TestCanBuyCard()
        {
            List<Player> fakePlayers = mocks.DynamicMock<List<Player>>();
            Player fakePlayer = mocks.DynamicMock<Player>("bob");
            fakePlayer.Gold = 6;
            fakePlayers.Add(fakePlayer);

            game game = new game();

            ICard fakeCard = mocks.DynamicMock<Corporation>();
            IDeck fakeBuyDeck = mocks.DynamicMock<Deck>();
            fakeBuyDeck.AddCard(fakeCard);

            Type gameType = typeof(game);
            PropertyInfo playersProperty = gameType.GetProperty("Players");
            PropertyInfo buyDeckProperty = gameType.GetProperty("BuyDeck");

            playersProperty.SetValue(game, fakePlayers);
            buyDeckProperty.SetValue(game, fakeBuyDeck);

            using (mocks.Ordered())
            {
                fakePlayer.BuyCard(Arg<ICard>.Is.Anything);
            }

            mocks.ReplayAll();

            Assert.IsTrue(game.BuyCard("Corporation", fakePlayer));

            mocks.VerifyAll();
        }

        [TestInitialize()]
        public void Initialize()
        {
            mocks = new MockRepository();
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

        private static bool IsHippieCamp(ICard card)
        {
            return (card.Type.ToLower().Equals("curse"));
        }

        #endregion

    }
}
