using System;

namespace RHFYP.Cards
{
    public class Company : Card
    {

        public Company() : base(3, "Company", "This building gives 2 coins when activated", CardType.Treasure, 0, "company")
        {

        }

        /// <summary>
        /// Adds 2 to the player's gold amount.
        /// </summary>
        /// <param name="player"></param> The player that played this card.
        public override void PlayCard(Player player)
        {
            if (player == null) throw new ArgumentNullException();
            player.AddGold(2);
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Company();
        }

    }
}
