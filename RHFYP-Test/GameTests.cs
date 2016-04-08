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
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsFamilyBusiness).CardList.Count == 60);

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
        public void GenerateCards_VictoryCardsPresent_8And3PerPlayerPurdues()
        {

            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.NumberOfPlayers = 3;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsPurdue).CardList.Count == 8 + 3 * g.NumberOfPlayers);
            g.NumberOfPlayers = 7;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsPurdue).CardList.Count == 8 + 3 * g.NumberOfPlayers);
            g.NumberOfPlayers = 5;
            g.GenerateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsPurdue).CardList.Count == 23);

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

        private static bool IsFamilyBusiness(ICard card)
        {
            return (card.Name.ToLower().Equals("familybusiness"));
        }

        private static bool IsCompany(ICard card)
        {
            return (card.Name.ToLower().Equals("company"));
        }

        private static bool IsCorporation(ICard card)
        {
            return (card.Name.ToLower().Equals("internationalcorporation"));
        }

        private bool IsRose(ICard card)
        {
            return (card.Name.ToLower().Equals("rosehulman"));
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
    }
}
