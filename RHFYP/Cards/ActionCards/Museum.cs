using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;
using System;

namespace RHFYP.Cards.ActionCards
{
    public class Museum : Card // Bureaucrat
    {
        public Museum() : base(4, "Museum", "Place a company that will be visited next turn.  Opponents civilians will stay on victory tiles next turn", CardType.Action, 0, "museum")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.DrawPile.CardList.Add(new Company());
            foreach (Player p in game.Players)
            {
                if (!p.HandContainsMilitaryBase() && !p.Equals(player))
                {
                    if(p.Hand.CardList.Find(card => card.Type == CardType.Victory) != null)
                    p.DrawPile.AddCard(p.Hand.GetFirstCard(card => card.Type == CardType.Victory));
                }
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Museum();
        }
    }
}
