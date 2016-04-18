﻿using System;

namespace RHFYP.Cards
{
    public class Village : Card
    {
        public Village() : base(3, "Village", "+1 civilian +2 managers", CardType.Action, 0, "village")
        {
        }

        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.Managers += 2;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Village();
        }
    }
}
