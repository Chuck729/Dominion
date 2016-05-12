using System;

namespace RHFYP.Cards
{
    public class BorderCard : Card
    {
        public BorderCard() : base(0, "Border Card", "Used to display where a player can build", CardType.System, 0, "bordertile")
        {
        }

        /// <summary>
        /// Shouldn't do anything because you never play border cards.
        /// </summary>
        /// <param name="player">Not used.</param>
        /// <param name="game">Not used.</param>
        public override void PlayCard(Player player, Game game)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new BorderCard();
        }
    }
}
