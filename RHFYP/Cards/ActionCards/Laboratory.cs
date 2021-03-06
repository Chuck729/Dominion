﻿using System;

namespace RHFYP.Cards.ActionCards
{
    public class Laboratory : Card
    {
        public Laboratory() : base(5, "Laboratory", "+2 civilians +1 manager", CardType.Action, 0, "laboratory")
        {
        }

        /// <summary>
        ///     The player's Manager count increases by one for the turn and the player draws two cards.
        /// </summary>
        /// <param name="player"> The player that played this card. </param>
        /// <exception cref="ArgumentNullException"> Throws exception if the player that is passed in does not exist. </exception>
        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException("Card was played without a player");
            if (game == null) throw new ArgumentNullException("Card must be played in a game");
            player.DrawCard();
            player.DrawCard();
            player.Managers++;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns> A new card object. </returns>
        public override ICard CreateCard()
        {
            return new Laboratory();
        }
    }
}
