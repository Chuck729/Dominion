namespace RHFYP.Cards
{
    public class Gardens : Card
    {
        // TODO: This card has special victory point requirements. fix?
        public Gardens() : base(4, "Gardens", "+1 vp for evry 10 tiles on map (rounded down)", "victory", 0, "")
        {
        }

        /// <summary>
        ///     Does nothing when played.
        /// </summary>
        /// <param name="player"></param>
        public override void PlayCard(Player player)
        {
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Gardens();
        }
    }
}