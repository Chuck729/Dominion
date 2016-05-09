namespace RHFYP.Cards.ActionCards
{
    public class StartUp : Card // Feast
    {
        public StartUp() : base(4, "StartUp", "Replace this tile with a tile costing up to 5", CardType.Action, 0, "startup")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new StartUp();
        }
    }
}
