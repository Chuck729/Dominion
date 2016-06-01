using System.Collections.Generic;
using RHFYP.Interfaces;

namespace RHFYP.Cards.ActionCards
{
    public class Prison : Card // Thief
    {
        public Prison()
            : base(
                4, "Prison",
                "Each other player reveals the next two tiles that will be visited. You may steal or trash a treasure tile if one is revealed.  Discard the other tile.",
                CardType.Action, 0, "prison")
        {
            NeedsOwnThread = true;
        }

        public override void PlayCard(Player player, Game game)
        {
            foreach (var player1 in game.Players)
            {
                if (player1 == player) continue;

                player1.DrawPile = player1.DrawPile.AppendDeck(player1.DiscardPile);
                for (var i = 0; i < 2; i++)
                {
                    var c = player1.DrawPile.DrawCard();
                    if (c == null) break;

                    if (c.Type == CardType.Treasure)
                    {
                        game.UserInputPrompt = "Would you like to steal or trash this card?";
                        game.PublicCardForUiUserInput = c;

                        var stealingCard =
                            game.GetUserResponse(new List<UserResponse> {UserResponse.Trash, UserResponse.Steal});

                        if (stealingCard != UserResponse.Steal) continue;
                        player.GiveCard(c);
                        break;
                    }
                    player1.DiscardPile.AddCard(c);
                }
            }
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