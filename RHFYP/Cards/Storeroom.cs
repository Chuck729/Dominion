namespace RHFYP.Cards
{
    public class Storeroom : Card // Festival
    {
        public Storeroom() : base(5, "Storeroom", "+2 managers +2 gold +1 investment", CardType.Action, 0, "storeroom")
        {
        }

        public override void PlayCard(Player player)
        {
            player.Managers += 2;
            player.Gold += 2;
            player.Investments++;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Storeroom();
        }
    }
}
