using System;
using System.Drawing;

namespace RHFYP.Cards
{
    public abstract class Card
    {

        public bool IsAddable { get; set; }

        public readonly int CardCost;
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        public readonly string Name;

        // The type of the card ("action", "victory", "treasure")
        public readonly string Type;

        // The description of what the card does when played
        public readonly string Description;

        //The amount of victory points each card is worth
        public readonly int VictoryPoints;

        public Card(int cardCost, string name, string description, string type, int victoryPoints)
        {
            CardCost = cardCost;
            Name = name;
            Type = type;
            Description = description;
            VictoryPoints = victoryPoints;
            IsAddable = true;
        }

        /// <summary>
        /// The location of this card on the map (if it's part of a map)
        /// </summary>
        public Point Location { get; set; }


        public bool CanAfford(Player player)
        {
            // TODO: Check if the player can afford the card and if they can return true;
            return true;
        }

        protected Card(bool isAddable)
        {
            IsAddable = isAddable;
        }

        //abstract method that must be implemented for each card 
        //since each card has different results from it being played
        abstract public void PlayCard();
        
    }
}