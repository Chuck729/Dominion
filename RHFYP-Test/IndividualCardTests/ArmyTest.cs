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

            var p1Hand = _mocks.DynamicMock<IDeck>();
            var p2Hand = _mocks.DynamicMock<IDeck>();

            var p1Disc = _mocks.DynamicMock<IDeck>();
            var p2Disc = _mocks.DynamicMock<IDeck>();

            var c16 = _mocks.Stub<Corporation>();

            Type playerType = typeof(Player);
            PropertyInfo handField = playerType.GetProperty("Hand");

            handField.SetValue(p1, p1Hand);
            handField.SetValue(p2, p2Hand);

            PropertyInfo discardField = playerType.GetProperty("DiscardPile");

            discardField.SetValue(p1, p1Disc);
            discardField.SetValue(p2, p2Disc);

            using (_mocks.Ordered())
            {
                p1.AddGold(2);
                p2.DrawHandToDiscard();
            }

            _mocks.ReplayAll();

            c.PlayCard(p1, new List<Player> { p1, p2 });

            _mocks.VerifyAll();
        }
    }
}
