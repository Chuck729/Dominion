using System;
using System.Collections.Generic;

namespace RHFYP.Cards
{
    public class Area51 : Card // Chapel
    {
        public Area51() : base(2, "Area 51", "Nuke up to 4 tiles that civilians are currently at", CardType.Action, 0, "area51")
        {
        }

        /// <summary>
        /// Playing this card will remove up to four selected cards
        /// in the current player's hand and place them in the trash
        /// pile.
        /// </summary>
        /// <param name="player"></param> The player that played this card.
        public override void PlayCard(Player player)
        {
            if (player == null) throw new ArgumentNullException();
            throw new NotImplementedException();
        }

        public void PlayCard(Player player, List<ICard> listOfCards)
        {
            foreach (ICard c in listOfCards)
            {
                player.TrashCard(c);
            }
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Area51();
        }
    }
}
