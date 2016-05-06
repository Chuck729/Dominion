using System;

namespace RHFYP.Cards.ActionCards
{
    public class Scholarship : Card // Market
    {
        public Scholarship() : base(5, "Scholarship", "+1 civilian +1 manager +1 coin +1 investment", CardType.Action, 0, "scholarship")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException("Card was played without a player");
            player.DrawCard();
            player.Managers++;
            player.Gold++;
            player.Investments++;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Scholarship();
        }
    }
}
