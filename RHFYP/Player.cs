using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Interfaces;

namespace RHFYP
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Player : IPlayer
    {
        private static readonly Random Random = new Random();

        public Player(string name)
        {
            DrawPile = new Deck();
            DiscardPile = new Deck();
            Hand = new Deck();
            Gold = 0;
            Investments = 0;
            Managers = 0;
            PlayerState = PlayerState.Action;
            Name = name;
            NextPlayCount = 1;
            NextPlayCountChanged = false;
            HomelessGuyMode = false;
            CardsToDrawAfterHomelessGuyMode = 0;
        }

        public IDeck TrashPile { get; set; }
        public int Nukes { get; set; }

        public Game Game { get; set; }

        public IDeck DiscardPile { get; set; }

        public IDeck DrawPile { get; set; }

        public int Gold { get; set; }

        public IDeck Hand { get; set; }

        public int Investments { get; set; }

        public int Managers { get; set; }

        public string Name { get; set; }

        public bool ActionCardsInHand => Hand.SubDeck(card => card.Type == CardType.Action).CardList.Count != 0;

        public bool TreasureCardsInHand => Hand.SubDeck(card => card.Type == CardType.Treasure).CardList.Count != 0;

        public PlayerState PlayerState { get; set; }

        public int VictoryPoints
        {
            get
            {
                return
                    Hand.AppendDeck(DiscardPile.AppendDeck(DrawPile))
                        .SubDeck(x => x.Type == CardType.Victory)
                        .CardList.Sum(card => card.VictoryPoints);
            }
        }

        public bool Winner { get; set; }

        public int NextPlayCount { get; set; }

        public bool NextPlayCountChanged { get; set; }

        public virtual bool GiveCard(ICard card, bool randomLoc = true)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));

            var possiblePoints = new List<Point>();
            foreach (var c in Hand.AppendDeck(DrawPile.AppendDeck(DiscardPile)).CardList)
            {
                possiblePoints.Add(new Point(c.Location.X + 1, c.Location.Y));
                possiblePoints.Add(new Point(c.Location.X - 1, c.Location.Y));
                possiblePoints.Add(new Point(c.Location.X, c.Location.Y + 1));
                possiblePoints.Add(new Point(c.Location.X, c.Location.Y - 1));
            }
            var distinctPoints = possiblePoints.Distinct().ToList();

            for (var i = distinctPoints.Count - 1; i >= 0; i--)
            {
                var remove =
                    Hand.AppendDeck(DrawPile.AppendDeck(DiscardPile))
                        .CardList.Any(c => distinctPoints[i].Equals(c.Location));
                if (remove) distinctPoints.RemoveAt(i);
            }

            if (randomLoc)
                card.Location = distinctPoints.Count == 0
                    ? new Point(20, 20)
                    : distinctPoints[Random.Next(0, distinctPoints.Count)];

            DiscardPile.AddCard(card);
            return true;
        }

        /// <summary>
        ///     Looks through all of the players cards, in no particular order,
        ///     and looks for
        ///     <param name="card"></param>
        ///     . If it finds the
        ///     <param name="card"></param>
        ///     then it will move that
        ///     <param name="card"></param>
        ///     to the trash pile.
        /// </summary>
        /// <param name="card">The card to trash.</param>
        /// <returns>True if the card was found and trashed.</returns>
        public virtual bool TrashCard(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), "Must supply a card to trash!");
            }

            if (DrawPile.InDeck(card))
            {
                DrawPile.CardList.Remove(card);
                TrashPile.AddCard(card);
                return true;
            }

            if (Hand.InDeck(card))
            {
                Hand.CardList.Remove(card);
                TrashPile.AddCard(card);
                Nukes = Math.Max(Nukes - 1, 0);
                return true;
            }

            if (!DiscardPile.InDeck(card)) return false;
            DiscardPile.CardList.Remove(card);
            TrashPile.AddCard(card);
            return true;
        }

        /// <summary>
        ///     Takes a card from the players draw pile and puts it into the players hand.
        /// </summary>
        /// <returns>True if a card was drawn.</returns>
        /// <remarks>The discard deck should be shuffled into the players hand if there are no more cards.</remarks>
        public virtual bool DrawCard()
        {
            return DrawCard(x => true);
        }

        /// <summary>
        ///     Takes a card that matches the predicate from the players draw pile and puts it into the players hand.
        /// </summary>
        /// <returns>True if a card was drawn.</returns>
        /// <remarks>The discard deck should be shuffled into the players hand if there are no more cards.</remarks>
        public virtual bool DrawCard(Predicate<ICard> pred)
        {
            if (DrawPile.SubDeck(pred).CardList.Count == 0 && DiscardPile.SubDeck(pred).CardList.Count == 0)
                return false;

            if (DrawPile.SubDeck(pred).CardList.Count == 0) DrawPile.ShuffleIn(DiscardPile, DateTime.Now.Second);

            Hand.AddCard(DrawPile.GetFirstCard(pred));

            return true;
        }

        public bool CanAfford(ICard card)
        {
            return Gold >= card.CardCost;
        }

        public bool DiscardToDeckAtEndOfTurn { get; set; }

        public int Coupons { get; set; }

        public bool HomelessGuyMode { get; set; }

        public int CardsToDrawAfterHomelessGuyMode { get; set; }

        public void EndActions()
        {
            if (PlayerState == PlayerState.Action)
            {
                PlayerState = PlayerState.Buy;
                var card = Hand.GetFirstCard(x => x.Type == CardType.Action);
                while (card != null)
                {
                    DrawPile.AddCard(card);
                    card = Hand.GetFirstCard(x => x.Type == CardType.Action);
                }
            }
            else
                throw new InvalidOperationException("This method should not"
                                                    + " have been called because the PlayerState was not currently "
                                                    + "set to Action");
        }

        public void EndTurn()
        {
            //IDeck discards = new Deck(Hand.DrawCards(Hand.CardList.Count));
            if (!DiscardToDeckAtEndOfTurn)
            {
                DrawPile = DrawPile.AppendDeck(DiscardPile);
                DiscardPile.CardList.Clear();
                DrawPile.Shuffle(DateTime.Now.Second);
            }
            else
            {
                DrawPile.CardList.InsertRange(DrawPile.CardList.Count, Hand.CardList);
                Hand.CardList.Clear();
                DiscardToDeckAtEndOfTurn = false;
            }

            // Draw 5 cards.
            while (Hand.CardList.Count < 5)
                if (DrawCard() == false)
                    break;

//            // Play all the victory cards to get an updated VP count.
            foreach (
                var victoryCard in
                    DrawPile.AppendDeck(Hand.AppendDeck(DiscardPile))
                        .SubDeck(card => card.Type == CardType.Victory)
                        .CardList)
            {
                victoryCard.PlayCard(this, Game);
            }

            PlayerState = PlayerState.TurnOver;
        }

        public void PlayAllTreasures()
        {
            for (var x = Hand.CardList.Count - 1; x >= 0; x--)
            {
                if (Hand.CardList[x].Type.Equals(CardType.Treasure))
                {
                    PlayCard(Hand.CardList[x]);
                }
            }
        }

        public bool PlayCard(ICard card)
        {
            if (PlayerState == PlayerState.TurnOver) return false;
            var managerChange = 0;

            if (card == null) throw new ArgumentNullException(nameof(card), "PlayCard passed a null card");

            if (Nukes > 0) return NukeCard(card);

            if (HomelessGuyMode)
            {
                Hand.CardList.Remove(card);
                DiscardPile.AddCard(card);
                CardsToDrawAfterHomelessGuyMode++;
                return true;
            }

            if (PlayerState != PlayerState.Action && card.Type == CardType.Action) return false;

            if (card.Type == CardType.Action)
            {
                if (Managers <= 0) return false;
                managerChange = -1;
            }

            if (!Hand.CardList.Remove(card)) return false;

            if (card.Type == CardType.Action)
            {
                for (var i = 0; i < NextPlayCount; i++)
                {
                    if (!card.CanPlayCard(this, Game))
                    {
                        DiscardPile.AddCard(card);
                        return false;
                    }
                    var thread = new Thread(() => card.PlayCard(this, Game));
                    thread.Start();
                }
            }
            else
            {
                if (!card.CanPlayCard(this, Game))
                {
                    DiscardPile.AddCard(card);
                    return false;
                }
                var thread = new Thread(() => card.PlayCard(this, Game));
                thread.Start();
            }

            if (!card.TrashOnAdd) DiscardPile.AddCard(card);
            else TrashPile.AddCard(card);
            card.TrashOnAdd = false;

            Managers += managerChange;

            if (!NextPlayCountChanged) NextPlayCount = 1;
            NextPlayCountChanged = false;

            return true;
        }

        public void StartTurn()
        {
            PlayerState = PlayerState.Action;
            Gold = 0;
            Investments = 1;
            Managers = 1;

            // god mode
            if (Name == "o")
            {
                Gold = 2000;
                Investments = 2000;
                Managers = 2000;
            }
        }

        public void DrawAfterHomelessGuyMode()
        {
            for (var i = 0; i < CardsToDrawAfterHomelessGuyMode; i++)
            {
                DrawCard();
            }
            CardsToDrawAfterHomelessGuyMode = 0;
            HomelessGuyMode = false;
        }

        private bool NukeCard(ICard card)
        {
            if (!Hand.CardList.Remove(card)) return false;
            Nukes--;
            return true;
        }

        /// <summary>
        ///     Adds given amount of gold to player
        /// </summary>
        /// <param name="amount"></param>
        public virtual void AddGold(int amount)
        {
            Gold += amount;
        }

        /// <summary>
        ///     Draws the top card from the players hand and adds it to the
        ///     discard pile
        /// </summary>
        public virtual void DrawHandToDiscard()
        {
            DiscardPile.AddCard(Hand.DrawCard());
        }

        /// <summary>
        ///     Returns true if the player currently has a military base in thier hand
        /// </summary>
        /// <returns></returns>
        public bool HandContainsMilitaryBase()
        {
            return Hand?.CardList != null && Hand.CardList.OfType<MilitaryBase>().Any();
        }
    }
}