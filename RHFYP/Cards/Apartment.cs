using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Apartment: Card
    {
        public Apartment()
        {
            CardCost = 3;
            Name = "Apartment";
            Description = "+2 actions and +1 card";
            Type = "action";
            VictoryPoints = 0;
        }

        public override void playCard()
        {

        }
    }
}
