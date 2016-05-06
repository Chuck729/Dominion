using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards;
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
        
        [Given(@"deck (.) gets an action card (.*)")]
        public void GivenDeckxGetsAnActionCardy(int x, string y)
        {
            Card c = (Card) Activator.CreateInstance(Type.GetType("RHFYP.Cards.ActionCards" + "." + y + ".cs"));
            decks[x].AddCard(c);
        }

        [Given(@"deck (.) gets a treasure card (.*)")]
        public void GivenDeckxGetsATreasureCardy(int x, string y)
        {
            Card c = (Card) Activator.CreateInstance(Type.GetType("RHFYP.Cards.TreasureCards" + "." + y + ".cs"));
            decks[x].AddCard(c);
        }

        [Given(@"deck (.) gets a victory card (.*)")]
        public void GivenDeckxGetsAVictoryCardy(int x, string y)
        {
            Card c = (Card) Activator.CreateInstance(Type.GetType("RHFYP.Cards.VictoryCards" + "." + y + ".cs"));
            decks[x].AddCard(c);
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
