using System;
using System.Collections.Generic;
using RHFYP.Interfaces;

namespace RHFYP.Cards.ActionCards
{
    public class Library : Card
    {
        public Library()
            : base(
                5, "Library",
                "Civilians visit tiles until 7 are visited.  If they visit an action tile you can choose to set aside that tile. The set aside tiles are not visited.",
                CardType.Action, 0, "library")
        {
            NeedsOwnThread = true;
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException();
            if (game == null) throw new ArgumentNullException();

            // Move all cards to the draw pile and clear the discard pile.
            player.DiscardPile.Shuffle(DateTime.Now.Second);
            player.DrawPile = player.DrawPile.AppendDeck(player.DiscardPile);
            player.DiscardPile.CardList.Clear();

            while (player.Hand.CardList.Count < 7)
            {
                var c = player.DrawPile.DrawCard();
                if (c == null) break;

                if (c.Type == CardType.Action)
                {
                    game.UserInputPrompt = "Would you like to keep this card?";
                    game.PublicCardForUiUserInput = c;

                    var keepingCard = game.GetUserResponse(new List<UserResponse> {UserResponse.Yes, UserResponse.No});

                    if (keepingCard == UserResponse.Yes)
                    {
                        player.Hand.AddCard(c);
                    }
                    else
                    {
                        player.DiscardPile.AddCard(c);
                    }
                }
                else
                {
                    // If not an action card, simply add the card to the player's hand.
                    player.Hand.AddCard(c);
                }
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Library();
        }
    }
}