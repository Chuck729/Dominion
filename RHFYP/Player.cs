using RHFYP.Cards;
using System;

namespace RHFYP
{
    public class Player : IPlayer
    {
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
            throw new NotImplementedException();
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