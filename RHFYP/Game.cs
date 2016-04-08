﻿using System;
using System.Collections.Generic;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Game : IGame
    {
        public bool PlayerChanged { get; set; }

        public Player CurrentPlayer { get; set; }

        public List<Player> Players { get; set; }

        public Game()
        {
            Players = new List<Player>();
            BuyDeck = new Deck();
        }

        /// <summary>
        /// How many players are in the game.
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// Adds the default number of tresure cards to the buy deck.
        /// </summary>
        private void AddStartingTresureCards()
        {

            for (var i = 0; i < 60 - (7 * NumberOfPlayers); i++) 
                BuyDeck.AddCard(new SmallBusiness());
            

            for (var i = 0; i < 40; i++)
                BuyDeck.AddCard(new Company());
            

            for (var i = 0; i < 30; i++)
                BuyDeck.AddCard(new Corporation());
            
        }

        /// <summary>
        /// Adds the default number of victory cards to the buy deck.
        /// </summary>
        private void AddStartingVictoryCards()
        {

            for (var i = 0; i < 8; i++)
                BuyDeck.AddCard(new Purdue());

            for (var i = 0; i < 8; i++)
            {
                BuyDeck.AddCard(new Mit());
                BuyDeck.AddCard(new Rose());
            }

            for (var i = 0; i < (NumberOfPlayers - 1) * 10; i++)
                BuyDeck.AddCard(new HippieCamp());
            
        }

        private static IEnumerable<int> RandomListOfSequentialNumbers(int length)
        {
            var cardNumbers = new List<int>();

            for (var i = 0; i < length; i++)
            {
                cardNumbers.Add(i);
            }

            return cardNumbers.Randomize();
        }

        /// <summary>
        /// populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the game
        /// </summary>
        public void GenerateCards()
        {
            while(BuyDeck.DrawCard() != null) { }
            AddStartingTresureCards();
            AddStartingVictoryCards();

            const int numberOfActionCards = 12;
            var cardNumbers = RandomListOfSequentialNumbers(numberOfActionCards).ToList();

            var pickedCards = 0;
            foreach (var i1 in cardNumbers)
            {
                switch (i1)
                {
                    case 0:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new HomelessGuy());
                        }
                        break;
                    case 1:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new Apartment());
                        }
                        break;
                    case 2:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new Area51());
                        }
                        break;
                    case 3:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new Army());
                        }
                        break;
                    case 4:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new ConstructionSite());
                        }
                        break;
                    case 5:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new Gardens());
                        }
                        break;
                    case 6:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new LawFirm());
                        }
                        break;
                    case 7:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new MilitaryBase());
                        }
                        break;
                    case 8:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new Museum());
                        }
                        break;
                    case 9:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new SpeedyLoans());
                        }
                        break;
                    case 10:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new Village());
                        }
                        break;
                    case 11:
                        for (var j = 0; j < 10; j++)
                        {
                            BuyDeck.AddCard(new WallStreet());
                        }
                        break;
                }
                if (pickedCards == 10) break;
                pickedCards++;
            }
        }

        /// <summary>
        /// Creates n players and deals them the proper number of cards.
        /// </summary>
        public void SetupPlayers(string[] playerNames)
        {
            Players.Clear();
            NumberOfPlayers = playerNames.Length;

            foreach (var player in playerNames.Select(t => new Player(t)))
            {
                Players.Add(player);

                for (var i = 0; i < 7; i++)
                    player.DrawPile.AddCard(new SmallBusiness());

                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new Purdue());

                player.PlayerState = PlayerState.Buy;
                player.EndTurn();
            }
        }

        /// <summary>
        /// method called when a card is bought and will take a card out of the deck passed in by the parameter
        /// </summary>
        /// <param name="pile"></param>
        public ICard BuyCard(IDeck pile, IPlayer player)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        public IDeck BuyDeck { get; set; }
    }
}