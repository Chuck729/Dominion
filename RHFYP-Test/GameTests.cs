﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            g.generateCards();
            Assert.IsTrue(g.BuyDeck.CardList.Count > 0);

        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_46FamilyBusinessesInBuyDeck()
        {

            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.generateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsFamilyBusiness).CardList.Count == 46);

        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_40CompaniesInBuyDeck()
        {

            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.generateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCompany).CardList.Count == 40);

        }

        [TestMethod]
        public void GenerateCards_TresureCardsAlwaysPresent_30CorporationsInBuyDeck()
        {

            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.generateCards();
            Assert.IsTrue(g.BuyDeck.SubDeck(IsCorporation).CardList.Count == 30);

        }

        [TestMethod]
        public void GenerateCards_ActionCardsPicked_10DifferentActionTypes()
        {

            var g = new Game();

            Assert.IsTrue(g.BuyDeck.CardList.Count == 0);
            g.generateCards();

            IDeck actionCards = g.BuyDeck.SubDeck(IsAction);
            

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

        private bool IsAction(ICard card)
        {
            return (card.Type.ToLower().Equals("action"));
        }
    }
}
