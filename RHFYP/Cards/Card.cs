using System;
using System.Data.Common;
using System.Drawing;

namespace RHFYP
{
    public abstract class Card
    { 
    
        public int CardCost { get; set; }
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// The location of this card on the map (if it's part of a map)
        /// </summary>
        public Point Location { get; set; }


        public bool CanAfford(Player player)
        {
            // TODO: Check if the player can afford the card and if they can return true;
            return true;
        }

        // The type of the card ("action", "victory", "treasure")
        public string Type { get; set; }

        // The description of what the card does when played
        public string Description { get; set; }

        //The amount of victory points each card is worth
        public int VictoryPoints { get; set; }

        //abstract method that must be implemented for each card 
        //since each card has different results from it being played
        abstract public void playCard();
        
    }
}