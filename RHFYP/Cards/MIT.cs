namespace RHFYP.Cards
{
    public class Mit : Card
    {
        public Mit() : base(5, "MIT", "This card is worth 3 victory points at the end of the Game", CardType.Victory, 3, "mit")
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
            return new Mit();
        }
    }
}