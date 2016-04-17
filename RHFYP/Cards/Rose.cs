using System;

namespace RHFYP.Cards
{
    public class Rose : Card
    {
        public Rose() : base(8, "Rose-Hulman", "This card is worth 6 victory points at the end of the Game", "victory", 6, "rosehulman")
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
            return new Rose();
        }
    }
}
