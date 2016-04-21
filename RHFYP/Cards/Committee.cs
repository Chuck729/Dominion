using System;

namespace RHFYP.Cards
{
    public class Committee : Card // Remodel
    {
        public Committee() : base(4, "Committee", "Upgrade a tile in your hand to something costing up to 2 more than it.", CardType.Action, 0, "committee")
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
            return new Committee();
        }
    }
}
