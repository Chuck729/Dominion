namespace RHFYP.Cards.ActionCards
{
    public class Army : Card // Militia
    {
        public Army()
            : base(
                4, "Army", "Scares all but 3 civilians away from all other players.  +2 coins", CardType.Action, 0,
                "army")
        {
        }

        /// <summary>
        ///     Performs the action of the Army Card
        /// </summary>
        /// <param name="player">Player playing the card.</param>
        /// <param name="game">The game the card is in.</param>
        public override void PlayCard(Player player, Game game)
        {
            player.AddGold(2);

            foreach (var p in game.Players)
            {
                if (p.Equals(player) || p.HandContainsMilitaryBase()) continue;

                for (var i = 0; i < 2; i++)
                {
                    p.DrawHandToDiscard();
                }
            }
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