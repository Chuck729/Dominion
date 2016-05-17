using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.VictoryCards;
using System;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class LibraryPlayCardSteps
    {
        GameSteps g;
        SpeedyLoansPlayCardSteps s;
        SimpleCardSteps ss;

        public LibraryPlayCardSteps(GameSteps g, SpeedyLoansPlayCardSteps s, SimpleCardSteps ss)
        {
            this.g = g;
            this.s = s;
            this.ss = ss;
        }


        [Given(@"player (.*) has no cards")]
        public void GivenPlayerHasNoCards(int p0)
        {
            g.Game.Players[p0].DrawPile = new Deck();
            g.Game.Players[p0].Hand = new Deck();
            g.Game.Players[p0].DiscardPile = new Deck();
        }

        private Library library = new Library();
        [Given(@"player (.*) has a Library card in their hand")]
        public void GivenPlayerHasALibraryCardInTheirHand(int p0)
        {
            library = new Library();
            g.Game.Players[p0].Hand.AddCard(library);
        }
        
        [Given(@"player (.*) has (.*) non-action cards in their draw pile")]
        public void GivenPlayerHasNon_ActionCardsInTheirDrawPile(int p0, int p1)
        {
            for (int i = 0; i < p1; i++)
            {
                g.Game.Players[p0].DrawPile.AddCard(new Rose());
            }
        }

        private Library libraryError;
        [Given(@"there is a Library card in the game")]
        public void GivenThereIsALibraryCardInTheGame()
        {
            libraryError = new Library();
        }
        
        [When(@"player (.*) plays the Library card")]
        public void WhenPlayerPlaysTheLibraryCard(int p0)
        {
            g.Game.Players[p0].PlayCard(library);
        }
        
        [When(@"the Library card is played without a player")]
        public void WhenTheLibraryCardIsPlayedWithoutAPlayer()
        {
            try
            {
                libraryError.PlayCard(null, g.Game);
            } catch (Exception e)
            {
                s.caughtException = e;
            }
        }
        
        [When(@"the Library card is played without a game")]
        public void WhenTheLibraryCardIsPlayedWithoutAGame()
        {
            try
            {
                libraryError.PlayCard(new RHFYP.Player(""), null);
            } catch (Exception e)
            {
                s.caughtException = e;
            }
        }
        
        [Then(@"player (.*) does not have a Library card in their hand")]
        public void ThenPlayerDoesNotHaveALibraryCardInTheirHand(int p0)
        {
            bool x = g.Game.Players[p0].Hand.GetFirstCard(c => c.Name == "Library") == null;
            bool y = g.Game.Players[p0].Hand.CardList.Contains(library);
            Assert.IsTrue(x && !y);
        }
        
        [Then(@"player (.*) has a Library card in their discard pile")]
        public void ThenPlayerHasALibraryCardInTheirDiscardPile(int p0)
        {
            bool x = g.Game.Players[p0].DiscardPile.GetFirstCard(c => c.Name == "Library") != null;
            bool y = g.Game.Players[p0].DiscardPile.CardList.Contains(library);
            Assert.IsTrue(x && y);
        }
        
        [Then(@"player (.*) has (.*) non-action cards in their hand")]
        public void ThenPlayerHasNon_ActionCardsInTheirHand(int p0, int p1)
        {
            Assert.AreEqual(p1, g.Game.Players[p0].Hand.CardList.Count);
        }
        
        [Then(@"player (.*) has (.*) non-action cards in their draw pile")]
        public void ThenPlayerHasNon_ActionCardsInTheirDrawPile(int p0, int p1)
        {
            Assert.AreEqual(p1, g.Game.Players[p0].DrawPile.CardList.Count);
        }
    }
}
