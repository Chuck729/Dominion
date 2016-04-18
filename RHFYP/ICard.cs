using System.Drawing;

namespace RHFYP
{
    public interface ICard
    {
        int CardCost { get; }

        /// <summary>
        ///     The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        ///     This is what determines what image is displayed for this card.
        ///     This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        string Name { get; }

        // The type of the card ("action", "victory", "treasure").
        string Type { get; set; }

        /// <summary>
        ///     The name of the image resource that represents this card.
        /// </summary>
        string ResourceName { get; }

        // The description of what the card does when played.
        string Description { get; }

        //The amount of victory points each card is worth.
        int VictoryPoints { get; }

        /// <summary>
        ///     The tile point the card card should at in the <see>
        ///         <cref>MapUi</cref>
        ///     </see>
        ///     .
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        ///     Gets set to false when it's put in a deck and true when it's removed to ensure it
        ///     only can exist in one deck.
        /// </summary>
        bool IsAddable { get; set; }


        void PlayCard(Player player);

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        ICard CreateCard();
    }
}