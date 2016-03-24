using System;

namespace RHFYP.Cards
{
    public class Corporation: Card
    {
        public Corporation()
        {
            CardCost = 6;
            Name = "Corporation";
            Description = "This card gives 3 coins when played";
            Type = "treasure";
            VictoryPoints = 0;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
