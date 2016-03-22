using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    public class Bronze: Card
    {
        public Bronze()
        {
            CardCost = 1;
            Name = "Bronze";
            Description = "This card gives 1 coin when played";
            Type = "treasure";
            VictoryPoints = 0;
        }
    }
}
