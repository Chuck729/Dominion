using System;

namespace RHFYP.Cards
{
    public class Corporation : Card
    {
        public Corporation() : base(6, "Corporation", "This card gves 3 coins when played", CardType.Treasure, 0, "internationalcorporation")
        {
            
        }

        /// <summary>
        /// The player's gold count increases by 3.
        /// </summary>
        /// <param name="player"></param> The player who played this card.
        public override void PlayCard(Player player)
        {
            if (player == null) throw new ArgumentNullException();
            player.AddGold(3);
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Corporation();
        }
    }
}
