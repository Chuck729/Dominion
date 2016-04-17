using System;

namespace RHFYP.Cards
{
    public class Mit : Card
    {
        public Mit() : base(5, "MIT", "This card is worth 3 victory points at the end of the Game", "victory", 3, "mit")
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
            return new Mit();
        }
    }
}
