using System;

namespace RHFYP.Cards
{
    public class SpeedyLoans : Card // Money lender
    {
        public SpeedyLoans() : base(4, "Speedy Loans", "Destory a small business and +3 coins.", CardType.Action, 0, "speedyloans")
        {
        }
        /// <summary>
        /// if there is a small buisiness in the player's hand, it can be trashed
        /// and the player gains 3 coins
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
            return new SpeedyLoans();
        }
    }
}
