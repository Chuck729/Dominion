using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Six: Card
    {
        public Six()
        {
            CardCost = 8;
            Name = "6 VP name";
            Description = "This card is worth 6 victory points at the end of the game";
            Type = "victory";
            VictoryPoints = 6;
        }
    }
}
