using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using System.Collections.Generic;
using RHFYP.Cards;

namespace RHFYP_Test.IntegrationTests
{
    //This class will mainly be used to test action cards that attack other players work properly
    [TestClass]
    public class CardPlayerIntegrationTests
    {
        [TestMethod]
        public void TestMilitaryBaseStopsAttacks()
        {
            var p1 = new Player("p1");
            var p2 = new Player("p2");
            var p3 = new Player("p3");

            var players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);

            foreach(Player p in players)
            {
                var deck = new Deck();
                for(int x = 0; x < 5; x++)
                {
                    deck.AddCard(new Company());
                }
                p.Hand = deck;
            }

            p1.Hand.AddCard(new MilitaryBase());
            p2.Hand.AddCard(new Army());
            p2.PlayCard(p2.Hand.GetFirstCard(card => card is Army));

            Assert.AreEqual(6, p1.Hand.CardList.Count);
            Assert.AreEqual(3, p3.Hand.CardList.Count);
        }

        [TestMethod]
        public void TestMuseumPutsVictoryCardOnTopOfOpponentDrawPile()
        {
            var p1 = new Player("p1");
            var p2 = new Player("p2");
            var p3 = new Player("p3");

            var players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);

            var rose1 = new Rose();
            var rose2 = new Rose();

            p1.Hand.AddCard(rose1);
            p3.Hand.AddCard(rose2);

            p2.Hand.AddCard(new Museum());
            Assert.AreEqual(p1.DrawPile.CardList.Count, 0);
            Assert.AreEqual(p3.DrawPile.CardList.Count, 0);

            p2.PlayCard(p2.Hand.GetFirstCard(card => card.GetType() == new Museum().GetType()));

            Assert.IsTrue(p1.DrawPile.InDeck(rose1));
            Assert.IsTrue(p3.DrawPile.InDeck(rose2));
            Assert.IsTrue(p2.DrawPile.CardList.Find(card => card.GetType() == new Company().GetType()).GetType() == new Company().GetType());
        }
    }
}
