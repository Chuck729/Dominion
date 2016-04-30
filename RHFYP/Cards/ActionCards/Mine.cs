using RHFYP.Cards.TreasureCards;

namespace RHFYP.Cards.ActionCards
{
    public class Mine : Card
    {
        public Mine() : base(5, "Mine", "Upgrade a treasure tile.", CardType.Action, 0, "mine")
        {
        }

        /// <summary>
        /// Upgrades the first <see cref="SmallBusiness"/> card in the players hand if there is one
        /// to a <see cref="Company"/> card at the same location.
        /// If there are no <see cref="SmallBusiness"/> card in the players hand then the first
        /// <see cref="Company"/> in the players hand becomes a <see cref="Corporation"/>.  If there
        /// are nethier then this card does nothing.
        /// </summary>
        /// <param name="player">The player who is player the card.</param>
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
