using System;
using System.Collections.Generic;
using RHFYP.Interfaces;

namespace RHFYP.Cards.ActionCards
{
    public class Cia : Card // Spy
    {
        public Cia() : base(4, "CIA", "+1 civilian +1 manager.  Each player reveals a card on top of their deck and you get to discard it or put it back.", CardType.Action, 0, "cia")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (game == null) throw new ArgumentNullException(nameof(game));

            for (int i = 0; i < game.NumberOfPlayers; i++)
            {
                if (game.Players[i] != player)
                {
                    ICard c = game.Players[i].DrawPile.DrawCard();

                    game.UserInputPrompt = "Would you like to discard this card or put on top of players deck?";
                    game.YesNoDialogCardViewer = c;

                    var keepingCard = game.GetUserResponse(new List<UserResponse> { UserResponse.Discard, UserResponse.PutOnDeck });

                    if (keepingCard == UserResponse.PutOnDeck)
                    {
                        game.Players[i].DrawPile.AddCard(c);
                    } else
                    {
                        game.Players[i].DiscardPile.AddCard(c);
                    }
                }
            }

            player.DrawCard();
            player.Managers++;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Cia();
        }
    }
}
