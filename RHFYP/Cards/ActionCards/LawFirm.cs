namespace RHFYP.Cards.ActionCards
{
    public class LawFirm : Card // Woodcutter
    {
        public LawFirm() : base(3, "Law Firm", "+1 investment +2 coins", CardType.Action, 0, "lawfirm")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.Investments++;
            player.Gold += 2;
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
