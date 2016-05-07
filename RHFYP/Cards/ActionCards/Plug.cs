using System;

namespace RHFYP.Cards.ActionCards
{
    public class Plug : Card // Witch
    {
        public Plug() : base(5, "Plug", "+2 civilians.  Every other player gains a hippie camp.", CardType.Action, 0, "plug")
        {
        }
        public override void PlayCard(Player player, Game game)
        {
            player.DrawCard();
            player.DrawCard();

            foreach (var otherPlayer in game.Players)
            {
                if (otherPlayer == player) continue;
                var card = game.BuyDeck.GetFirstCard(c => c.Name == "Hippie Camp");
                if (card == null) break;
                otherPlayer.GiveCard(card);
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Plug();
        }
    }
}
