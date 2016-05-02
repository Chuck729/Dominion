using System;

namespace RHFYP.Cards.TreasureCards
{
    public class Company : Card
    {

        public Company() : base(3, "Company", "This building gives 2 coins when activated", CardType.Treasure, 0, "company")
        {

        }

        /// <summary>
        ///     Adds 2 to the player's gold amount.
        /// </summary>
        /// <param name="player"> The player that played this card. </param> 
        /// <exception cref="ArgumentNullException"> Throws exception if player that is passed in does not exist. </exception>
        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException();
            player.AddGold(2);
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns> A new card object. </returns>
        public override ICard CreateCard()
        {
            return new Company();
        }

    }
}
