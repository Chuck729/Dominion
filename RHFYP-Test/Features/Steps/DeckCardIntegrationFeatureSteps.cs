using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class DeckCardIntegrationFeatureSteps
    {
        List<Deck> decks = new List<Deck>();

        [Given(@"I have (.) decks")]
        public void GivenIHavexDecks(int x)
        {
            for (int i = 0; i < x; i++)
            {
                decks.Add(new Deck());
            }
        }
        
        [Given(@"deck (.) gets a Bank")]
        public void GivenDeckxGetsABank(int x)
        {
            decks[x].AddCard(new Bank());
        }

        [Given(@"deck (.) gets an Army")]
        public void GivenDeckxGetsAnArmy(int x)
        {
            decks[x].AddCard(new Army());
        }

        [Given(@"deck (.) gets a CIS")]
        public void GivenDeckxGetsAVictoryCardy(int x)
        {
            decks[x].AddCard(new Cis());
        }

        [When(@"deck (.) is appended to deck (.)")]
        public void WhenDeckxIsAppendedToDecky(int x, int y)
        {
            decks[y] = (Deck)decks[y].AppendDeck(decks[x]);
        }
        
        [When(@"deck (.) is shuffled into deck (.)")]
        public void WhenDeckxIsShuffledIntoDecky(int x, int y)
        {
            decks[y].ShuffleIn(decks[x]);
        }
        
        [Then(@"deck (.) has (.) cards")]
        public void ThenDeckxHasyCards(int x, int y)
        {
            Assert.AreEqual(y, decks[x].CardList.Count);
        }
        
        [Then(@"deck (.) has a (.*)")]
        public void ThenDeckxHasAy(int x, string y)
        {
            Assert.IsNotNull(decks[x].GetFirstCard(p => p.Name.Equals(y)));
        }
    }
}
