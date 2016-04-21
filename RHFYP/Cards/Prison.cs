using System;

namespace RHFYP.Cards
{
    public class Prison : Card // Thief
    {
        public Prison() : base(4, "Prison", "Each other player reveals the next two tiles that will be visited. You may steal or trash a treasure tile if one is revealed.  Discard the other tile.", CardType.Action, 0, "prison")
        {
        }
        /// <summary>
        /// look at each opponent's top 2 cards of their draw pile 
        /// if they are treasures, the player may trash one of them per opponent, the cards not trashed
        /// are discarded
        /// the player chooses to gain each of the trashed cards
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
            return new Prison();
        }
    }
}
