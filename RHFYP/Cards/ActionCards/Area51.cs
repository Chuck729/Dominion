using System;

namespace RHFYP.Cards.ActionCards
{
    public class Area51 : Card // Chapel
    {
        public Area51()
            : base(2, "Area 51", "Nuke up to 4 tiles that civilians are currently at", CardType.Action, 0, "area51")
        {
        }

        /// <summary>
        ///     Playing this card will remove up to four selected cards
        ///     in the current player's hand and place them in the trash
        ///     pile.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// The player that played this card.
        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            player.Nukes += 4;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Area51();
        }
    }
}