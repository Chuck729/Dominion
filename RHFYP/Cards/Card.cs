using System.Drawing;

namespace RHFYP.Cards
{
    public abstract class Card : ICard
    {
        protected Card(int cardCost, string name, string description, CardType type, int victoryPoints,
            string resourceName)
        {
            CardCost = cardCost;
            Name = name;
            Description = description;
            Type = type;
            VictoryPoints = victoryPoints;
            ResourceName = resourceName;
        }

        public int CardCost { get; set; }

        public string Name { get; set; }

        public CardType Type { get; set; }

        public string ResourceName { get; }

        public string Description { get; }

        public virtual int VictoryPoints { get; }

        public virtual Point Location { get; set; }

        public abstract void PlayCard(Player player, Game game);

        public virtual bool CanPlayCard(Player player, Game game)
        {
            return true;
        }

        public abstract ICard CreateCard();

        public override string ToString()
        {
            return Name;
        }
    }


}