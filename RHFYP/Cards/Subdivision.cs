using System;

namespace RHFYP.Cards
{
    public class Subdivision : Card // Council Room
    {
        public Subdivision() : base(5, "Subdivision", "+4 civilians +1 manager.  Each other person gains a civilian.", CardType.Action, 0, "subdivision")
        {
        }
        /// <summary>
        /// draw 4 cards and add a manager to the player
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
            return new Subdivision();
        }
    }
}
