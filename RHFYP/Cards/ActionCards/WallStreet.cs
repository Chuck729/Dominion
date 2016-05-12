using System;

namespace RHFYP.Cards.ActionCards
{
    public class WallStreet : Card // Chancellor
    {
        public WallStreet() : base(3, "Wall Street", "+2 coins.  All civilians visit the same places on your next turn", CardType.Action, 0, "wallstreet")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.Gold += 2;
            player.DiscardToDeckAtEndOfTurn = true;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new WallStreet();
        }
    }
}
