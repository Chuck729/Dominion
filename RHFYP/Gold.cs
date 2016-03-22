using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Gold: Card
    {
        public Gold()
        {
            CardCost = 6;
            Name = "Gold";
            Description = "This card gives 3 coins when played";
            Type = "treasure";
            VictoryPoints = 0;
        }
    }
}
