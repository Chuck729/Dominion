﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP.Cards.VictoryCards;

namespace RHFYP_Test.IndividualCardTests
{
    [TestClass]
    public class PurdueTests
    {
        private MockRepository _mocks;

        [TestInitialize()]
        public void Initialize()
        {
            _mocks = new MockRepository();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TestPurduePlayerNotExist()
        {
            Card c = new Purdue();
            c.PlayCard(null, null);

        }


        [TestMethod]
        public void TestPlayCardPurdue()
        {
            ICard card = new Purdue();
            var p = _mocks.DynamicMock<Player>("bob");

            card.PlayCard(p, null);

            Assert.AreEqual(0, p.Gold);
            Assert.AreEqual(0, p.Investments);
            Assert.AreEqual(0, p.Managers);
        }

        [TestMethod]
        public void TestPurdueFactory()
        {
            ICard card = new Purdue();
            var newCard = card.CreateCard() as Purdue;
            Assert.IsNotNull(newCard);
        }
    }
}
