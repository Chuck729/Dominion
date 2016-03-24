using System;

namespace RHFYP.Cards
{
    public class SmallBusiness: Card
    {
        public SmallBusiness()
        {
            CardCost = 1;
            Name = "Small Business";
            Description = "This card gives 1 coin when played";
            Type = "treasure";
            VictoryPoints = 0;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
