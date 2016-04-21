using System;

namespace RHFYP.Cards
{
    public class Cis : Card // Spy
    {
        public Cis() : base(4, "CIS", "+1 civilian +1 manager.  Each player reveals a card on top of thier deck and you get to discard it or put it back.", CardType.Action, 0, "cis")
        {
        }
        /// <summary>
        /// draw a card, add a manager, and look at each players top card of draw pile and decide whether to discard 
        /// or put it back on top of the draw pile
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
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
