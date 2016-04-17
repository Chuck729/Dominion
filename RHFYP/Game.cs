using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RHFYP.Cards;

namespace RHFYP
{
    public class Game : IGame
    {

        /// <summary> 
        /// The rondomizer cards (one copy of each action card) to pick from when
        /// generating the games deck.
        /// </summary>
        private readonly List<ICard> _actionCardsList = new List<ICard>();

        /// <summary>
        /// Player whos turn it is.
        /// </summary>
        public int CurrentPlayer { get; set; }

        /// <summary>
        /// A list of all the players in the Game.
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Initializes the Game with a new list of players and a new deck to buy from.
        /// </summary>
        public Game()
        {
            _actionCardsList.Add(new HomelessGuy());
            _actionCardsList.Add(new Apartment());
            _actionCardsList.Add(new Area51());
            _actionCardsList.Add(new Army());
            _actionCardsList.Add(new ConstructionSite());
            _actionCardsList.Add(new LawFirm());
            _actionCardsList.Add(new MilitaryBase());
            _actionCardsList.Add(new Museum());
            _actionCardsList.Add(new SpeedyLoans());
            _actionCardsList.Add(new Village());
            _actionCardsList.Add(new WallStreet());

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

            var cardNumbers = RandomListOfSequentialNumbers(_actionCardsList.Count).ToList();

            var pickedCards = 0;
            foreach (var i1 in cardNumbers)
            {
                for (var i = 0; i < 10; i++)
                {
                    BuyDeck.AddCard(_actionCardsList[i1].CreateCard());
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
            if (NumberOfPlayers == 0)
            {
                throw new DivideByZeroException("There are no players in the game!");
            }

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
        public bool BuyCard(string name, IPlayer player)
        {
            if (player == null)  throw new ArgumentNullException(nameof(player), "Must provide a player to sell the card to.");

            var c = BuyDeck.GetFirstCard(x => x.Name == name);
            if (c == null) return false;
            if (!player.CanAfford(c)) return false;
            player.BuyCard(c);
            return true;
        }

        /// <summary>
        /// The deck of cards that are in all the "draw" piles.
        /// </summary>
        public IDeck BuyDeck { get; set; }
    }
}