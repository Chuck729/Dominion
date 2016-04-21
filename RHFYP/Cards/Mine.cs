using System;

namespace RHFYP.Cards
{
    public class Mine : Card
    {
        public Mine() : base(5, "Mine", "Upgrade a treasure tile.", CardType.Action, 0, "mine")
        {
        }

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
