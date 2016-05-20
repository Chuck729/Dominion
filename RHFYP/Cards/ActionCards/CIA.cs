using System;

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

                    // bool keepingCard = player.PromptAction(c, yesNo();
                    // Look in Library for explanation of this method

                    // For now we will just assume the player wants the
                    // other player to discard his card.
                    bool keepingCard = false;

                    if (keepingCard)
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
