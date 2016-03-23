using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    public class HippieCamp: Card
    {
        public HippieCamp()
        {
            CardCost = 0;
            Name = "Hippie Camp";
            Description = "-1 Victory Point at the end of the game";
            Type = "victory";
            VictoryPoints = -1;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }
    }
}
