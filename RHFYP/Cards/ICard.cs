using System.Drawing;

namespace RHFYP.Cards
{
    public enum CardType
    {
        Action,
        Treasure,
        Victory,
        System
    }

    public interface ICard
    {
        /// <summary>
        /// How must that cost cost to buy.
        /// </summary>
        int CardCost { get; set; }

        /// <summary>
        ///     The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        ///     This is what determines what image is displayed for this card.
        ///     This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        string Name { get; set; }
        
        /// <summary>
        /// The type of the card (CardType.Action, CardType.Victory, CardType.Treasure).
        /// </summary>
        CardType Type { get; set; }

        /// <summary>
        ///     The name of the image resource that represents this card.
        /// </summary>
        string ResourceName { get; }
        
        /// <summary>
        /// The description of what the card does when played.
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// The number of victory points each card is worth.
        /// </summary>
        int VictoryPoints { get; }

        /// <summary>
        ///     The tile point the card card should at in the <see>
        ///         <cref>MapUi</cref>
        ///     </see>
        ///     .
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Preforms the actions that the card is supposed to.
        /// </summary>
        /// <param name="player">The player whom played the card.</param>
        /// <param name="game">The game that is currently being played.</param>
        void PlayCard(Player player, Game game);

        bool CanPlayCard(Player player, Game game);

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        ICard CreateCard();
    }
}