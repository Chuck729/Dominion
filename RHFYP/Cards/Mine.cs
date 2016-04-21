using System;

namespace RHFYP.Cards
{
    public class Mine : Card
    {
        public Mine() : base(5, "Mine", "Upgrade a treasure tile.", CardType.Action, 0, "mine")
        {
        }
        /// <summary>
        /// a player can take a treasure card in their hand and upgrade it to a card costing up 
        /// to 3 more than the selected card
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Mine();
        }
    }
}
