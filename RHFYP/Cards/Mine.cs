using System;

namespace RHFYP.Cards
{
    public class Mine : Card
    {
        public Mine() : base(5, "Mine", "Upgrade a treasure tile.", CardType.Action, 0, "mine")
        {
        }

        public override void PlayCard(Player player)
        {
            var card = player.Hand.GetFirstCard(x => x.Name == "Small Business");
            if (card != null)
            {
                player.Hand.AddCard(new Company {Location = card.Location});
                return;
            }

            card = player.Hand.GetFirstCard(x => x.Name == "Company");
            if (card == null) return;
            player.Hand.AddCard(new Corporation { Location = card.Location });
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Mine();
        }
    }
}
