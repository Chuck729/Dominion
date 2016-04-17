using System;

namespace RHFYP.Cards
{
    public class Army : Card // Militia
    {
        public Army() : base(4, "Army", "Scares all but 3 civilians away from all other players.  +2 coins", "action", 0, "army")
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
            return new Army();
        }
    }
}
