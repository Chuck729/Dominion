using System;

namespace RHFYP.Cards
{
    public class Bank : Card // Adventurer
    {
        public Bank() : base(6, "Bank", "Civilians will visit two treasure tiles in your city.", CardType.Action, 0, "bank")
        {
        }
        /// <summary>
        /// draw until two treasure cards have been drawn, discard all other cards drawn
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
            return new Bank();
        }
    }
}
