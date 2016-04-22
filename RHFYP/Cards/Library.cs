﻿using System;

namespace RHFYP.Cards
{
    class Library : Card
    {
        public Library() : base(5, "Library", "Civilians visit tiles until 7 are visited.  If they visit an action tile you can have that tile be visited next turn instead if you want.", CardType.Action, 0, "library")
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
            return new Library();
        }
    }
}
