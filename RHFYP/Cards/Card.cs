using System;
using System.Drawing;

namespace RHFYP.Cards
{
    public abstract class Card : ICard
    {

        private bool _costIsSet;
        private bool _typeIsSet;
        private bool _descIsSet;
        private bool _nameIsSet;
        private bool _vpIsSet;

        public bool IsAddable { get; set; }
        private int _cardCost;

        public int CardCost { get; }
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        public string Name { get; }

        // The type of the card ("action", "victory", "treasure")
        public string Type { get; }

        // The description of what the card does when played
        public string Description { get; }

        //The amount of victory points each card is worth
        public int VictoryPoints { get; }

        public Card(int cardCost, string name, string description, string type, int victoryPoints)
        {
            CardCost = cardCost;
            Name = name;
            Description = description;
            Type = type;
            VictoryPoints = victoryPoints;
            IsAddable = true;
        }

        /// <summary>
        /// The location of this card on the map (if it's part of a map)
        /// </summary>
        public Point Location { get; set; }
     

        protected Card(bool isAddable)
        {
            IsAddable = isAddable;
        }

        protected Card()
        {
        }

        //abstract method that must be implemented for each card 
        //since each card has different results from it being played
        abstract public void PlayCard();
        
    }
}