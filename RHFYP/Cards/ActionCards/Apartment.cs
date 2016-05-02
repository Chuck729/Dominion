using System;

namespace RHFYP.Cards.ActionCards
{
    public class Apartment : Card // Village
    {
        public Apartment() : base(3, "Apartment", "+2 Managers and +1 Civilian", CardType.Action, 0, "apartments")
        {
            
        }

        /// <summary>
        ///     Adds two to the player's Manager amount and the player draws a card.
        /// </summary>
        /// <param name="player"> The player that played this card. </param>
        /// <exception cref="ArgumentNullException"> Throws exception if player that is passed in does not exist. </exception>
        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException();
            player.DrawCard();
            player.Managers += 2;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns> A new card object. </returns>
        public override ICard CreateCard()
        {
            return new Apartment();
        }
    }
}
