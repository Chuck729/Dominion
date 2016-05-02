using System;

namespace RHFYP.Cards.ActionCards
{
    public class Museum : Card // Bureaucrat
    {
        public Museum() : base(4, "Museum", "Place a company that will be visited next turn.  Opponents civilians will stay on victory tiles next turn", CardType.Action, 0, "museum")
        {
        }

        public override void PlayCard(Player player, Game game)
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
