using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Company: Card
    {
        public Company()
        {
            CardCost = 3;
            Name = "Company";
            Description = "This card gives 2 coins when played";
            Type = "treasure";
            VictoryPoints = 0;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }

    }
}
