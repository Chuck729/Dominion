namespace RHFYP.Cards.ActionCards
{
    public class MilitaryBase : Card // Moat
    {
        public MilitaryBase() : base(2, "Military Base", "+2 Civilians", CardType.Action, 0, "militarybase")
        {
        }

        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.DrawCard();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new MilitaryBase();
        }
    }
}
