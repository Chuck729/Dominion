using RHFYP.Cards;

namespace RHFYP
{
    public class StartUp : Card // Feast
    {
        public StartUp() : base(4, "StartUp", "Replace this tile with atile costing up to 5", CardType.Action, 0, "startup")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new StartUp();
        }
    }
}
