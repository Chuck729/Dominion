using System;

namespace RHFYP.Cards
{
    public class ConstructionSite : Card // Workshop
    {
        public ConstructionSite() : base(3, "Construction Site", "Transform into a tile costing up to 4", CardType.Action, 0, "constructionsite")
        {
        }
        /// <summary>
        /// remove card from hand and add a card that is worth up to 4
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
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
