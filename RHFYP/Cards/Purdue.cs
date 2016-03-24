using System;

namespace RHFYP.Cards
{
    public class Purdue: Card
    {
        public Purdue()
        {
            CardCost = 3;
            Name = "Purdue";
            Description = "This card is worth 1 victory point at the end of the game";
            Type = "victory";
            VictoryPoints = 1;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
