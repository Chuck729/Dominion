using System;

namespace RHFYP.Cards
{
    public class WallStreet : Card // Chancellor
    {
        public WallStreet() : base(3, "Wall Street", "+2 coins.  Shuffle the order of the civilians", CardType.Action, 0, "wallstreet")
        {
        }
        /// <summary>
        /// add all cards to draw pile if player chooses
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
            return new WallStreet();
        }
    }
}
