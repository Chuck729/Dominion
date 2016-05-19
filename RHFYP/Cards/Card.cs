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

        /// <inheritdocs/>
        public abstract ICard CreateCard();

        /// <summary>
        /// Set to true in order to not add this card to a deck.
        /// </summary>
        public bool TrashOnAdd { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


}