namespace RHFYP.Cards
{
    public class Apartment : Card // Smithy
    {
        public Apartment() : base(4, "Apartment", "+3 civilians", CardType.Action, 0, "apartments")
        {
            
        }

        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
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
