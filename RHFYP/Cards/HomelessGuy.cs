using System;

namespace RHFYP.Cards
{
    public class HomelessGuy : Card // Cellar
    {
        public HomelessGuy() : base(2, "Homeless Guy", "Activates one of your tiles and allows you to randomly relolate any of your existing civilians", CardType.Action, 0, "")
        {
        }
        /// <summary>
        /// adds a manager to the player and draws a card for every card the player discards from their hand
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
        {
            player.Managers++;
            // TODO: Player picks a card to discard
            player.DrawCard();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new HomelessGuy();
        }
    }
}
