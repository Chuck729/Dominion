using System;

namespace RHFYP.Cards
{
    public class Prison : Card // Thief
    {
        public Prison() : base(4, "Prison", "Each other player reveals the next two tiles that will be visited. You may steal or trash a treasure tile if one is revealed.  Discard the other tile.", CardType.Action, 0, "prison")
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
            return new Prison();
        }
    }
}
