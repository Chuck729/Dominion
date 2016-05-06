using System.Drawing;

namespace RHFYP.Cards
{
    public abstract class Card : ICard
    {

        public bool IsAddable { get; set; }

        public int CardCost { get; set; }

        public string Name { get; set; }
        
        public CardType Type { get; set; }
        
        public string ResourceName { get; }
        
        public string Description { get; }
        
        public virtual int VictoryPoints { get; }

        protected Card(int cardCost, string name, string description, CardType type, int victoryPoints, string resourceName)
        {
            CardCost = cardCost;
            Name = name;
            Description = description;
            Type = type;
            VictoryPoints = victoryPoints;
            ResourceName = resourceName;
            IsAddable = true;
        }

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

        abstract public void PlayCard(Player player, Game game);

        public abstract ICard CreateCard();

    }
}