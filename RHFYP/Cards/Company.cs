using System;

namespace RHFYP.Cards
{
    public class Company : Card
    {

        public Company() : base(3, "Company", "This building gives 3 coins when activated", "treasure", 0, "company")
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
            return new Company();
        }

    }
}
