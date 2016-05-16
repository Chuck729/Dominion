using System;

namespace RHFYP.Cards.ActionCards
{
    public class Library : Card
    {
        public Library() : base(5, "Library", "Civilians visit tiles until 7 are visited.  If they visit an action tile you can choose to set aside that tile. The set aside tiles are not visited.", CardType.Action, 0, "library")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Library();
        }
    }
}
