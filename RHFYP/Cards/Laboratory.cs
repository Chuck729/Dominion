namespace RHFYP.Cards
{
    public class Laboratory : Card
    {
        public Laboratory() : base(5, "Laboratory", "+2 civilians +1 manager", CardType.Action, 0, "laboratory")
        {
        }

        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.DrawCard();
            player.Managers++;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Laboratory();
        }
    }
}
