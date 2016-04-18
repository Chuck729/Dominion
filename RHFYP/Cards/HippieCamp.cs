using System;

namespace RHFYP.Cards
{
    public class HippieCamp : Card
    {
        public HippieCamp() : base(0, "Hippie Camp", "-1 Victory Point at the end of the Game", CardType.Victory, -1, "")
        {

        }

        /// <summary>
        /// Does nothing when played.
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
            return new HippieCamp();
        }
    }
}
