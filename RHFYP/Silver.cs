using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Silver: Card
    {
        public Silver()
        {
            CardCost = 3;
            Name = "Silver";
            Description = "This card gives 2 coins when played";
            Type = "treasure";
            VictoryPoints = 0;
        }
    }
}
