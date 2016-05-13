using System;

namespace RHFYP.Cards.ActionCards
{
    public class HomelessGuy : Card // Cellar
    {
        public HomelessGuy() : base(2, "Homeless Guy", "+1 Manager.  Activates one of your tiles and allows you to randomly relocate any of your existing civilians", CardType.Action, 0, "homelessguy")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException("Card was played without a player");
            if (game == null) throw new ArgumentNullException("Card must be played in a game");
            player.HomelessGuyMode = true;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new HomelessGuy();
        }
    }
}
