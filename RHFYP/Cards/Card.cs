using System;
using System.Data.Common;
using System.Drawing;

namespace RHFYP
{
    public abstract class Card
    {

        private bool CostIsSet = false;
        private bool TypeIsSet = false;
        private bool DescIsSet = false;
        private bool NameIsSet = false;

        private int _CardCost;
        public int CardCost {
            get
            {
                return _CardCost;
            }
            set
            {
                if (CostIsSet)
                {
                    throw new Exception();
                }
                CostIsSet = true;
                _CardCost = value;
            }
        }
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (NameIsSet)
                {
                    throw new Exception();
                }
                NameIsSet = true;
                _Name = value;
            }
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

        // The type of the card ("action", "victory", "treasure")
        private string _Type;
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (TypeIsSet)
                {
                    throw new Exception();
                }
                TypeIsSet = true;
                _Type = value;
            }
        }

        // The description of what the card does when played
        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (DescIsSet)
                {
                    throw new Exception();
                }
                DescIsSet = true;
                _Description = value;
            }
        }

        //The amount of victory points each card is worth
        public int VictoryPoints { get; set; }

        //abstract method that must be implemented for each card 
        //since each card has different results from it being played
        abstract public void playCard();
        
    }
}