using System;

namespace RHFYP.Cards
{
    public class HippieCamp : Card
    {
        public HippieCamp() : base(0, "Hippie Camp", "-1 Victory Point at the end of the Game", "curse", -1, "")
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
            return new HippieCamp();
        }
    }
}
