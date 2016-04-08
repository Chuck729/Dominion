using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP.Cards
{
    public interface ICard
    {
        int CardCost { get; }
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        string Name { get; }

        // The type of the card ("action", "victory", "treasure").
        string Type { get; }

        /// <summary>
        /// The name of the image resource that represents this card.
        /// </summary>
        string ResourceName { get; }

        // The description of what the card does when played.
        string Description { get; }

        //The amount of victory points each card is worth.
        int VictoryPoints { get; }

        Point Location { get; set; }


        bool IsAddable { get; set; }

        
        void PlayCard(Player player);
    }
}
