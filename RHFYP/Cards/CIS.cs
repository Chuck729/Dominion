﻿using System;

namespace RHFYP.Cards
{
    public class Cis : Card // Spy
    {
        public Cis() : base(4, "CIS", "+1 civilian +1 manager.  Each player reveals a card on top of thier deck and you get to discard it or put it back.", "action", 0, "cis")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            throw new NotImplementedException();
        }
    }
}
