using System;

namespace RHFYP.Cards
{
    public class Museum : Card // Bureaucrat
    {
        public Museum() : base(4, "Museum", "Place a company that will be visited next turn.  One of each Opponent's civilians will stay on victory tiles next turn", CardType.Action, 0, "museum")
        {
        }
        /// <summary>
        /// a company is placed on top of the draw pile and each opponet puts a victory card on top of 
        /// their deck if they have one in thier hand
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
            return new Museum();
        }
    }
}
