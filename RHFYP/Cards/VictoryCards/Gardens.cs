using System;

namespace RHFYP.Cards.VictoryCards
{
    public class Gardens : Card
    {
        // TODO: This card has special victory point requirements. fix?
        public Gardens() : base(4, "Gardens", "+1 vp for evry 10 tiles on map (rounded down)", CardType.Victory, 0, "garden")
        {
        }

        /// <summary>
        ///     Does nothing when played.
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
            return new Gardens();
        }
    }
}