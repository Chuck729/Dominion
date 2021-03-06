﻿using RHFYP.Cards.TreasureCards;
using RHFYP.Interfaces;
using System;

namespace RHFYP.Cards.ActionCards
{
    public class Museum : Card // Bureaucrat
    {
        public Museum() : base(4, "Museum", "Place a company that will be visited next turn.  Opponents civilians will stay on victory tiles next turn", CardType.Action, 0, "museum")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException("Card was played without a player");
            if (game == null) throw new ArgumentNullException("Card must be played in a game");
            player.DrawPile.CardList.Add(new Company());
            foreach (Player p in game.Players)
            {
                if (!p.HandContainsMilitaryBase() && !p.Equals(player))
                {
                    var victoryCard = p.Hand.GetFirstCard(card => card.Type == CardType.Victory);
                    if (victoryCard != null)
                        p.DrawPile.AddCard(victoryCard);
                }
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Museum();
        }
    }
}
