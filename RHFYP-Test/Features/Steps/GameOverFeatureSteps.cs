using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP.Interfaces;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class GameOverFeatureSteps
    {
        private readonly GameSteps _game;

        public GameOverFeatureSteps(GameSteps game)
        {
            _game = game;
        }

        [Then(@"the game should be over")]
        public void ThenTheGameShouldBeOver()
        {
            Assert.IsTrue(_game.Game.GameState == GameState.Ended);
        }

        [Then(@"player ([0-9]) should win")]
        public void ThenPlayerXShouldWin(int player)
        {
            if (player < 0) throw new ArgumentOutOfRangeException(nameof(player));
            Assert.IsTrue(_game.Game.Players[player].Winner);
        }

        [Then(@"a Rose-Hulman card should be in the buy deck")]
        public void ThenARose_HulmanCardShouldBeInTheBuyDeck()
        {
            Assert.AreNotEqual(0, _game.Game.BuyDeck.SubDeck(x => x.Name == "Rose-Hulman").CardList.Count);
        }

        [Then(@"the number of depleted names should be ([0-9]+)")]
        public void ThenTheNumberOfDepletedNamesShouldBe(int numberOfDepletedNames)
        {
            Assert.AreEqual(numberOfDepletedNames, _game.Game.BuyDeck.NumberOfDepletedNames());
        }
    }
}