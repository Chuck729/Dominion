namespace RHFYP.Cards
{
    public class Apartment : Card // Village
    {
        public Apartment() : base(3, "Apartment", "+2 Managers and +1 Civilian", CardType.Action, 0, "apartments")
        {
            
        }
        /// <summary>
        /// player gets another card and 2 more managers
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
        {
            player.DrawCard();
            player.Managers += 2;
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
