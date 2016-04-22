using System;

namespace RHFYP.Cards
{
    public class Purdue : Card
    {
        public Purdue() : base(3, "Purdue", "This card is worth 1 victory point at the end of the Game", CardType.Victory, 1, "purdue")
        {

        }

        /// <summary>
        /// Does nothing when played.
        /// </summary>
        /// <param name="player"></param> Player that played this card.
        public override void PlayCard(Player player)
        {
            if (player == null) throw new ArgumentNullException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Purdue();
        }
    }
}
