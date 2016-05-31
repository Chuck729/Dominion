using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.TreasureCards;
using RHFYP.Cards.VictoryCards;
using RHFYP.Interfaces;

namespace RHFYP
{
    public class Game : IGame
    {

        /// <summary>
        ///     The rondomizer cards (one copy of each action card) to pick from when
        ///     generating the games deck.
        /// </summary>
        private readonly List<ICard> _randomCardsList = new List<ICard>();

        /// <summary>
        ///     Holds the number of players in the game
        /// </summary>
        private int _numberOfPlayers;

        private static int _seed;

        /// <summary>
        ///     Initializes the Game with a new list of players and a new deck to buy from.
        /// </summary>
        public Game(int seed)
        {
            _seed = seed;
            GameState = GameState.InProgress;

            _randomCardsList.Add(new Apartment()); // 23 total cards added
            _randomCardsList.Add(new Area51());
            _randomCardsList.Add(new Army());
            _randomCardsList.Add(new Bank());
            _randomCardsList.Add(new CeosHouse());
            _randomCardsList.Add(new Committee());
            _randomCardsList.Add(new ConstructionSite());
            _randomCardsList.Add(new Gardens());
            _randomCardsList.Add(new HomelessGuy());
            _randomCardsList.Add(new Laboratory());
            _randomCardsList.Add(new LawFirm());
            _randomCardsList.Add(new Library());
            _randomCardsList.Add(new MilitaryBase());
            _randomCardsList.Add(new Mine());
            _randomCardsList.Add(new Museum());
            _randomCardsList.Add(new Plug());
            _randomCardsList.Add(new Prison());
            _randomCardsList.Add(new Scholarship());
            _randomCardsList.Add(new SpeedyLoans());
            _randomCardsList.Add(new StartUp());
            _randomCardsList.Add(new Storeroom());
            _randomCardsList.Add(new Subdivision());
            _randomCardsList.Add(new WallStreet());

            Players = new List<Player>();
            BuyDeck = new Deck();
            TrashDeck = new Deck();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class with a seed of 0.
        /// </summary>
        public Game() : this(0)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the game needs user input.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the game needs user input; otherwise, <c>false</c>.
        /// </value>
        public bool NeedUserInput { get; set; }

        /// <summary>
        /// Gets or sets the user input prompt.
        /// </summary>
        /// <value>
        /// The user input prompt.
        /// </value>
        public string UserInputPrompt { get; set; }

        /// <summary>
        /// Gets or sets the possible responses for the games current input request.
        /// </summary>
        /// <value>
        /// The possible responses.
        /// </value>
        public ICollection<UserResponse> PossibleUserResponses { get; set; }

        /// <summary>
        /// Gets or sets the users response.
        /// </summary>
        /// <value>
        /// The users response.
        /// </value>
        public UserResponse UserResponse { get; set; }

        /// <summary>
        /// Gets or sets the card that a simple yes no dialog card viewer can display.
        /// </summary>
        /// <value>
        /// The yes no dialog card viewer.
        /// </value>
        public ICard YesNoDialogCardViewer { get; set; }

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
        /// <exception cref="ArgumentOutOfRangeException">Thrown if _numberOfPlayers is less than 0.</exception>
        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                if (value < 2 || value > 4)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Must have between 2 and 4 players.");
                }
                _numberOfPlayers = value;
            }
        }

        /// <summary>
        /// The current state of the game.
        /// </summary>
        public GameState GameState { get; set; }

        /// <summary>
        ///     Populates decks of the 10 action cards, 3 treasure cards, and 6 victory cards for the Game.
        /// </summary>
        public virtual void GenerateCards(List<ICard> actionCardList)
        {
            BuyDeck.CardList.Clear();

            AddStartingTresureCards();
            AddStartingVictoryCards();
            if (actionCardList == null)
            {
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
            else
            {   
                foreach(var card in actionCardList)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        BuyDeck.AddCard(card.CreateCard());
                    }
                }
            }
        }
        /// <summary>
        /// calls the other generateCards method with null parameter
        /// </summary>
        public virtual void GenerateCards()
        {
            GenerateCards(null);
        }

        /// <summary>
        /// Blocking call to get input from the user.  Posts a response request that the UI has to pick up on and respond to
        /// </summary>
        /// <param name="possibleResponses">The possible responses the user can give.</param>
        /// <returns>The response that the user chose.</returns>
        public UserResponse GetUserResponse(ICollection<UserResponse> possibleResponses)
        {
            NeedUserInput = true;
            PossibleUserResponses = possibleResponses;
            while (NeedUserInput) { }
            return UserResponse;
        }

        /// <summary>
        ///     Creates players and deals them the proper number of cards.
        /// </summary>
        /// <param name="playerNames">A list of names for the players.</param>
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

                player.DrawPile.Shuffle(DateTime.Now.Second);

                player.PlayerState = PlayerState.Buy;
                player.Gold = 10;
                player.EndTurn();
                player.Game = this;
            }

            NextTurn();
        }

        /// <summary>
        ///     This method is called when a card is bought and will take a card out of the deck 
        ///     passed in by the parameter.
        /// </summary>
        /// <param name="name">The game of the card you want to sell the player.</param>
        /// <param name="player">The player you want to sell the card to.</param>
        /// <param name="x">The x cord the player wants the tile at.</param>
        /// <param name="y">The y cord the player wants the tile at.</param>
        /// <exception cref="ArgumentNullException">Thrown if no player is given.</exception>
        /// <exception cref="ArgumentException">Thrown if the name of the card is not in the BuyDeck</exception>
        /// <returns>True if the card was bought.</returns>
        public bool BuyCard(string name, IPlayer player, int x = 0, int y = 0)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (name == null) throw new ArgumentNullException(nameof(name));
          
            var card = BuyDeck.GetFirstCard(c => c.Name == name);
            if (card == null) return false;

            // Check to see if the player has enough coupons to buy the card.
            // TODO: Might want to make this ask the play if they want to use coupons or coins.
            if (player.Coupons >= card.CardCost)
            {
                card.Location = new Point(x, y);
                player.GiveCard(card, false);
                player.Coupons -= card.CardCost;
                return true;
            }

            if (player.Gold < card.CardCost || player.Investments < 1)
            {
                BuyDeck.AddCard(card);
                return false;
            }

            card.Location = new Point(x, y);
            player.GiveCard(card, false);
            player.Investments--;
            player.Gold -= card.CardCost;
            return true;
        }

        /// <summary>
        ///     The deck of cards that are in all the "draw" piles.
        /// </summary>
        public IDeck BuyDeck { get; set; }

        /// <summary>
        ///     The games global trash deck.
        /// </summary>
        /// <remarks>All players need a reference to this object.</remarks>
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
        /// <param name="length">Length of range for randomized list.</param>
        /// <returns>List of numbers in random order.</returns>
        /// <exception cref="ArgumentException">Thrown if the length is less than or equal to 1</exception>
        public static IEnumerable<int> RandomListOfSequentialNumbers(int length)
        {
            if(length <= 0)
            {
                throw new ArgumentException(nameof(length), "Random Card List was not populated");
            }

            var cardNumbers = new List<int>();

            for (var i = 0; i < length; i++)
            {
                cardNumbers.Add(i);
            }

            return cardNumbers.Randomize(_seed);
        }

        /// <summary>
        ///     Starts the turn of the next player in the Game.
        /// </summary>
        /// <exception cref="Exception">Thrown if there is np players in the game</exception>
        public void NextTurn()
        {
            Players[CurrentPlayer].EndTurn();

            CurrentPlayer++;
            CurrentPlayer %= NumberOfPlayers;

            HandleGameOverConditions();

            Players[CurrentPlayer].StartTurn();
        }

        /// <summary>
        /// Checks to see if the game is over, and if it is then properly ends the game.
        /// </summary>
        /// <returns>True if the game is over.</returns>
        private bool HandleGameOverConditions()
        {
            if (BuyDeck.SubDeck(x => x.Name == "Rose-Hulman").CardList.Count == 0)
            {
                EndGame();
                return true;
            }
            if (BuyDeck.NumberOfDepletedNames() >= 3)
            {
                EndGame();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void EndGame()
        {
            GameState = GameState.Ended;

            var vpMax = 0;
            var firstPlacePlayers = new List<IPlayer>();

            foreach (var player in Players)
            {
                var playerVp = player.VictoryPoints;
                if (player.VictoryPoints > vpMax)
                {
                    firstPlacePlayers.Clear();
                    firstPlacePlayers.Add(player);
                    vpMax = playerVp;
                }
                else if (player.VictoryPoints == vpMax)
                {
                    firstPlacePlayers.Add(player);
                }
            }

            foreach (var firstPlacePlayer in firstPlacePlayers)
            {
                firstPlacePlayer.Winner = true;
            }
        }
    }
}