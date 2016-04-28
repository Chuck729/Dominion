using System;
using System.Collections.Generic;

namespace RHFYP.Cards
{
    public class Army : Card // Militia
    {
        public Army() : base(4, "Army", "Scares all but 3 civilians away from all other players.  +2 coins", CardType.Action, 0, "army")
        {
            
        }

        public override void PlayCard(Player player)
        {
            
        }

        /// <summary>
        /// Performs the action of the Army Card
        /// </summary>
        /// <param name="player">Player playing the card.</param>
        /// <param name="listOfPlayers">Players in the game.</param>
        public void PlayCard(Player player, List<Player> listOfPlayers)
        {
            player.AddGold(2);

            foreach (Player p in listOfPlayers)
            {
                if (!p.Equals(player) && !p.HandContainsMilitaryBase())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        p.DrawHandToDiscard();
                    }
                }
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Army();
        }
    }
}
