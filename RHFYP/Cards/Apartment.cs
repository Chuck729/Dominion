namespace RHFYP.Cards
{
    public class Apartment : Card // Village
    {
        public Apartment() : base(3, "Apartment", "+2 actions and +1 card", CardType.Action, 0, "apartments")
        {
            
        }

        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.Managers += 1;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Apartment();
        }
    }
}
