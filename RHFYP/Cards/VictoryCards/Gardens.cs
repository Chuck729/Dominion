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
            if (player == null) throw new ArgumentNullException("Card played by a null player");
            if (game == null) throw new ArgumentNullException("Card must be played during a game");
            // ReSharper disable once PossibleLossOfFraction
            var allCards = player.DrawPile.AppendDeck(player.Hand.AppendDeck(player.DiscardPile));
            var totalCards = allCards.CardList.Count;
            _vpValue = (int) Math.Floor(totalCards / 10.0);
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