﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using TechTalk.SpecFlow;

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
