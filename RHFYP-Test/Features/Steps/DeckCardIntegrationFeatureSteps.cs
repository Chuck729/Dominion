using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHFYP;
using RHFYP.Cards.ActionCards;
using TechTalk.SpecFlow;

// ReSharper disable UnusedMember.Global

namespace RHFYP_Test.Features.Steps
{
    [Binding]
    public class DeckCardIntegrationFeatureSteps
    {
        private readonly List<Deck> _decks = new List<Deck>();

        [Given(@"I have (.) decks")]
        public void GivenIHavexDecks(int x)
        {
            for (var i = 0; i < x; i++)
            {
                _decks.Add(new Deck());
            }
        }

        [Given(@"deck (.) gets a Bank")]
        public void GivenDeckxGetsABank(int x)
        {
            _decks[x].AddCard(new Bank());
        }

        [Given(@"deck (.) gets an Army")]
        public void GivenDeckxGetsAnArmy(int x)
        {
            _decks[x].AddCard(new Army());
        }

        [Given(@"deck (.) gets a CIS")]
        public void GivenDeckxGetsAVictoryCardy(int x)
        {
            _decks[x].AddCard(new Cis());
        }

        [When(@"deck (.) is appended to deck (.)")]
        public void WhenDeckxIsAppendedToDecky(int x, int y)
        {
            _decks[y] = (Deck) _decks[y].AppendDeck(_decks[x]);
        }

        [When(@"deck (.) is shuffled into deck (.)")]
        public void WhenDeckxIsShuffledIntoDecky(int x, int y)
        {
            _decks[y].ShuffleIn(_decks[x]);
        }

        [Then(@"deck (.) has (.) cards")]
        public void ThenDeckxHasyCards(int x, int y)
        {
            Assert.AreEqual(y, _decks[x].CardList.Count);
        }

        [Then(@"deck (.) has a (.*)")]
        public void ThenDeckxHasAy(int x, string y)
        {
            Assert.IsNotNull(_decks[x].GetFirstCard(p => p.Name.Equals(y)));
        }
    }
}