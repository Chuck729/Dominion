namespace RHFYP.Cards
{
    public class StartUp : Card // Feast
    {
        public StartUp() : base(4, "StartUp", "Replace this tile with a tile costing up to 5", CardType.Action, 0, "startup")
        {
        }
        /// <summary>
        /// trash the card and add a card to player's discard pile that costs 5 or less
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
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
