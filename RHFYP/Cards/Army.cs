using System;

namespace RHFYP.Cards
{
    public class Army : Card // Militia
    {
        public Army() : base(4, "Army", "Scares all but 3 civilians away from all other players.  +2 coins", CardType.Action, 0, "army")
        {
            
        }
        /// <summary>
        /// makes each player discard from hand deck until they have 3 cards left in their hand
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
        {
            
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
