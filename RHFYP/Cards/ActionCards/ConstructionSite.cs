namespace RHFYP.Cards.ActionCards
{
    public class ConstructionSite : Card // Workshop
    {
        public ConstructionSite() : base(3, "Construction Site", "Transform into a tile costing up to 4", CardType.Action, 0, "constructionsite")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new ConstructionSite();
        }
    }
}
