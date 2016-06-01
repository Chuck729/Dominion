using System.Collections.Generic;
using RHFYP.Interfaces;

namespace RHFYP.Cards.ActionCards
{
    public class Committee : Card // Remodel
    {
        public Committee()
            : base(
                4, "Committee", "Upgrade a tile in your hand to something costing up to 2 more than it.",
                CardType.Action, 0, "committee")
        {
            NeedsOwnThread = true;
        }

        public override void PlayCard(Player player, Game game)
        {
            game.GetUserResponse(new List<UserResponse> {UserResponse.CardInHand});
            if (game.PublicCardForUiUserInput == null) return;
            if (!player.Hand.InDeck(game.PublicCardForUiUserInput)) return;
            player.Hand.CardList.Remove(game.PublicCardForUiUserInput);
            player.Coupons += game.PublicCardForUiUserInput.CardCost + 2;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Committee();
        }
    }
}