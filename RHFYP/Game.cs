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
        ///     The rondomizer cards (one copy of each action card) to pick from when
        ///     generating the games deck.
        /// </summary>
        private readonly List<ICard> _randomCardsList = new List<ICard>();

        private int _numberOfPlayers;

        /// <summary>
        ///     Initializes the Game with a new list of players and a new deck to buy from.
        /// </summary>
        public Game()
        {
            _randomCardsList.Add(new HomelessGuy());
            _randomCardsList.Add(new Apartment());
            _randomCardsList.Add(new Area51());
            _randomCardsList.Add(new Army());
            _randomCardsList.Add(new ConstructionSite());
            _randomCardsList.Add(new LawFirm());
            _randomCardsList.Add(new MilitaryBase());
            _randomCardsList.Add(new Museum());
            _randomCardsList.Add(new SpeedyLoans());
            _randomCardsList.Add(new Village());
            _randomCardsList.Add(new WallStreet());
            _randomCardsList.Add(new Gardens());

            Players = new List<Player>();
            BuyDeck = new Deck();
            TrashDeck = new Deck();
        }

        /// <summary>
        ///     Player whos turn it is.
        /// </summary>
        public int CurrentPlayer { get; set; }

        /// <summary>
        ///     A list of all the players in the Game.
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        ///     The number of players in the Game.
        /// </summary>
        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Can't have less that 0 players.");
                }
                _numberOfPlayers = value;
            }
        }

        /// <summary>
        ///     Populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the Game.
        /// </summary>
        public void GenerateCards()
        {
            while (BuyDeck.DrawCard() != null)
            {
            }
            AddStartingTresureCards();
            AddStartingVictoryCards();

            var cardNumbers = RandomListOfSequentialNumbers(_randomCardsList.Count).ToList();

            var pickedCards = 0;
            foreach (var i1 in cardNumbers)
            {
                for (var i = 0; i < 10; i++)
                {
                    BuyDeck.AddCard(_randomCardsList[i1].CreateCard());
                }
                if (pickedCards == 10) break;
                pickedCards++;
            }
        }

        /// <summary>
        ///     Creates players and deals them the proper number of cards.
        /// </summary>
        public void SetupPlayers(string[] playerNames)
        {
            Players.Clear();
            NumberOfPlayers = playerNames.Length;
            CurrentPlayer = NumberOfPlayers - 1;

            foreach (var player in playerNames.Select(t => new Player(t)))
            {
                Players.Add(player);
                player.TrashPile = TrashDeck;

                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new SmallBusiness {Location = new Point(20, 20 + i)});
                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new SmallBusiness {Location = new Point(22, 20 + i)});
                player.DrawPile.AddCard(new SmallBusiness {Location = new Point(21, 23)});

                for (var i = 0; i < 3; i++)
                    player.DrawPile.AddCard(new Purdue {Location = new Point(21, 20 + i)});

                player.DrawPile.Shuffle();

                player.PlayerState = PlayerState.Buy;
                player.Gold = 10;
                player.EndTurn();
            }

            NextTurn();
        }

        /// <summary>
        /// This method is called when a card is bought and will take a card out of the deck passed in by the parameter.
        /// </summary>
        /// <param name="name">The game of the card you want to sell the player.</param>
        /// <param name="player">The player you want to sell the card to.</param>
        /// <param name="x">The x cord the player wants the tile at.</param>
        /// <param name="y">The y cord the player wants the tile at.</param>
        /// <returns>True if the card was bought.</returns>
        public bool BuyCard(string name, IPlayer player, int x = 0, int y = 0)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player), "Must provide a player to sell the card to.");

            var c = BuyDeck.GetFirstCard(card => card.Name == name);
            if (c == null) return false;
            if (!player.CanAfford(c)) return false;
            c.Location = new Point(x, y);
            player.BuyCard(c);
            return true;
        }

        /// <summary>
        ///     The deck of cards that are in all the "draw" piles.
        /// </summary>
        public IDeck BuyDeck { get; set; }

        /// <summary>
        /// The games global trash deck.
        /// </summary>
        /// <remarks>All players nee da reference to this object.</remarks>
        public IDeck TrashDeck { get; set; }

        /// <summary>
        ///     Adds the default number of treasure cards to the buy deck.
        /// </summary>
        private void AddStartingTresureCards()
        {
            for (var i = 0; i < 60 - (7*NumberOfPlayers); i++)
                BuyDeck.AddCard(new SmallBusiness());


            for (var i = 0; i < 40; i++)
                BuyDeck.AddCard(new Company());


            for (var i = 0; i < 30; i++)
                BuyDeck.AddCard(new Corporation());
        }

        /// <summary>
        ///     Adds the default number of victory cards to the buy deck.
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

            for (var i = 0; i < (NumberOfPlayers - 1)*10; i++)
                BuyDeck.AddCard(new HippieCamp());
        }

        /// <summary>
        ///     Randomizes a list of numbers ranging from 0 to the given length.
        ///     This is used to generate the Game's Action cards.
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
        ///     Starts the turn of the next player in the Game.
        /// </summary>
        public void NextTurn()
        {
            if (NumberOfPlayers == 0)
            {
                throw new DivideByZeroException("There are no players in the game!");
            }

            Players[CurrentPlayer].EndTurn();

            CurrentPlayer++;
            CurrentPlayer %= NumberOfPlayers;

            if (Players.Count == 0) throw new Exception("Must have more then 0 players.");

            Players[CurrentPlayer].StartTurn();
        }
    }
}