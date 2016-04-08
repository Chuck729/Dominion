using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;

namespace RHFYP_Test
{

    [TestClass]
    public class GameTests
    {

        [TestMethod]
        public void GenerateCards_CardIsPutIntoBuyDeck_BuyDeckNotEmpty()
        {

            var g  = new Game();

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
            Assert.IsTrue(g.BuyDeck.SubDeck(IsFamilyBusiness).CardList.Count == 60 - (7 * 4));

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

            var g = new Game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });
            for (var i = 0; i < g.NumberOfPlayers; i++)
                Assert.AreEqual(7, g.Players[i].DrawPile.SubDeck(IsFamilyBusiness).CardCount());

        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_Has3Purdues()
        {

            var g = new Game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });
            for (var i = 0; i < g.NumberOfPlayers; i++)
                Assert.AreEqual(3, g.Players[i].DrawPile.SubDeck(IsPurdue).CardCount());

        }

        [TestMethod]
        public void SetupPlayers_StartWithCorrectCards_HasCorrectNumberOfStartingCards()
        {

            var g = new Game();

            g.GenerateCards();
            g.SetupPlayers(new[] { "bob", "larry", "george" });
            for (var i = 0; i < g.NumberOfPlayers; i++)
                Assert.AreEqual(10, g.Players[i].DrawPile.CardCount());

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
