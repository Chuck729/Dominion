using System;
using System.Data.Common;
using System.Drawing;

namespace RHFYP
{
    public class Card
    { 
    
        public int CardCost { get; set; }
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// The location of this card on the map (if it's part of a map)
        /// </summary>
        public Point Location { get; set; }

        public Card()
        {
            // NOTE: ALL TEMP DEFAULTS FOR TESTING.
            Type = "familybusiness";
        }


        public bool CanAfford(Player player)
        {
            // TODO: Check if the player can afford the card and if they can return true;
            return true;
        }

    }
}