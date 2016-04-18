using System;
using RHFYP.Cards;

namespace RHFYP
{
    public sealed class Player : IPlayer
    {
        private bool _treasurePlayedThisTurn;

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
        }

        public IDeck DiscardPile { get; set; }

        public IDeck DrawPile { get; set; }

        public int Gold { get; set; }

        public IDeck Hand { get; set; }

        public int Investments { get; set; }

        public int Managers { get; set; }

        public string Name { get; set; }

        public PlayerState PlayerState { get; set; }

        public void BuyCard(ICard card)
        {
            if (!CanAfford(card)) return;
            //TODO Remove card from the deck in Game where it came from
            DiscardPile.AddCard(card);
            Gold = Gold - card.CardCost;
            Investments--;
        }

        /// <summary>
        /// Takes a hand from the players draw pile and puts it into the players hand.
        /// </summary>
        /// <returns>True if a card was drawn.</returns>
        /// <remarks>The discard deck should be shuffled into the players hand if there are no more cards.</remarks>
        public bool DrawCard()
        {
            return false;
        }

        public bool CanAfford(ICard card)
        {
            return (Gold >= card.CardCost);
        }

        public void EndActions()
        {
            if (PlayerState == PlayerState.Action)
            {
                PlayerState = PlayerState.Buy;
            }
            else
                throw new AccessViolationException("This method should not"
                                                   + " have been called because the PlayerState was not currently "
                                                   + "set to Action");
        }

        public void EndTurn()
        {
            if (PlayerState != PlayerState.Buy && PlayerState != PlayerState.Action) return;

            //IDeck discards = new Deck(Hand.DrawCards(Hand.CardCount()));
            DiscardPile = DiscardPile.AppendDeck(Hand.DrawCards(Hand.CardCount()));

            // Draw 5 cards.
            while (Hand.CardCount() < 5 && DrawPile.CardCount() != 0)
                Hand.AddCard(DrawPile.DrawCard());

            PlayerState = PlayerState.TurnOver;
        }

        public void PlayAllTreasures()
        {
            for (var x = Hand.CardCount() - 1; x >= 0; x--)
            {
                if (Hand.CardList[x].Type.Equals(CardType.Treasure))
                {
                    PlayCard(Hand.CardList[x]);
                }
            }
        }

        public bool PlayCard(ICard card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card), "PlayCard passed a null card");
            if (PlayerState != PlayerState.Action) return false;
            if (!Hand.CardList.Remove(card)) return false;

            if (_treasurePlayedThisTurn && card.Type == CardType.Action) return false;
            if (card.Type == CardType.Treasure) _treasurePlayedThisTurn = true;

            card.PlayCard(this);
            card.IsAddable = true;
            DiscardPile.AddCard(card);
            return true;
        }

        public void StartTurn()
        {
            PlayerState = PlayerState.Action;
            _treasurePlayedThisTurn = false;
            Gold = 0;
            Investments = 1;
            Managers = 1;
        }

        /// <summary>
        ///     Adds given amount of gold to player
        /// </summary>
        /// <param name="amount"></param>
        public void AddGold(int amount)
        {
            Gold += amount;
        }
    }
}