using System;

namespace RHFYP.Cards.ActionCards
{
    public class CIS : Card // Spy
    {
        public CIS() : base(4, "CIS", "+1 civilian +1 manager.  Each player reveals a card on top of thier deck and you get to discard it or put it back.", CardType.Action, 0, "cis")
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
            throw new NotImplementedException();
        }
    }
}
