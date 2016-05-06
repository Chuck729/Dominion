using RHFYP.Cards.TreasureCards;
using System;

namespace RHFYP.Cards.ActionCards
{
    public class SpeedyLoans : Card // Money lender
    {
        public SpeedyLoans() : base(4, "Speedy Loans", "Destory a small business and +3 coins.", CardType.Action, 0, "speedyloans")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException("Card was played without a player");
            if (game == null) throw new ArgumentNullException("Card must be played in a game");
            ICard smallBusiness = player.Hand.GetFirstCard(card => card is SmallBusiness);
            if (smallBusiness != null)
            {
                player.TrashPile.CardList.Add(smallBusiness);
                player.Gold += 3;
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new SpeedyLoans();
        }
    }
}
