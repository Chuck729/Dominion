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
            if (player == null) throw new ArgumentNullException("Card was played without a player");
            if (game == null) throw new ArgumentNullException("Card must be played in a game");
            player.NextPlayCount = 2;
            player.NextPlayCountChanged = true;
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
