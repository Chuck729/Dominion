﻿using System;

namespace RHFYP.Cards.TreasureCards
{
    public class SmallBusiness : Card
    {
        public SmallBusiness()
            : base(1, "Small Business", "Provides 1 coin when activated", CardType.Treasure, 0, "familybusiness")
        {
        }

        /// <summary>
        ///     Gives the player one gold.
        /// </summary>
        /// <param name="player">Player.</param>
        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException();
            player.AddGold(1);
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new SmallBusiness();
        }
    }
}