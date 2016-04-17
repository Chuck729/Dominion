using System;

namespace RHFYP.Cards
{
    public class Village : Card
    {
        public Village() : base(3, "Village", "+1 civilian +2 managers", "action", 0, "village")
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
            return new Village();
        }
    }
}
