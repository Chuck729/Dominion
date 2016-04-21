using System;

namespace RHFYP.Cards
{
    public class Area51 : Card // Chapel
    {
        public Area51() : base(2, "Area 51", "Nuke up to 4 tiles on the map", CardType.Action, 0, "area51")
        {
        }
        /// <summary>
        /// remove up to 4 tiles from the map
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Area51();
        }
    }
}
