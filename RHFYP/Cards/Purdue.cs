using System;

namespace RHFYP.Cards
{
    public class Purdue : Card
    {
        public Purdue() : base(3, "Purdue", "This card is worth 1 victory point at the end of the Game", "victory", 1, "purdue")
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
            return new Purdue();
        }
    }
}
