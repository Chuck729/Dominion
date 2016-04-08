using System;
using System.Collections.Generic;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Game : IGame
    {
        public bool PlayerChanged { get; set; }

        public Player CurrentPlayer { get; set; }

        public Game()
        {
            BuyDeck = new Deck();
        }

        /// <summary>
        /// How many players are in the game.
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the game
        /// </summary>
        public void GenerateCards()
        {
            while(BuyDeck.DrawCard() != null) { }

            for (var i = 0; i < 60; i++)
            {
                // TODO: Add "AddCards()" method to deck to simplify all of these loops.
                BuyDeck.AddCard(new SmallBusiness());
            }

            for (var i = 0; i < 40; i++)
            {
                BuyDeck.AddCard(new Company());
            }

            for (var i = 0; i < 30; i++)
            {
                BuyDeck.AddCard(new Corporation());
            }

            for (var i = 0; i < 8 + 3 * NumberOfPlayers; i++)
            {
                BuyDeck.AddCard(new Purdue());
            }

            for (var i = 0; i < 8; i++)
            {
                BuyDeck.AddCard(new Mit());
                BuyDeck.AddCard(new Rose());
            }

            for (var i = 0; i < (NumberOfPlayers - 1) * 10; i++)
            {
                BuyDeck.AddCard(new HippieCamp());
            }

            const int numberOfActionCards = 12;
            var cardNumbers = new List<int>();

            for (var i = 0; i < numberOfActionCards; i++)
            {
                cardNumbers.Add(i);
            }

            var cardnumbers2 = cardNumbers.Randomize();

            var pickedCards = 0;
            foreach (var i1 in cardnumbers2)
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
                    default:
                        break;
                }
                if (pickedCards == 10) break;
                pickedCards++;
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