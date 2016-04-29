using System;

namespace RHFYP.Cards.TreasureCards
{
    public class Corporation : Card
    {
        public Corporation() : base(6, "Corporation", "This card gves 3 coins when played", CardType.Treasure, 0, "internationalcorporation")
        {
            
        }

        /// <summary>
        ///     The player's gold count increases by 3.
        /// </summary>
        /// <param name="player">  The player who played this card. </param>
        /// <exception cref="ArgumentNullException"> Throws exception if player that is passed in does not exist. </exception>
        public override void PlayCard(Player player)
        {
            if (player == null) throw new ArgumentNullException("Player is null.");
            player.AddGold(3);
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns> A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Corporation();
        }
    }
}
