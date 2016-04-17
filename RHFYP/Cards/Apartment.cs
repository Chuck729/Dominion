namespace RHFYP.Cards
{
    public class Apartment : Card // Village
    {
        public Apartment() : base(3, "Apartment", "+2 actions and +1 card", "action", 0, "apartments")
        {
            
        }

        public override void PlayCard(Player player)
        {

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
