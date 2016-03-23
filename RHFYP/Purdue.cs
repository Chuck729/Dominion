using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class Purdue: Card
    {
        public Purdue()
        {
            CardCost = 3;
            Name = "Purdue";
            Description = "This card is worth 1 victory point at the end of the game";
            Type = "victory";
            VictoryPoints = 1;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }
    }
}
