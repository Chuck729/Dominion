using System;

namespace RHFYP.Cards
{
    public class Corporation : Card
    {
        public Corporation() : base(6, "Corporation", "This card gves 3 coins when played", CardType.Treasure, 0, "internationalcorporation")
        {
            
        }

        public override void PlayCard(Player player)
        {
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
