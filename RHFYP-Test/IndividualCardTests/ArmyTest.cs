using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP.Cards;
using RHFYP;
using System.Reflection;
using Rhino.Mocks.Constraints;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;

namespace RHFYP_Test.IndividualCardTests
{
    /// <summary>
    /// Summary description for ArmyTest
    /// </summary>
    [TestClass]
    public class ArmyTest
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        public void TestPlayArmyCard()
        {
            var c = new Army();

            var p1 = _mocks.DynamicMock<Player>("p1");
            var p2 = _mocks.DynamicMock<Player>("p2");

            var game = _mocks.DynamicMock<Game>();

            var p1Hand = _mocks.DynamicMock<IDeck>();
            var p2Hand = _mocks.DynamicMock<IDeck>();

            var p1Disc = _mocks.DynamicMock<IDeck>();
            var p2Disc = _mocks.DynamicMock<IDeck>();

            using (_mocks.Ordered())
            {
                p1.AddGold(2);
                p2.DrawHandToDiscard();
            }

            _mocks.ReplayAll();

            game.Players = new List<Player> {p1, p2};

            var playerType = typeof(Player);
            var handField = playerType.GetProperty("Hand");
            var discardField = playerType.GetProperty("DiscardPile");

            handField.SetValue(p1, p1Hand);
            handField.SetValue(p2, p2Hand);

            discardField.SetValue(p1, p1Disc);
            discardField.SetValue(p2, p2Disc);

            p1Hand.CardList = new List<ICard>();
            p2Hand.CardList = new List<ICard>();

            c.PlayCard(p1, game);

            _mocks.VerifyAll();
        }

        [TestMethod]
        public void TestArmyFactory()
        {
            ICard card = new Army();
            var newCard = card.CreateCard() as Army;
            Assert.IsNotNull(newCard);
        }
    }
}
