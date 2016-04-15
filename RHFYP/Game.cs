﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Game : IGame
    {
        /// <summary>
        /// Keeps track of whether or not the current player was changed.
        /// </summary>
        public bool PlayerChanged { get; set; }

        /// <summary>
        /// Keeps track of the player who's turn it currently is.
        /// </summary>
        private int _currentPlayer;

        /// <summary>
        /// Getter and setter for _currentPlayer.
        /// </summary>
        public int CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }

            set
            {
                PlayerChanged = true;
                _currentPlayer = value;
            }
        }

        /// <summary>
        /// A list of all the players in the Game.
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Initializes the Game with a new list of players and a new deck to buy from.
        /// </summary>
        public Game()
        {
            Players = new List<Player>();
            BuyDeck = new Deck();
        }

        /// <summary>
        /// The number of players in the Game.
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// Adds the default number of treasure cards to the buy deck.
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

        /// <summary>
        /// Randomizes a list of numbers ranging from 0 to the given length.
        /// This is used to generate the Game's Action cards.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
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
        /// Populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the Game.
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
        /// Creates players and deals them the proper number of cards.
        /// </summary>
        public void SetupPlayers(string[] playerNames)
        {
            Players.Clear();
            NumberOfPlayers = playerNames.Length;
            CurrentPlayer = NumberOfPlayers - 1;

            foreach (var player in playerNames.Select(t => new Player(t)))
            {
                Players.Add(player);

                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new SmallBusiness {Location = new Point(20, 20 + i)});
                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new SmallBusiness { Location = new Point(22, 20 + i) });
                player.DrawPile.AddCard(new SmallBusiness { Location = new Point(21, 23) });

                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new Purdue { Location = new Point(21, 20 + i) });

                player.DrawPile.Shuffle();

                player.PlayerState = PlayerState.Buy;
                player.Gold = 10;
                player.EndTurn();
            }

            NextTurn();
        }

        /// <summary>
        /// Starts the turn of the next player in the Game.
        /// </summary>
        public void NextTurn()
        {
            CurrentPlayer++;
            CurrentPlayer %= NumberOfPlayers;

            if (Players.Count == 0) throw new Exception("Must have more then 0 players.");

            Players[CurrentPlayer].StartTurn();

            
        }

        /// <summary>
        /// This method is called when a card is bought and will take a card out of the deck passed in by the parameter.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="player"></param>
        public bool BuyCard(String name, IPlayer player)
        {
            ICard c = BuyDeck.GetFirstCard(x => x.Name == name);
            if (player.CanAfford(c))
            {
                player.BuyCard(c);
                return true;
            }

            return false;
        }

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        public IDeck BuyDeck { get; set; }
    }
}