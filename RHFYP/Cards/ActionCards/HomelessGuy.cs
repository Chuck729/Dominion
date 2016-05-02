namespace RHFYP.Cards.ActionCards
{
    public class HomelessGuy : Card // Cellar
    {
        public HomelessGuy() : base(2, "Homeless Guy", "Activates one of your tiles and allows you to randomly relolate any of your existing civilians", CardType.Action, 0, "")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            player.Managers++;
            // TODO: Player picks a card to discard
            player.DrawCard();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new HomelessGuy();
        }
    }
}
