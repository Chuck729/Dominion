using RHFYP.Cards;
using System;

namespace RHFYP
{
    public class Player : IPlayer
    {
        public Player()
        {
            DrawPile = new Deck();
            DiscardPile = new Deck();
            Hand = new Deck();
            Gold = 0;
            Investments = 0;
            Managers = 0;

        }

        public Deck DiscardPile { get; set; }

        public Deck DrawPile { get; set; }

        public int Gold { get; set; }

        public Deck Hand { get; set; }

        public int Investments { get; set; }

        public int Managers { get; set; }

        public string Name { get; set; }

        public PlayerState PlayerState { get; set; }

        public void BuyCard(ICard card)
        {
            if (card.CanAfford(this))
            {
                //TODO Remove card from the deck in Game where it came from
                DiscardPile.AddCard(card);
                Gold = Gold - card.CardCost;
                Investments--;
            }
        }

        public void EndActions()
        {
            throw new NotImplementedException();
        }

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void PlayAllTreasures()
        {
            throw new NotImplementedException();
        }

        public void PlayCard(ICard card)
        {
            throw new NotImplementedException();
        }

        public void StartTurn()
        {
            throw new NotImplementedException();
        }
    }
}