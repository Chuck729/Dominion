using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.IntegrationTests
{
    //This class will mainly be used to test action cards that attack other players work properly
    [TestClass]
    public class CardPlayerIntegrationTests
    {
        [TestMethod]
        public void TestMilitaryBaseStopsArmy()
        {
            var p1 = new Player("p1");
            var p2 = new Player("p2");
            var p3 = new Player("p3");

            var game = new Game {Players = new List<Player> {p1, p2, p3}};

            foreach (var p in game.Players)
            {
                var deck = new Deck();
                for (var x = 0; x < 5; x++)
                {
                    deck.AddCard(new Company());
                }
                p.Hand = deck;
            }

            p2.Gold = 0;
            p1.Hand.AddCard(new MilitaryBase());
            p2.Hand.AddCard(new Army());

            var army = new Army();
            army.PlayCard(p2, game);

            Assert.AreEqual(2, p2.Gold);
            Assert.AreEqual(6, p1.Hand.CardList.Count);
            Assert.AreEqual(3, p3.Hand.CardList.Count);
        }

        [TestMethod]
        public void TestMuseumPutsVictoryCardOnTopOfOpponentDrawPile()
        {
            var game = new Game();

            string[] players = {"p1", "p2", "p3"};
            game.SetupPlayers(players);
            game.Players[1].Managers = 1;
            game.Players[1].PlayerState = PlayerState.Action;
            game.Players[0].Hand.CardList = new List<ICard>();
            game.Players[2].Hand.CardList = new List<ICard>();
            game.Players[0].DrawPile.CardList = new List<ICard>();
            game.Players[2].DrawPile.CardList = new List<ICard>();
            game.Players[1].Hand.CardList = new List<ICard>();
            game.Players[1].DrawPile.CardList = new List<ICard>();


            var rose1 = new Rose();
            var rose2 = new Rose();

            game.Players[0].Hand.AddCard(rose1);
            game.Players[2].Hand.AddCard(rose2);

            game.Players[1].Hand.AddCard(new Museum());
            Assert.AreEqual(game.Players[0].DrawPile.CardList.Count, 0);
            Assert.AreEqual(game.Players[2].DrawPile.CardList.Count, 0);
            Assert.IsTrue(game.Players[0].Hand.InDeck(rose1));
            game.Players[1].PlayCard(game.Players[1].Hand.CardList.Find(card => card is Museum));

            Assert.AreEqual(game.Players[1].Hand.CardList.Count, 0);
            Assert.AreEqual(game.Players[0].DrawPile.CardList.Count, 1);
            Assert.IsTrue(game.Players[0].DrawPile.InDeck(rose1));
            Assert.IsTrue(game.Players[2].DrawPile.InDeck(rose2));
            Assert.IsTrue(game.Players[1].DrawPile.CardList.Find(card => card is Company) != null);
        }
    }
}