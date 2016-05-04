using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RHFYP_Test
{
    [Binding]
    public class DeckCardIntegrationFeatureSteps
    {

        List<Deck> decks = new List<Deck>();
        [Given(@"I have (.) decks")]
        public void GivenIHaveDecks(int num)
        {
            for (int i = 0; i < num; i++)
            {
                decks.Add(new Deck());
            }
        }
        
        [Given(@"deck (.) gets a Bank")]
        public void GivenDeckGetsABank(int i)
        {
            decks[i].AddCard(new Bank());
        }
        
        [Given(@"deck (.) gets a Army")]
        public void GivenDeckGetsAArmy(int i)
        {
            decks[i].AddCard(new Army());
        }
        
        [Given(@"deck (.) gets a CIS")]
        public void GivenDeckGetsACIS(int i)
        {
            decks[i].AddCard(new CIS());
        }
        
        [When(@"deck (.) is appended to deck (.)")]
        public void WhenDeckIsAppendedToDeck(int appended, int toAppend)
        {
            decks[toAppend] = (Deck) decks[toAppend].AppendDeck(decks[appended]);
        }
        
        [Then(@"deck (.) has a Bank")]
        public void ThenDeckHasABank(int p0)
        {
            Assert.IsNotNull(decks[p0].GetFirstCard(p => p.Name == "Bank"));
        }
        
        [Then(@"deck (.) has a Army")]
        public void ThenDeckHasAArmy(int p0)
        {
            Assert.IsNotNull(decks[p0].GetFirstCard(p => p.Name == "Army"));
        }

        [Then(@"deck (.) has a CIS")]
        public void ThenDeckHasACIS(int p0)
        {
            Assert.IsNotNull(decks[p0].GetFirstCard(p => p.Name == "CIS"));
        }

        [Then(@"deck (.) has (.) cards")]
        public void ThenDeckHasCards(int d, int c)
        {
            Assert.AreEqual(c, decks[d].CardList.Count);
        }

    }
}
