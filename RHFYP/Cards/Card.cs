using System;
using System.Drawing;

namespace RHFYP.Cards
{
    public abstract class Card
    {

        private bool _costIsSet;
        private bool _typeIsSet;
        private bool _descIsSet;
        private bool _nameIsSet;
        private bool _vpIsSet;

        public bool IsAddable { get; set; }
        private int _cardCost;
        public int CardCost {
            get
            {
                return _cardCost;
            }
            set
            {
                if (_costIsSet)
                {
                    throw new Exception("Cannot change card cost.");
                }
                _costIsSet = true;
                _cardCost = value;
            }
        }
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        /// <remarks>
        /// This is what determines what image is displayed for this card.  
        /// This string must match the title of the resource (eg. "grass" or "corperation")
        /// </remarks>
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_nameIsSet)
                {
                    throw new Exception("Cannot change card name.");
                }
                _nameIsSet = true;
                _name = value;
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
        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_typeIsSet)
                {
                    throw new Exception("Cannot change card tpye.");
                }
                _typeIsSet = true;
                _type = value;
            }
        }

        // The description of what the card does when played
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_descIsSet)
                {
                    throw new Exception("Cannot change card description.");
                }
                _descIsSet = true;
                _description = value;
            }
        }

        //The amount of victory points each card is worth
        private int _victoryPoints;

        protected Card(bool isAddable)
        {
            IsAddable = isAddable;
        }

        public Card()
        {
        }

        public int VictoryPoints
        {
            get
            {
                return _victoryPoints;
            }
            set
            {
                if (_vpIsSet)
                {
                    throw new Exception("Cannot change victory point value.");
                }
                _vpIsSet = true;
                _victoryPoints = value;
            }
        }

        //abstract method that must be implemented for each card 
        //since each card has different results from it being played
        abstract public void PlayCard();
        
    }
}