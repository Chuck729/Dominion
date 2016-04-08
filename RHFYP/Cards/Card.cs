using System.Drawing;

namespace RHFYP.Cards
{
    public abstract class Card : ICard
    {
        /// <summary>
        /// True if the card can be added to a deck.  Used by the deck class to prevent card duplication.
        /// </summary>
        public bool IsAddable { get; set; }

        /// <summary>
        /// How many coins the card costs.
        /// </summary>
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

        /// <summary>
        /// The name of the image resource that represents this card.
        /// </summary>
        public string ResourceName { get; }

        // The description of what the card does when played
        public string Description { get; }

        //The amount of victory points each card is worth
        public int VictoryPoints { get; }

        protected Card(int cardCost, string name, string description, string type, int victoryPoints, string resourceName)
        {
            CardCost = cardCost;
            Name = name;
            Description = description;
            Type = type;
            VictoryPoints = victoryPoints;
            ResourceName = resourceName;
            IsAddable = true;
        }

        /// <summary>
        /// The location of this card on the map (if it's part of a map)
        /// </summary>
        public Point Location { get; set; }
     

        protected Card(bool isAddable, string resourceName)
        {
            IsAddable = isAddable;
            ResourceName = resourceName;
        }

        protected Card(string resourceName)
        {
            ResourceName = resourceName;
        }

        //abstract method that must be implemented for each card 
        //since each card has different results from it being played
        //the card will modify the player's fields based on what the card does
        abstract public void PlayCard(Player player);
        
    }
}