using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Three: Card
    {
        public Three()
        {
            CardCost = 5;
            Name = "3 VP name";
            Description = "This card is worth 3 victory points at the end of the game";
            Type = "victory";
            VictoryPoints = 3;
        }
    }
}
