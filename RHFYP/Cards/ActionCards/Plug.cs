using System;

namespace RHFYP.Cards.ActionCards
{
    public class Plug : Card // Witch
    {
        public Plug() : base(5, "Plug", "+2 civilians.  Every other play gains a hippie camp.", CardType.Action, 0, "plug")
        {
        }
        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.DrawCard();
            
            throw new NotImplementedException();
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
