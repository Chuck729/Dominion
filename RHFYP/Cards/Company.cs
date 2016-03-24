using System;

namespace RHFYP.Cards
{
    public class Company: Card
    {
        public Company()
        {
            CardCost = 3;
            Name = "Company";
            Description = "This card gives 2 coins when played";
            Type = "treasure";
            VictoryPoints = 0;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }

    }
}
