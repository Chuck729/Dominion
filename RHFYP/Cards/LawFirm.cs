using System;

namespace RHFYP.Cards
{
    public class LawFirm : Card // Woodcutter
    {
        public LawFirm() : base(3, "Law Firm", "+1 investment +2 coins", CardType.Action, 0, "")
        {
        }

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
            return new LawFirm();
        }
    }
}
