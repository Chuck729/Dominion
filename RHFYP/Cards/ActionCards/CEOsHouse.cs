﻿using System;

namespace RHFYP.Cards.ActionCards
{
    public class CeosHouse : Card // Throne room
    {
        public CeosHouse() : base(4, "Ceo's House", "The next tile that you activate with a manager will be played twice", CardType.Action, 0, "ceoshouse")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.NextPlayCount = 2;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new CeosHouse();
        }
    }
}
