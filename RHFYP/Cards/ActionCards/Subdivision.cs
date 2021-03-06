﻿namespace RHFYP.Cards.ActionCards
{
    public class Subdivision : Card // Council Room
    {
        public Subdivision()
            : base(
                5, "Subdivision", "+4 civilians +1 manager.  Each other person gains a civilian.", CardType.Action, 0,
                "subdivision")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.Managers++;
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();

            foreach (var player1 in game.Players)
            {
                if (player1 != player)
                {
                    player1.DrawCard();
                }
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Subdivision();
        }
    }
}