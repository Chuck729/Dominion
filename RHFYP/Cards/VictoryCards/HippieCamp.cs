using System;

namespace RHFYP.Cards.VictoryCards
{
    public class HippieCamp : Card
    {
        public HippieCamp() : base(0, "Hippie Camp", "-1 Victory Point at the end of the Game", CardType.Victory, -1, "homelessguy")
        {

        }

        /// <summary>
        /// Does nothing when played.
        /// </summary>
        /// <param name="player"></param> Player that played this card.
        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException();
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
