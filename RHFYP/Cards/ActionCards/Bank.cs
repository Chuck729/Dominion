using System;

namespace RHFYP.Cards.ActionCards
{
    public class Bank : Card // Adventurer
    {
        public Bank() : base(6, "Bank", "Civilians will visit two treasure tiles in your city.", CardType.Action, 0, "bank")
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
            return new Bank();
        }
    }
}
