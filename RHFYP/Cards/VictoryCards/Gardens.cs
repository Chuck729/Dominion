using System;

namespace RHFYP.Cards.VictoryCards
{
    public class Gardens : Card
    {
        private int _vpValue;

        /// <summary>The number of victory points each card is worth.</summary>
        public override int VictoryPoints => _vpValue;

        public Gardens() : base(4, "Gardens", "+1 vp for evry 10 tiles on map (rounded down)", CardType.Victory, 0, "garden")
        {
        }

        /// <summary>
        ///     Does nothing when played.
        /// </summary>
        /// <param name="player">The player who played the card.r</param>
        /// <param name="game">The game that this card is played in.</param>
        public override void PlayCard(Player player, Game game)
        {
                // ReSharper disable once PossibleLossOfFraction
                _vpValue = (int) Math.Floor((double) (player.DrawPile.AppendDeck(player.DiscardPile).AppendDeck(player.Hand).CardList.Count/10));
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