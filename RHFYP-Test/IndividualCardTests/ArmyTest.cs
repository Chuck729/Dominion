using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RHFYP.Cards;
using RHFYP;
using System.Reflection;

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
            var p3 = _mocks.DynamicMock<Player>("p3");
            var p4 = _mocks.DynamicMock<Player>("p4");

            var p1Hand = _mocks.DynamicMock<Deck>();
            var p2Hand = _mocks.DynamicMock<Deck>();
            var p3Hand = _mocks.DynamicMock<Deck>();
            var p4Hand = _mocks.DynamicMock<Deck>();

            var c1 = _mocks.Stub<Card>();
            var c2 = _mocks.Stub<Card>();
            var c3 = _mocks.Stub<Card>();
            var c4 = _mocks.Stub<Card>();
            var c5 = _mocks.Stub<Card>();

            p2.Hand.AddCard(c1);
            p2.Hand.AddCard(c2);
            p2.Hand.AddCard(c3);
            p2.Hand.AddCard(c4);
            p2.Hand.AddCard(c5);

            var c6 = _mocks.Stub<Card>();
            var c7 = _mocks.Stub<Card>();
            var c8 = _mocks.Stub<Card>();
            var c9 = _mocks.Stub<Card>();

            p3.Hand.AddCard(c6);
            p3.Hand.AddCard(c7);
            p3.Hand.AddCard(c8);
            p3.Hand.AddCard(c9);

            var c10 = _mocks.Stub<Card>();
            var c11 = _mocks.Stub<Card>();
            var c12 = _mocks.Stub<Card>();

            p4.Hand.AddCard(c10);
            p4.Hand.AddCard(c11);
            p4.Hand.AddCard(c12);

            var c13 = _mocks.Stub<Card>();
            var c14 = _mocks.Stub<Card>();
            var c15 = _mocks.Stub<Card>();
            var c16 = _mocks.Stub<Card>();

            p1.Hand.AddCard(c13);
            p1.Hand.AddCard(c14);
            p1.Hand.AddCard(c15);
            p1.Hand.AddCard(c16);


            Type playerType = typeof(Player);
            FieldInfo handField = playerType.GetField("Hand");

            handField.SetValue(p1, p1Hand);
            handField.SetValue(p2, p2Hand);
            handField.SetValue(p3, p3Hand);
            handField.SetValue(p4, p4Hand);

            using (_mocks.Ordered())
            {
                p1.AddGold(2);
                p2.Hand.DrawCard();
                p2.DiscardPile.AddCard(Arg<Card>.Is.Anything);
                p2.Hand.DrawCard();
                p2.DiscardPile.AddCard(Arg<Card>.Is.Anything);
                p2.Hand.DrawCard();
                p3.DiscardPile.AddCard(Arg<Card>.Is.Anything);
            }

            _mocks.ReplayAll();

            c.PlayCard(p1, new List<Player> { p1, p2, p3, p4 });

            _mocks.VerifyAll();
        }
    }
}
