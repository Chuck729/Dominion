using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class One: Card
    {
        public One()
        {
            CardCost = 3;
            Name = "1 VP name";
            Description = "This card is worth 1 victory point at the end of the game";
            Type = "victory";
            VictoryPoints = 1;
        }
    }
}
