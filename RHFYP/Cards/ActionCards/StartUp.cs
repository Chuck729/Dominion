namespace RHFYP.Cards.ActionCards
{
    public class StartUp : Card // Feast
    {
        public StartUp() : base(4, "Start Up", "Replace this tile with a tile costing up to 5", CardType.Action, 0, "startup")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.Coupons += 5;
            TrashOnAdd = true;
        }

        public override bool CanPlayCard(Player player, Game game)
        {
            return (player.Coupons == 0);
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
