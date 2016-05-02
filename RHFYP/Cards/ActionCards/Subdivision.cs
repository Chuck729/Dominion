using System;

namespace RHFYP.Cards.ActionCards
{
    public class Subdivision : Card // Council Room
    {
        public Subdivision() : base(5, "Subdivision", "+4 civilians +1 manager.  Each other person gains a civilian.", CardType.Action, 0, "subdivision")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Subdivision();
        }
    }
}
