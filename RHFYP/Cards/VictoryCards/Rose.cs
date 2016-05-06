using System;

namespace RHFYP.Cards.VictoryCards
{
    public class Rose : Card
    {
        public Rose() : base(8, "Rose-Hulman", "This card is worth 6 victory points at the end of the Game", CardType.Victory, 6, "rosehulman")
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
            return new Rose();
        }
    }
}
